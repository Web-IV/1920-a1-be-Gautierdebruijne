using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Recipe_API.Models;

namespace Recipe_API.Controllers
{
    [Route("api/meetings")]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingsController(IMeetingRepository context)
        {
            _meetingRepository = context;
        }

        [HttpGet]
        public IEnumerable<Meeting> GetMeetings()
        {
            return _meetingRepository.GetAll().OrderBy(m => m.Name);
        }

        [HttpGet("{id}")]
        public ActionResult<Meeting> GetMeeting(int id)
        {
            Meeting meeting = _meetingRepository.GetBy(id);
            if (meeting == null)
                return NotFound();

            return meeting;
        }
    }
}