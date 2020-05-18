using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingAPI.Models
{
    public class Meeting
    {
        #region Properties
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        public DateTime Date { get; set; }

        public ICollection<Verkoper> Verkopers { get; private set; }
        #endregion

        #region Constructors
        public Meeting()
        {
            Verkopers = new List<Verkoper>();
            Created = DateTime.Now;
        }

        public Meeting(string name, DateTime planned) : this()
        {
            Name = name;
            Date = planned;
        }
        #endregion

        #region Methods
        public void AddVerkoper(Verkoper verkoper) => Verkopers.Add(verkoper);

        public Verkoper GetVerkoper(int id) => Verkopers.SingleOrDefault(i => i.Id == id);
        #endregion
    }
}
