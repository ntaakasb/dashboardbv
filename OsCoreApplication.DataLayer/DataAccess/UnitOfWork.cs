using System;
using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.DataLayer.Infrastructure;

namespace OsCoreApplication.DataLayer.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;

        private EFDBEntities _dbContext;
        public EFDBEntities DbContext => _dbContext ?? (_dbContext = new EFDBEntities());

        public UnitOfWork()
        {
            _dbContext = new EFDBEntities();
        }

        private IRepository<TB_KHACHHANG> _khachhangRepository;
        private IRepository<TB_CHITIETDKKHAM> _DKKhamRepository;
        private IRepository<CHART_THONGKETHEOTUAN> _ThongKeTuan;

        // private IRepository<OS_Schedule> _scheduleRepository;

        public IRepository<TB_KHACHHANG> KhachHangRepository => _khachhangRepository ?? (_khachhangRepository = new Repository<TB_KHACHHANG>(_dbContext));

        public IRepository<TB_CHITIETDKKHAM> DKKhamRepository => _DKKhamRepository ?? (_DKKhamRepository = new Repository<TB_CHITIETDKKHAM>(_dbContext));

        public IRepository<CHART_THONGKETHEOTUAN> ThongKeTuan => _ThongKeTuan ?? (_ThongKeTuan = new Repository<CHART_THONGKETHEOTUAN>(_dbContext));
        // public IRepository<OS_Schedule> ScheduleRepository => _scheduleRepository ?? (_scheduleRepository = new Repository<OS_Schedule>(_dbContext));

        public void SaveChanges()
        {
            CheckIsDisposed();
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _dbContext?.Dispose();
            }
            _disposed = true;
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
