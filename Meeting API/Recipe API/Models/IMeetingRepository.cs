using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingAPI.Models
{
    public interface IMeetingRepository
    {
        Meeting GetBy(int id);
        bool TryGetMeeting(int id, out Meeting meeting);
        IEnumerable<Meeting> GetAll();
        IEnumerable<Meeting> GetBy(string name = null, string verkoperName = null);
        void Add(Meeting meeting);
        void Delete(Meeting meeting);
        void Update(Meeting meeting);
        void SaveChanges();
    }
}
