using OsCoreApplication.DataLayer.EFModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsCoreApplication.Services
{
    public interface IImagesUpload
    {
        List<ImagesUpload> GetListImageUpload(int qty);
        bool AddImagesUpload(ImagesUpload imagesUpload);
    }
}
