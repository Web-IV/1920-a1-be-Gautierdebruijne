using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe_API.Data
{
    public class MeetingDataInitializer
    {
        private readonly MeetingContext _context;

        public MeetingDataInitializer(MeetingContext context)
        {
            _context = context;
        }

        public void InitializeData()
        {
            _context.Database.EnsureDeleted();
            if (_context.Database.EnsureCreated())
            {
                //seeding in context
            }
        }
    }
}
