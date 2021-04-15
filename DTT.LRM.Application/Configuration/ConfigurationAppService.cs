using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using DTT.LRM.Configuration.Dto;

namespace DTT.LRM.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : LRMAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
