﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingAPI.DTO
{
    public class MeetingDTO
    {
        [Required]
        public string Name { get; set; }

        public DateTime Date { get; set; }

        public IList<VerkoperDTO> Verkopers { get; set; }

    }
}
