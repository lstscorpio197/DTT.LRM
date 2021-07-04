using System.Data.Common;
using System.Data.Entity;
using Abp.DynamicEntityProperties;
using Abp.Zero.EntityFramework;
using DTT.LRM.Authorization.Roles;
using DTT.LRM.Authorization.Users;
using DTT.LRM.BookBorrows;
using DTT.LRM.BookCategories;
using DTT.LRM.BookClassifies;
using DTT.LRM.BookFields;
using DTT.LRM.BookGiveBacks;
using DTT.LRM.BookLiquidations;
using DTT.LRM.BookReaderUsings;
using DTT.LRM.Books;
using DTT.LRM.Borrows;
using DTT.LRM.Employees;
using DTT.LRM.GiveBacks;
using DTT.LRM.Liquidations;
using DTT.LRM.MultiTenancy;
using DTT.LRM.PositionQuotas;
using DTT.LRM.Positions;
using DTT.LRM.Publishers;
using DTT.LRM.Readers;
using DTT.LRM.Violates;

namespace DTT.LRM.EntityFramework
{
    public class LRMDbContext : AbpZeroDbContext<Tenant, Role, User>
    {
        //TODO: Define an IDbSet for your Entities...
        public DbSet<BookClassify> BookClassifies { get; set; }
        public DbSet<BookField> BookFields { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<Publisher> Publishers { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<PositionQuota> PositionQuotas { get; set; }
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Book> Books { get; set; }

        public DbSet<Borrow> Borrows { get; set; }
        public DbSet<BookBorrow> BookBorrows { get; set; }
        public DbSet<GiveBack> GiveBacks { get; set; }
        public DbSet<BookGiveBack> BookGiveBacks { get; set; }
        public DbSet<BookReaderUsing> BookReaderUsings { get; set; }
        public DbSet<Violate> Violates { get; set; }
        public DbSet<Liquidation> Liquidations { get; set; }
        public DbSet<BookLiquidation> BookLiquidations { get; set; }
        /* NOTE: 
         *   Setting "Default" to base class helps us when working migration commands on Package Manager Console.
         *   But it may cause problems when working Migrate.exe of EF. If you will apply migrations on command line, do not
         *   pass connection string name to base classes. ABP works either way.
         */
        public LRMDbContext()
            : base("Default")
        {

        }

        /* NOTE:
         *   This constructor is used by ABP to pass connection string defined in LRMDataModule.PreInitialize.
         *   Notice that, actually you will not directly create an instance of LRMDbContext since ABP automatically handles it.
         */
        public LRMDbContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {

        }

        //This constructor is used in tests
        public LRMDbContext(DbConnection existingConnection)
         : base(existingConnection, false)
        {

        }

        public LRMDbContext(DbConnection existingConnection, bool contextOwnsConnection)
         : base(existingConnection, contextOwnsConnection)
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DynamicProperty>().Property(p => p.PropertyName).HasMaxLength(250);
            modelBuilder.Entity<DynamicEntityProperty>().Property(p => p.EntityFullName).HasMaxLength(250);
        }
    }
}
