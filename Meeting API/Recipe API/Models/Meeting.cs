using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe_API.Models
{
    public class Meeting
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public DateTime Created { get; set; }

        [Required]
        public DateTime Planned { get; set; }

        [Required]
        public ICollection<Verkoper> Verkopers { get; private set; }
        #endregion

        #region Constructors
        public Meeting()
        {
            Verkopers = new List<Verkoper>();
            Created = DateTime.Now;
            Planned = new DateTime();
        }

        public Meeting(string name) : this()
        {
            Name = name;
        }
        #endregion

        #region Methods
        public void addVerkoper(Verkoper verkoper) => Verkopers.Add(verkoper);

        public Verkoper GetVerkoper(int id) => Verkopers.SingleOrDefault(i => i.Id == id);
        #endregion
    }
}
