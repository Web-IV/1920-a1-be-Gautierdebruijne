using Microsoft.EntityFrameworkCore;
using MeetingAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MeetingAPI.Data
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly MeetingContext _context;
        private readonly DbSet<Meeting> _meetings;

        public MeetingRepository(MeetingContext context)
        {
            _context = context;
            _meetings = context.Meetings;
        }

        public void Add(Meeting meeting)
        {
            _meetings.Add(meeting);
        }

        public void Delete(Meeting meeting)
        {
            _meetings.Remove(meeting);
        }

        public IEnumerable<Meeting> GetAll()
        {
            return _meetings.Include(m => m.Verkopers).ToList();
        }   

        public Meeting GetBy(int id)
        {
            return _meetings.Include(m => m.Verkopers).SingleOrDefault(m => m.Id == id);
        }

        public IEnumerable<Meeting> GetBy(string name = null, string verkoperName = null)
        {
            var meetings = _meetings.Include(m => m.Verkopers).AsQueryable();
            if (!string.IsNullOrEmpty(name))
                meetings = meetings.Where(m => m.Name == name);
            if (!string.IsNullOrEmpty(verkoperName))
                meetings = meetings.Where(m => m.Verkopers.Any(m => m.Name == verkoperName));

            return meetings.OrderBy(m => m.Planned);
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        public bool TryGetMeeting(int id, out Meeting meeting)
        {
            meeting = _context.Meetings.Include(m => m.Verkopers).FirstOrDefault(m => m.Id == id);
            return meeting != null;
        }

        public void Update(Meeting meeting)
        {
            _context.Update(meeting);
        }
    }
}
