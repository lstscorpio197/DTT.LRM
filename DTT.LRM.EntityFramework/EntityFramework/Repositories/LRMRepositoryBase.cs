using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace DTT.LRM.EntityFramework.Repositories
{
    public abstract class LRMRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<LRMDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected LRMRepositoryBase(IDbContextProvider<LRMDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class LRMRepositoryBase<TEntity> : LRMRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected LRMRepositoryBase(IDbContextProvider<LRMDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
