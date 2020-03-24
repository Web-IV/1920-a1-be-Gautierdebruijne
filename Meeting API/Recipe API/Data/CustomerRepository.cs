using MeetingAPI.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingAPI.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly MeetingContext _context;
        private readonly DbSet<Customer> _customers;

        public CustomerRepository(MeetingContext context)
        {
            _context = context;
            _customers = context.Customers;
        }

        public void Add(Customer customer)
        {
            _customers.Add(customer);
        }

        public void Delete(Customer customer)
        {
            _customers.Remove(customer);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
