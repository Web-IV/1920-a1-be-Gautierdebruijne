using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingAPI.Models
{
    public interface ICustomerRepository
    {
        Customer GetBy(string name);
        void Add(Customer customer);
        void Delete(Customer customer);
        void SaveChanges();
    }
}
