using System.Data.Entity;
using System.Reflection;
using Abp.Modules;
using DTT.LRM.EntityFramework;

namespace DTT.LRM.Migrator
{
    [DependsOn(typeof(LRMDataModule))]
    public class LRMMigratorModule : AbpModule
    {
        public override void PreInitialize()
        {
            Database.SetInitializer<LRMDbContext>(null);

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(Assembly.GetExecutingAssembly());
        }
    }
}