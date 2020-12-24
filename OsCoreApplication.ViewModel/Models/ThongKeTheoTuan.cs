using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.ViewModel.Models
{
    [Serializable]
    public class ThongKeTheoTuan
    {
        public int NgayDuLieu { get; set; }
        public int TuanDuLieu { get; set; }
        public int SoLuong { get; set; }
    }
}
