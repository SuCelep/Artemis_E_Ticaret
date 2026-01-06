using Artemis.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Artemis.DAL.Context
{
    public class SQLContext: DbContext
    {
        public SQLContext(DbContextOptions<SQLContext> opt):base(opt) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany(x => x.SubCategories).HasForeignKey(x => x.ParentID);

            modelBuilder.Entity<Admin>().HasData(new Admin
            {
                ID = 1,
                NameSurname= " Çağla Celep",
                UserName="cagla",
                LastLoginIP= "",
                LastLoginDate= DateTime.Now,
                Password= "202cb962ac59075b964b07152d234b70"
            });
        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<Slide> Slide { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Brand> Brand { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductPicture> ProductPicture { get; set; }

        public DbSet<Order> Order { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }

    }
}
