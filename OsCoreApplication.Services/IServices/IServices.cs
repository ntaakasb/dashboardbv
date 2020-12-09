using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.Services
{
    public interface IServices
    {
        ThongKeModel getThongKeTheoTuan(DateTime? fromDate, DateTime? toDate);
        List<int> getDetailBenhNhanTheoTuan(DateTime? fromDate, DateTime? toDate);
        ThongKeTheoChuyenKhoa getThongKeTheoKhoa(DateTime? fromDate, DateTime? toDate);
    }
}
