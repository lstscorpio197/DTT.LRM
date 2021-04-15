using System.Linq;
using DTT.LRM.EntityFramework;
using DTT.LRM.MultiTenancy;

namespace DTT.LRM.Migrations.SeedData
{
    public class DefaultTenantCreator
    {
        private readonly LRMDbContext _context;

        public DefaultTenantCreator(LRMDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateUserAndRoles();
        }

        private void CreateUserAndRoles()
        {
            //Default tenant

            var defaultTenant = _context.Tenants.FirstOrDefault(t => t.TenancyName == Tenant.DefaultTenantName);
            if (defaultTenant == null)
            {
                _context.Tenants.Add(new Tenant {TenancyName = Tenant.DefaultTenantName, Name = Tenant.DefaultTenantName});
                _context.SaveChanges();
            }
        }
    }
}
