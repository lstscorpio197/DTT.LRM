using DTT.LRM.EntityFramework;
using EntityFramework.DynamicFilters;

namespace DTT.LRM.Migrations.SeedData
{
    public class InitialHostDbBuilder
    {
        private readonly LRMDbContext _context;

        public InitialHostDbBuilder(LRMDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            _context.DisableAllFilters();

            new DefaultEditionsCreator(_context).Create();
            new DefaultLanguagesCreator(_context).Create();
            new HostRoleAndUserCreator(_context).Create();
            new DefaultSettingsCreator(_context).Create();
        }
    }
}
