using System.Threading.Tasks;
using Abp.Application.Services;
using DTT.LRM.Configuration.Dto;

namespace DTT.LRM.Configuration
{
    public interface IConfigurationAppService: IApplicationService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}