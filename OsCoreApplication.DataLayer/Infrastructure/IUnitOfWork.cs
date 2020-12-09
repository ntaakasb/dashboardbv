using System;
using OsCoreApplication.DataLayer.EFModel;

namespace OsCoreApplication.DataLayer.Infrastructure
{
    public interface IUnitOfWork : IDisposable
    {
        EFDBEntities DbContext { get; }

        void SaveChanges();

        IRepository<Banner> BannerRepository { get; }
        IRepository<ConfigWeb> ConfigWebRepository { get; }
        IRepository<Project> ProjectRepository { get; }
        IRepository<NewsCategory> NewsCategoryRepository { get; }
        IRepository<News> NewsRepository { get; } 
        IRepository<Contact> ContactRepository { get;}
        IRepository<Account> AccountRepository { get; }
        IRepository<ProjectImage> ProjectImagesRepository { get; }
        IRepository<ImagesUpload> ImagesUploadRepository { get; }

        //  IRepository<OS_Schedule> ScheduleRepository { get; }
    }
}
