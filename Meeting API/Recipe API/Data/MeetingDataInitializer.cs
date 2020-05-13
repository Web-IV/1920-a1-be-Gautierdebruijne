using MeetingAPI.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingAPI.Data
{
    public class MeetingDataInitializer
    {
        private readonly MeetingContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public MeetingDataInitializer(MeetingContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task InitializeData()
        {
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                //seeding in context

                Customer customer = new Customer { Email = "gautier@gmail.com", FirstName = "Gautier", LastName = "de Bruijne" };
                _context.Customers.Add(customer);

                await CreateUser(customer.Email, "Meeting3.");
                _context.SaveChanges();
            }
        }

        private async Task CreateUser(string email, string password)
        {
            var user = new IdentityUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }
    }
}
