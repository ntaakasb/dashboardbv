using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace OsCoreApplication.DataLayer.Infrastructure
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        int Count { get; }

        void Insert(TEntity entity);

        void InsertRange(IEnumerable<TEntity> entities);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        void Delete(object id);

        void Delete(Expression<Func<TEntity, bool>> condition);

        IQueryable<TEntity> GetAllData();

        TEntity FindById(object id);

        TEntity Find(Expression<Func<TEntity, bool>> condition);

        IQueryable<TEntity> Filter(Expression<Func<TEntity, bool>> condition);

        bool HasRows(Expression<Func<TEntity, bool>> condition);

        List<T> ExecStoreProdure<T>(string strProcname, object[] param);
    }
}
