using MeetingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingAPI.DTO
{
    public class CustomerDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        public CustomerDTO() { }

        public CustomerDTO(Customer customer) : this()
        {
            FirstName = customer.FirstName;
            LastName = customer.LastName;
            Email = customer.Email;
        }
    }
}
