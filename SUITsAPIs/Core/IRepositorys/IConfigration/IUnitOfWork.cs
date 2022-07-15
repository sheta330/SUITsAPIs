using Microsoft.AspNetCore.Identity;
using SUITsAPIs.Models.Core_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Core.IRepositorys.IConfigration
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<categorie> categories { get; }
        public IGenericRepository<IdentityUser> Users { get; }
        public IAuthServiceRepository AuthService { get; }
        public IGenericRepository<Sex> Sexs { get; }
        public IGenericRepository<Discound> Discounds { get; }
        public IGenericRepository<productimgs> productimgs { get; }
        public IGenericRepository<Proudect> Proudects { get; }
        public IGenericRepository<offer> offers { get; }
        public IGenericRepository<ProudectCategories> ProudectCategories { get; }
        public IGenericRepository<sub_category> sub_category { get; }
        public IGenericRepository<sub_category_prodacts> sub_category_prodacts { get; }
        int Save();
    }
}
