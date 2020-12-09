using System;
using OsCoreApplication.DataLayer.EFModel;

namespace OsCoreApplication.DataLayer.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        EFDBEntities DbContext { get; }

        void SaveChanges();

        IRepository<TB_KHACHHANG> KhachHangRepository { get; }
        IRepository<TB_CHITIETDKKHAM> DKKhamRepository { get; }
        //  IRepository<OS_Schedule> ScheduleRepository { get; }


    }
}
