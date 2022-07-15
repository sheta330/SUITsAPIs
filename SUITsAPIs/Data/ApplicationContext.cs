using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SUITsAPIs.Models;
using SUITsAPIs.Models.Core_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SUITsAPIs.Data
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }
        public DbSet<categorie> categories { get; set; }
        public DbSet<Sex> Sexs { get; set; }
        public DbSet<Discound> Discounds { get; set; }
        public DbSet<productimgs> productimgs { get; set; }
        public DbSet<Proudect> Proudects { get; set; }
        public DbSet<offer> offers { get; set; }
        public DbSet<ProudectCategories> ProudectCategories { get; set; }
        public DbSet<sub_category> sub_category { get; set; }
        public DbSet<sub_category_prodacts> sub_category_prodacts { get; set; }
    }
}
