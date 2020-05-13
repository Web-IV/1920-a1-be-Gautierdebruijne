using Microsoft.EntityFrameworkCore;
using MeetingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MeetingAPI.Data
{
    public class MeetingContext : IdentityDbContext
    {
        public MeetingContext(DbContextOptions<MeetingContext> options) : base(options){}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Meeting>().HasMany(m => m.Verkopers).WithOne().IsRequired().HasForeignKey("MeetingId");
            builder.Entity<Meeting>().Property(m => m.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Meeting>().Property(m => m.Planned).IsRequired();

            builder.Entity<Verkoper>().Property(v => v.Name).IsRequired().HasMaxLength(50);
            builder.Entity<Verkoper>().Property(v => v.Title).HasMaxLength(50);

            builder.Entity<Customer>().Property(c => c.Email).IsRequired().HasMaxLength(100);
            builder.Entity<Customer>().Property(c => c.FirstName).HasMaxLength(50);
            builder.Entity<Customer>().Property(c => c.LastName).HasMaxLength(50);
                
            builder.Entity<Meeting>().HasData(
                new Meeting { Id = 1, Name = "Pascale Engels", Planned = DateTime.Now.AddDays(7) },
                new Meeting { Id = 2, Name = "Gautier de Bruijne", Planned = DateTime.Now.AddDays(9) }
                );

            builder.Entity<Verkoper>().HasData(
                new { Id = 1, Name = "Jo de Bruijne", Title = "Verkoopsverantwoordelijke", MeetingId = 1 },
                new { Id = 2, Name = "Stefaan Durwael", Title = "", MeetingId = 1}
                );
        }

        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
