using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Enterprise.App.ViewModels;

namespace Enterprise.App.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Enterprise.App.ViewModels.SupplierViewModel> SupplierViewModel { get; set; }
        public DbSet<Enterprise.App.ViewModels.ProductViewModel> ProductViewModel { get; set; }
    }
}
