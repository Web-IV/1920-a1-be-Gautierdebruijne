using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe_API.DTO
{
    public class MeetingDTO
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public DateTime Planned { get; set; }

        public IList<VerkoperDTO> Verkopers { get; set; }

    }
}
