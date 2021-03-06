using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.IdentityFramework;
using DTT.LRM.Authorization;
using DTT.LRM.Authorization.Roles;
using DTT.LRM.Authorization.Users;
using DTT.LRM.Roles.Dto;
using DTT.LRM.Share;
using DTT.LRM.Users.Dto;
using Microsoft.AspNet.Identity;
using System.Linq.Dynamic.Core;
using Abp.Linq.Extensions;

namespace DTT.LRM.Users
{
    public class UserAppService : AsyncCrudAppService<User, UserDto, long, PagedResultRequestDto, CreateUserDto, UpdateUserDto>, IUserAppService
    {
        private readonly UserManager _userManager;
        private readonly RoleManager _roleManager;
        private readonly IRepository<Role> _roleRepository;
        private readonly IRepository<User,long> _userRepository;

        public UserAppService(
            IRepository<User, long> repository,
            UserManager userManager,
            IRepository<Role> roleRepository,
            IRepository<User, long> userRepository,
            RoleManager roleManager)
            : base(repository)
        {
            _userManager = userManager;
            _roleRepository = roleRepository;
            _roleManager = roleManager;
            _userRepository = userRepository;
        }

        public override async Task<UserDto> GetAsync(EntityDto<long> input)
        {
            var user = await base.GetAsync(input);
            var userRoles = await _userManager.GetRolesAsync(user.Id);
            user.Roles = userRoles.Select(ur => ur).ToArray();
            return user;
        }

        public override async Task<UserDto> CreateAsync(CreateUserDto input)
        {
            //CheckCreatePermission();

            var user = ObjectMapper.Map<User>(input);

            user.TenantId = AbpSession.TenantId;
            user.Password = new PasswordHasher().HashPassword(input.Password);
            user.IsEmailConfirmed = true;

            //Assign roles
            user.Roles = new Collection<UserRole>();
            foreach (var roleName in input.RoleNames)
            {
                var role = await _roleManager.GetRoleByNameAsync(roleName);
                user.Roles.Add(new UserRole(AbpSession.TenantId, user.Id, role.Id));
            }

            CheckErrors(await _userManager.CreateAsync(user));

            await CurrentUnitOfWork.SaveChangesAsync();

            return MapToEntityDto(user);
        }

        public override async Task<UserDto> UpdateAsync(UpdateUserDto input)
        {
            //CheckUpdatePermission();

            var user = await _userManager.GetUserByIdAsync(input.Id);

            //MapToEntity(input, user);

            //CheckErrors(await _userManager.UpdateAsync(user));

            if (input.RoleNames != null)
            {
                CheckErrors(await _userManager.SetRoles(user, input.RoleNames));
            }

            return await GetAsync(input);
        }

        public override async Task DeleteAsync(EntityDto<long> input)
        {
            var user = await _userManager.GetUserByIdAsync(input.Id);
            await _userManager.DeleteAsync(user);
        }

        public async Task<ListResultDto<RoleDto>> GetRoles()
        {
            var roles = await _roleRepository.GetAllListAsync();
            return new ListResultDto<RoleDto>(ObjectMapper.Map<List<RoleDto>>(roles));
        }

        protected override User MapToEntity(CreateUserDto createInput)
        {
            var user = ObjectMapper.Map<User>(createInput);
            return user;
        }

        protected override void MapToEntity(UpdateUserDto input, User user)
        {
            ObjectMapper.Map(input, user);
        }

        protected override IQueryable<User> CreateFilteredQuery(PagedResultRequestDto input)
        {
            return Repository.GetAllIncluding(x => x.Roles);
        }

        protected override async Task<User> GetEntityByIdAsync(long id)
        {
            var user = Repository.GetAllIncluding(x => x.Roles).FirstOrDefault(x => x.Id == id);
            return await Task.FromResult(user);
        }

        protected override IQueryable<User> ApplySorting(IQueryable<User> query, PagedResultRequestDto input)
        {
            return query.OrderBy(r => r.UserName);
        }

        protected virtual void CheckErrors(IdentityResult identityResult)
        {
            identityResult.CheckErrors(LocalizationManager);
        }

        public async Task<string> UserNameIsExist(string userName, long id)
        {
            var user = await _userManager.FindByNameAsync(userName);
            if (user != null && user.Id != id)
                return "Tài khoản đã tồn tại.";
            return string.Empty;
        }

        public async Task<PagedResultExtendDto<UserIndexDto>> GetAll(PagedResultRequestExtendDto input)
        {
            var keyword = string.IsNullOrEmpty(input.Keyword) ? string.Empty : input.Keyword.ToLower();
            var status = input.Status;
            var listUser = _userRepository.GetAll().Where(x=>x.Id > 1 &&
                                                            (x.UserName.ToLower().Contains(keyword)||
                                                            x.Surname.ToLower().Contains(keyword)||
                                                            x.EmailAddress.ToLower().Contains(keyword))&&
                                                            (status == null ? true : (status==0 ? x.IsActive == false : x.IsActive == true)));
            var items = listUser.OrderBy("id DESC").PageBy(input).ToList();
            var listItems = ObjectMapper.Map<List<UserIndexDto>>(items);
            return new PagedResultExtendDto<UserIndexDto>(totalCount: listUser.Count(), items: listItems, countStatus: null);
        }

        public async Task<int> ActiveToggle(long userId)
        {
            var user = await _userRepository.GetAsync(userId);
            if (user == null)
                return 0;
            user.IsActive = user.IsActive == true ? false : true;
            await _userRepository.UpdateAsync(user);
            return 1;
        }
    }
}