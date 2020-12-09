using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.ViewModel.Models
{
    [Serializable]
    public class BannerModels
    {
        public long id { get; set; }
        public string ImageUrl { get; set; }
        public Nullable<bool> IsShow { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<System.DateTime> DateUpdated { get; set; }
        public Nullable<long> UserCreated { get; set; }
        public Nullable<long> UserUpdated { get; set; }
    }
}
