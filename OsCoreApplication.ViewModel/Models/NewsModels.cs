using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.ViewModel.Models
{
    public class NewsModels
    {
        public long id { get; set; }
        public string NewsTitle { get; set; }
        public string ShortDescription { get; set; }
        public string NewsContent { get; set; }
        public string Thumb { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdate { get; set; }
        public Nullable<long> UserCreated { get; set; }
        public Nullable<long> UserUpdate { get; set; }
        public Nullable<bool> IsShow { get; set; }
        public Nullable<long> idNewsCategory { get; set; }
        public string NewsCategoryName { get; set; }

    }
}
