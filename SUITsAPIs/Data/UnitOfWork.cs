using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SUITsAPIs.Core.IRepositorys;
using SUITsAPIs.Core.IRepositorys.IConfigration;
using SUITsAPIs.Core.Repositorys;
using SUITsAPIs.Helper;
using SUITsAPIs.Models;
using SUITsAPIs.Models.Core_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Data
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        protected ApplicationContext _context;
        private readonly UserManager<ApplicationUser> manager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWT _jwt;
        public UnitOfWork(ApplicationContext context, UserManager<ApplicationUser> manager,
            RoleManager<IdentityRole> roleManager, IOptions<JWT> jwt)
        {
            _context = context;
            this.manager = manager;
            _roleManager = roleManager;
            _jwt = jwt.Value; 
            categories = new GenericRepository<categorie>(_context,this.manager, _roleManager, jwt);
            Sexs = new GenericRepository<Sex>(_context, this.manager, _roleManager, jwt);
            productimgs = new GenericRepository<productimgs>(_context, this.manager, _roleManager, jwt);
            Proudects = new GenericRepository<Proudect>(_context, this.manager, _roleManager, jwt);
            offers = new GenericRepository<offer>(_context, this.manager, _roleManager, jwt);
            ProudectCategories = new GenericRepository<ProudectCategories>(_context, this.manager, _roleManager, jwt);
            sub_category = new GenericRepository<sub_category>(_context, this.manager, _roleManager, jwt);
            sub_category_prodacts = new GenericRepository<sub_category_prodacts>(_context, this.manager, _roleManager, jwt);
            Discounds = new GenericRepository<Discound>(_context, this.manager, _roleManager, jwt);
            Users = new GenericRepository<IdentityUser>(_context, this.manager, _roleManager, jwt);
            AuthService = new AuthServiceRepository(_context, this.manager, _roleManager, jwt);
        }

        public IGenericRepository<categorie> categories { get; private set; }
        public IGenericRepository<IdentityUser> Users { get; private set; }
        public IAuthServiceRepository AuthService  { get; private set; }
        public IGenericRepository<Sex> Sexs { get; private set; }
        public IGenericRepository<Discound> Discounds { get; private set; }
        public IGenericRepository<productimgs> productimgs { get; private set; }
        public IGenericRepository<Proudect> Proudects { get; private set; }
        public IGenericRepository<offer> offers { get; private set; }
        public IGenericRepository<ProudectCategories> ProudectCategories { get; private set; }
        public IGenericRepository<sub_category> sub_category { get; private set; }
        public IGenericRepository<sub_category_prodacts> sub_category_prodacts { get; private set; }
        public int Save()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }


    }
}
