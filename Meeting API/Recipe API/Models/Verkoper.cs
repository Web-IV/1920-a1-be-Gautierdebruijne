using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe_API.Models
{
    public class Verkoper
    {
        #region Properties
        public int Id { get; set; }

        public string Name { get; set; }

        public string Title { get; set; }
        #endregion

        #region Constructors
        public Verkoper(string name, string title)
        {
            Name = name;
            Title = title;
        }
        #endregion
    }
}
