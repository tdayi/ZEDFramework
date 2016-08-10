using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ZEDFramework.Collection.Infrastructure.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Delete(TEntity entity);
        TEntity Get(Expression<Func<TEntity, bool>> predicate);
        TEntity Get(Expression<Func<TEntity, bool>> predicate, string include);
        IEnumerable<TEntity> GetList();
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> GetList(Expression<Func<TEntity, bool>> predicate, string include);
        PagingResponse<TEntity> GetPage(PagingRequest request);
        PagingResponse<TQueryModel> GetPage<TQueryModel>(IQueryable<TQueryModel> query, PagingRequest request) where TQueryModel : class;
        void Insert(TEntity entity);
        void Update(TEntity entity);
    }
}
