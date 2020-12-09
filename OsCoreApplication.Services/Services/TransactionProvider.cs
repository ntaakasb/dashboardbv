using System;
using System.Data;
using System.Data.Entity;
using OsCoreApplication.DataLayer.Infrastructure;

namespace OsCoreApplication.Services
{
    public class TransactionProvider : ITransactionProvider
    {
        private IUnitOfWork _unitOfWork;
        private DbContextTransaction _transaction;
        private bool _begin = false;

        public TransactionProvider(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void Begin(IsolationLevel isolationLevel)
        {
           if(_begin) return;
            _transaction?.Dispose();
            _transaction = _unitOfWork.DbContext.Database.BeginTransaction(isolationLevel);
            _begin = true;
        }

        public void Commit()
        {
            CheckIsDisposed();
            CheckHasBegun();
            _transaction.Commit();
            _begin = false;
        }

        public void RollBack()
        {
            CheckIsDisposed();
            CheckHasBegun();
            _transaction.Rollback();
            _begin = false;
        }

        private bool _disposed = false;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!this._disposed)
            {
                if (disposing)
                {
                    if (_transaction != null)
                    {
                        _transaction.Dispose();
                    }
                }
            }
            _disposed = true;
        }

        private void CheckHasBegun()
        {
            if (!_begin)
            {
                throw new InvalidOperationException("Must call Begin() on the unit of work before committing");
            }
        }

        private void CheckIsDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}
