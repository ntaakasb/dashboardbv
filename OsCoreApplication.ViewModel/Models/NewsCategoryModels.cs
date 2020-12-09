using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.ViewModel.Models
{
    public class NewsCategoryModels
    {
        public long id { get; set; }
        public string NewsCategoryName { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public Nullable<long> UserCreated { get; set; }
        public Nullable<long> UserUpdated { get; set; }
    }
}
