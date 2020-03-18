using Recipe_API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Recipe_API.Data
{
    public class MeetingRepository : IMeetingRepository
    {
        public void Add(Meeting meeting)
        {
            throw new NotImplementedException();
        }

        public void Delete(Meeting meeting)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Meeting> GetAll()
        {
            throw new NotImplementedException();
        }

        public Meeting GetBy(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Meeting> GetBy(string name = null, string verkoperName = null)
        {
            throw new NotImplementedException();
        }

        public void SaveChanges()
        {
            throw new NotImplementedException();
        }

        public bool TryGetMeeting(int id, out Meeting meeting)
        {
            throw new NotImplementedException();
        }

        public void Update(Meeting meeting)
        {
            throw new NotImplementedException();
        }
    }
}
