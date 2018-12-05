using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace TouristAgency.Models
{
    public class AgencyContext : DbContext
    {
        public AgencyContext(DbContextOptions<AgencyContext> options) : base(options) { }

        public DbSet<Client> Clients { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Service> _Services { get; set; }
        public DbSet<ServiceList> ServiceList { get; set; }
        public DbSet<TypeRest> TypeRests { get; set; }
        public DbSet<Voucher> Vouchers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<ServiceList>()
            //    .HasKey(t => new { t.VoucherID, t.ServiceID });

            modelBuilder.Entity<ServiceList>()
                .HasOne(sc => sc.Voucher)
                .WithMany(s => s.ServiceList)
                .HasForeignKey(sc => sc.VoucherID);

            modelBuilder.Entity<ServiceList>()
                .HasOne(sc => sc.Service)
                .WithMany(c => c.ServiceList)
                .HasForeignKey(sc => sc.ServiceID);
        }
    }
}
