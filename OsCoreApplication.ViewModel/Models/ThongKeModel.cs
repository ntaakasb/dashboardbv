using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.ViewModel.Models
{
    public class ThongKeModel
    {
        public long TongSoDangKy { get; set; }
        public long ChoKham { get; set; }
        public long DangKham { get; set; }
        public long DaKham { get; set; }
    }

    public class ThongKeTheoChuyenKhoa
    {
        public long TongSo { get; set; }
        public long KhoaNoi { get; set; }
        public long KhoaNgoai { get; set; }
        public long KhoaDongY { get; set; }
        public long KhoaUngBieu { get; set; }
        public long KhoaKhac { get; set; }
    }
}
