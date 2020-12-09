using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.ViewModel.Models
{
    public class ContactModels
    {
        public long id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string ContentMsg { get; set; }
        public Nullable<System.DateTime> DateContact { get; set; }
    }
}
