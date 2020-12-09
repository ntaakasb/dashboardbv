using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
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


namespace OsCoreApplication.Services
{
    public partial class ConfigWebServices : IConfigWeb
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICached _cached;
        public ConfigWebServices(IUnitOfWork unitOfWork, IMapper mapper, ICached cached)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cached = cached;
        }
        public List<ConfigWebModels> GetListConfigWebByType(string type)
        {
            try
            {
                var lstData = _cached.Get<List<ConfigWebModels>>(KeyCache.Key_ConfigWeb);
                if (lstData != null) return lstData;
                var data = new List<ConfigWeb>();
                if(type != "")
                    data = _unitOfWork.ConfigWebRepository.Filter(c => c.Type == type).ToList();
                else
                    data = _unitOfWork.ConfigWebRepository.GetAllData().ToList();
                lstData = _mapper.Map<List<ConfigWeb>, List<ConfigWebModels>>(data);
                _cached.Set(CacheMode.Add, KeyCache.Key_ConfigWeb, lstData, new TimeSpan(1, 0, 0, 0));
                return lstData;
            }
            catch (Exception ex)
            {
                OsLog.Error("ConfigWebServices --> GetListConfigWebByType ", ex);
                return new List<ConfigWebModels>();
            }
        }


        public bool UpdadeConfig(ConfigWeb c)
        {
            try
            {
                ConfigWeb config = _unitOfWork.ConfigWebRepository.FindById(c.id);
                if (config == null)
                    return false;

                config.ValueConfig = c.ValueConfig;
                config.UserUpdated = c.UserUpdated;
                _unitOfWork.ConfigWebRepository.Update(config);
                _unitOfWork.SaveChanges();
                return true;
            }
            catch (DbEntityValidationException ex)
            {
                foreach (var eve in ex.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
               
            }
            return false;
        }



    }

    public partial class ConfigWebServices
    {


    }
}
