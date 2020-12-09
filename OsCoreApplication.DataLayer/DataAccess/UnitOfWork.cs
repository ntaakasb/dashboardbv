using System;
using OsCoreApplication.DataLayer.EFModel;
using OsCoreApplication.DataLayer.Infrastructure;

namespace OsCoreApplication.DataLayer.DataAccess
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;

        private EFDBEntities _dbContext;
        public EFDBEntities DbContext => _dbContext ?? (_dbContext = new EFDBEntities());

        public UnitOfWork()
        {
            _dbContext = new EFDBEntities();
        }

        private IRepository<Banner> _bannerRepository;
        private IRepository<ConfigWeb> _configWebRepository;
        private IRepository<Project> _projectRepository;
        private IRepository<News> _newRepository;
        private IRepository<NewsCategory> _newCategoryRepository;
        private IRepository<Contact> _contactRepository;
        private IRepository<Account> _accountRepository;
        private IRepository<ProjectImage> _projectImagesRepository;
        private IRepository<ImagesUpload> _imagesUploadRepository;
        // private IRepository<OS_Schedule> _scheduleRepository;

        public IRepository<Banner> BannerRepository => _bannerRepository ?? (_bannerRepository = new Repository<Banner>(_dbContext));
        public IRepository<ConfigWeb> ConfigWebRepository => _configWebRepository ?? (_configWebRepository = new Repository<ConfigWeb>(_dbContext));
        public IRepository<Project> ProjectRepository => _projectRepository ?? (_projectRepository = new Repository<Project>(_dbContext));
        public IRepository<News> NewsRepository => _newRepository ?? (_newRepository = new Repository<News>(_dbContext));
        public IRepository<NewsCategory> NewsCategoryRepository => _newCategoryRepository ?? (_newCategoryRepository = new Repository<NewsCategory>(_dbContext));
        public IRepository<Contact> ContactRepository => _contactRepository ?? (_contactRepository = new Repository<Contact>(_dbContext));
        public IRepository<Account> AccountRepository => _accountRepository ?? (_accountRepository = new Repository<Account>(_dbContext));
        public IRepository<ProjectImage> ProjectImagesRepository => _projectImagesRepository ?? (_projectImagesRepository = new Repository<ProjectImage>(_dbContext));
        public IRepository<ImagesUpload> ImagesUploadRepository => _imagesUploadRepository ?? (_imagesUploadRepository = new Repository<ImagesUpload>(_dbContext));

        // public IRepository<OS_Schedule> ScheduleRepository => _scheduleRepository ?? (_scheduleRepository = new Repository<OS_Schedule>(_dbContext));

        public void SaveChanges()
        {
            CheckIsDisposed();
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                    _dbContext?.Dispose();
            }
            _disposed = true;
        }

        private void CheckIsDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(GetType().Name);
            }
        }
    }
}
