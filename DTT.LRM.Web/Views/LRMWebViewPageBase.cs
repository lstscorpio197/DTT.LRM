using Abp.Web.Mvc.Views;

namespace DTT.LRM.Web.Views
{
    public abstract class LRMWebViewPageBase : LRMWebViewPageBase<dynamic>
    {

    }

    public abstract class LRMWebViewPageBase<TModel> : AbpWebViewPage<TModel>
    {
        protected LRMWebViewPageBase()
        {
            LocalizationSourceName = LRMConsts.LocalizationSourceName;
        }
    }
}