using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ZEDFramework.Collection.Infrastructure.Repository;
using ZEDFramework.Collection.Infrastructure.UnitOfWork;

namespace ZEDFramework.Collection.Infrastructure.EntityFramework.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly IUnitOfWorkFactory UnitOfWorkFactory;

        public Repository(IUnitOfWorkFactory unitOfWorkFactory)
        {
            this.UnitOfWorkFactory = unitOfWorkFactory;
        }

        public void Delete(TEntity entity)
        {
            this.Context.Entry<TEntity>(entity).State = EntityState.Deleted;
            this.Context.SaveChanges();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate)
        {
            return this.EntityModel.Where<TEntity>(predicate).FirstOrDefault<TEntity>();
        }

        public TEntity Get(Expression<Func<TEntity, bool>> predicate, string include)
        {
            return this.EntityModel.Where<TEntity>(predicate).Include<TEntity>(include).FirstOrDefault<TEntity>();
        }

        public IEnumerable<TEntity> GetList()
        {
            return this.EntityModel.AsEnumerable<TEntity>();
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate)
        {
            return this.EntityModel.Where<TEntity>(predicate).AsEnumerable<TEntity>();
        }

        public IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, string include)
        {
            return this.EntityModel.Where<TEntity>(predicate).Include<TEntity>(include).AsEnumerable<TEntity>();
        }

        public PagingResponse<TEntity> GetPage(PagingRequest request)
        {
            PagingResponse<TEntity> response = new PagingResponse<TEntity>();

            Expression<Func<TEntity, bool>> predicate = null;

            if ((request.FilterParameter != null) && (request.FilterParameter.Count > 0))
            {
                predicate = request.FilterParameter.ToExpression<TEntity>();
            }

            if (!request.OrderByType.HasValue)
            {
                request.OrderByType = OrderByType.Asc;
            }

            if (!request.SkipCount.HasValue)
            {
                request.SkipCount = 0;
            }

            IQueryable<TEntity> source = (predicate != null) ? this.EntityModel.Where<TEntity>(predicate).AsQueryable<TEntity>() : this.EntityModel.AsQueryable<TEntity>();

            response.TotalCount = source.Count<TEntity>();

            if (!request.TakeCount.HasValue)
            {
                request.TakeCount = response.TotalCount;
            }

            response.Result = source.ToOrderBy<TEntity>(request.OrderColumn, request.OrderByType.Value).Skip<TEntity>(request.SkipCount.Value).Take<TEntity>(request.TakeCount.Value);

            return response;
        }

        public PagingResponse<TQueryModel> GetPage<TQueryModel>(IQueryable<TQueryModel> query, PagingRequest request) where TQueryModel : class
        {
            PagingResponse<TQueryModel> response = new PagingResponse<TQueryModel>();

            Expression<Func<TQueryModel, bool>> predicate = null;

            if ((request.FilterParameter != null) && (request.FilterParameter.Count > 0))
            {
                predicate = request.FilterParameter.ToExpression<TQueryModel>();
            }

            if (!request.OrderByType.HasValue)
            {
                request.OrderByType = OrderByType.Asc;
            }

            if (!request.SkipCount.HasValue)
            {
                request.SkipCount = 0;
            }

            IQueryable<TQueryModel> source = (predicate != null) ? query.Where<TQueryModel>(predicate).AsQueryable<TQueryModel>() : query.AsQueryable<TQueryModel>();

            response.TotalCount = source.Count<TQueryModel>();

            if (!request.TakeCount.HasValue)
            {
                request.TakeCount = response.TotalCount;
            }

            response.Result = source.ToOrderBy<TQueryModel>(request.OrderColumn, request.OrderByType.Value).Skip<TQueryModel>(request.SkipCount.Value).Take<TQueryModel>(request.TakeCount.Value).AsEnumerable<TQueryModel>();

            return response;
        }

        public void Insert(TEntity entity)
        {
            this.EntityModel.Add(entity);
            this.Context.SaveChanges();
        }

        public void Update(TEntity entity)
        {
            if (this.Context.Entry<TEntity>(entity).State == EntityState.Detached)
            {
                this.EntityModel.Attach(entity);
            }
            this.Context.Entry<TEntity>(entity).State = EntityState.Modified;
            this.Context.SaveChanges();
        }

        private DbContext Context
        {
            get
            {
                return (DbContext)this.UnitOfWorkFactory.GetCurrentDbObject;
            }
        }

        private DbSet<TEntity> EntityModel
        {
            get
            {
                return this.Context.Set<TEntity>();
            }
        }
    }
}
