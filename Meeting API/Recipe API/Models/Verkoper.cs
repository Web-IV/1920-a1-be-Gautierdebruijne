using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingAPI.Models
{
    public class Verkoper
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Title { get; set; }
        #endregion

        #region Constructors
        public Verkoper()
        {
            Title = "verkoper";
        }

        public Verkoper(string name, string title)
        {
            Name = name;
            Title = title;
        }
        #endregion
    }
}
