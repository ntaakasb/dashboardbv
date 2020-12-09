using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.ViewModel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.Services
{
    public interface IContact
    {
        bool InsertContact(Contact c);
        List<ContactModels> GetAllContact(int pageSize, int pageNumber, out int totalRows);
        bool DeleteContact(int id);
    }
}
