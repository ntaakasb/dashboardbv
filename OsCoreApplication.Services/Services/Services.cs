using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.DataLayer.Infrastructure;
using OsCoreApplication.Libraries.Memcached;
using OsCoreApplication.Logger;
using OsCoreApplication.ViewModel.Models;

namespace OsCoreApplication.Services
{
    public partial class Services : IServices
    {
        private readonly IUnitOfWork _unitOfWork;

        public Services(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<int> getDetailBenhNhanTheoTuan(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                DayOfWeek weekStart = DayOfWeek.Sunday; // or Sunday, or whenever
                DateTime startingDate = DateTime.Today;

                while (startingDate.DayOfWeek != weekStart)
                    startingDate = startingDate.AddDays(-1);

                DateTime previousWeekStart = startingDate.AddDays(-7);
                DateTime previousWeekEnd = startingDate.AddDays(-1);

                var lsValue = new List<int>();
                var lsResult = new List<TB_CHITIETDKKHAM>();
                lsResult = _unitOfWork.DKKhamRepository.Filter(c => DbFunctions.TruncateTime(c.NGAYDANGKY) <= previousWeekEnd && DbFunctions.TruncateTime(c.NGAYDANGKY) >= previousWeekStart).ToList();
                if (lsResult != null)
                {
                    double runDate = (previousWeekEnd - previousWeekStart).TotalDays;
                    int i = 1;
                    while (i <= 7)
                    {
                        var tempSL = lsResult.Where(x => x.NGAYDANGKY == previousWeekStart.AddDays(i)).ToList();
                        lsValue.Add(tempSL != null ? tempSL.Count() : 0);
                        i++;
                    }
                }
                return lsValue;
            }
            catch (Exception ex)
            {
                OsLog.Error("ConfigWebServices --> getDetailBenhNhanTheoTuan ", ex);
                return new List<int>();
            }
        }

        public ThongKeModel getThongKeTheoTuan(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                DayOfWeek weekStart = DayOfWeek.Monday; // or Sunday, or whenever
                DateTime startingDate = DateTime.Today;

                while (startingDate.DayOfWeek != weekStart)
                    startingDate = startingDate.AddDays(-1);

                DateTime previousWeekStart = startingDate.AddDays(-7);
                DateTime previousWeekEnd = startingDate.AddDays(-1);

                var thongke = new ThongKeModel();
                var lsResult = new List<TB_CHITIETDKKHAM>();
                lsResult = _unitOfWork.DKKhamRepository.Filter(c => DbFunctions.TruncateTime(c.NGAYDANGKY) <= previousWeekEnd && DbFunctions.TruncateTime(c.NGAYDANGKY) >= previousWeekStart).ToList();
                if (lsResult != null)
                {
                    thongke.TongSoDangKy = lsResult.Count();
                    thongke.ChoKham = lsResult.Where(x => x.MATRANGTHAI == 2).Count();
                    thongke.DangKham = lsResult.Where(x => x.MATRANGTHAI == 3).Count();
                    thongke.DaKham = lsResult.Where(x => x.MATRANGTHAI == 4).Count();
                }
                return thongke;
            }
            catch (Exception ex)
            {
                OsLog.Error("ConfigWebServices --> getThongKe ", ex);
                return new ThongKeModel();
            }
        }

        public ThongKeTheoChuyenKhoa getThongKeTheoKhoa(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                DayOfWeek weekStart = DayOfWeek.Monday; // or Sunday, or whenever
                DateTime startingDate = DateTime.Today;

                while (startingDate.DayOfWeek != weekStart)
                    startingDate = startingDate.AddDays(-1);

                DateTime previousWeekStart = startingDate.AddDays(-7);
                DateTime previousWeekEnd = startingDate.AddDays(-1);

                var thongke = new ThongKeTheoChuyenKhoa();
                var lsResult = new List<TB_CHITIETDKKHAM>();
                lsResult = _unitOfWork.DKKhamRepository.Filter(c => DbFunctions.TruncateTime(c.NGAYDANGKY) <= previousWeekEnd && DbFunctions.TruncateTime(c.NGAYDANGKY) >= previousWeekStart).ToList();
                if (lsResult != null)
                {
                    thongke.TongSo = lsResult.Count();
                    thongke.KhoaNoi = lsResult.Where(x => x.MAPK == 1).Count();
                    thongke.KhoaNgoai = lsResult.Where(x => x.MAPK == 2).Count();
                    thongke.KhoaDongY = lsResult.Where(x => x.MAPK == 3).Count();
                    thongke.KhoaUngBieu = lsResult.Where(x => x.MAPK == 4).Count();
                    thongke.KhoaKhac = lsResult.Where(x => x.MAPK == 5).Count();
                }
                return thongke;
            }
            catch (Exception ex)
            {
                OsLog.Error("ConfigWebServices --> getThongKe ", ex);
                return new ThongKeTheoChuyenKhoa();
            }
        }
    }

}
