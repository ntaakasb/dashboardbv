using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using OsCoreApplication.Common;
using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.DataLayer.Infrastructure;
using OsCoreApplication.Libraries.Memcached;
using OsCoreApplication.Logger;
using OsCoreApplication.ViewModel.Models;

namespace OsCoreApplication.Services.Services
{
    public partial class ContactServices : IContact
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICached _cached;

        public ContactServices(IUnitOfWork unitOfWork, IMapper mapper, ICached cached)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cached = cached;
        }
        public bool InsertContact(Contact c)
        {
            _unitOfWork.ContactRepository.Insert(c);
            _unitOfWork.SaveChanges();
            return true;
        }

        public List<ContactModels> GetAllContact(int pageSize, int pageNumber, out int totalRows)
        {
            totalRows = 0;
            try
            {
                var lstData = _cached.Get<List<ContactModels>>(KeyCache.Key_Contact);
                if (lstData != null) return lstData;
                totalRows = _unitOfWork.ContactRepository.GetAllData().Count();
                var data = _unitOfWork.ContactRepository.GetAllData().OrderByDescending(p => p.id).Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToList();
                lstData = _mapper.Map<List<Contact>, List<ContactModels>>(data);
                _cached.Set(CacheMode.Add, KeyCache.Key_Contact, lstData, new TimeSpan(1, 0, 0, 0));
                return lstData;
            }
            catch (Exception ex)
            {
                OsLog.Error("ContactServices --> GetAllContact ", ex);
                return new List<ContactModels>();
            }
        }



        public bool DeleteContact(int id)
        {
            Contact _contact = new Contact();
            _contact.id = id;
            try
            {
                Contact contact = _unitOfWork.ContactRepository.FindById(_contact.id);
                if (contact == null)
                    return false;
                _unitOfWork.ContactRepository.Delete(contact);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch { }
            return false;
        }






    }

    public partial class ContactServices
    {

    }


}
