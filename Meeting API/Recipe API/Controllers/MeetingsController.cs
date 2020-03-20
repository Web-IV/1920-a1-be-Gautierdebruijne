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

        [HttpGet("{id}/verkopers/{verkoperId}")]
        public ActionResult<Verkoper> GetVerkoper(int id, int verkoperId)
        {
            if(!_meetingRepository.TryGetMeeting(id, out var meeting))
            {
                return NotFound();
            }

            Verkoper verkoper = meeting.GetVerkoper(verkoperId);
            if(verkoper == null)
            {
                return NotFound();
            }

            return verkoper;
        }

        [HttpPost] 
        public ActionResult<Meeting> PostMeeting(Meeting meeting)
        {
            _meetingRepository.Add(meeting);
            _meetingRepository.SaveChanges();

            return CreatedAtAction(nameof(GetMeeting),
                new { id = meeting.Id }, meeting);
        }

        [HttpPost("{id}/verkopers")]
        public ActionResult<Verkoper> PostVerkoper(int id, Verkoper verkoper)
        {
            if(!_meetingRepository.TryGetMeeting(id, out var meeting))
            {
                return NotFound();
            }

            var verkoperToCreate = new Verkoper(verkoper.Name, verkoper.Title);
            meeting.AddVerkoper(verkoperToCreate);
            _meetingRepository.SaveChanges();

            return CreatedAtAction("GetVerkoper", new { id = meeting.Id, verkoperId = verkoperToCreate.Id }, verkoperToCreate);
        }

        [HttpPut("{id}")]
        public IActionResult PutMeeting(int id, Meeting meeting)
        {
            if(id != meeting.Id)
            {
                return BadRequest();
            }

            _meetingRepository.Update(meeting);
            _meetingRepository.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMeeting(int id)
        {
            Meeting meeting = _meetingRepository.GetBy(id);

            if(meeting == null)
            {
                return NotFound();
            }

            _meetingRepository.Delete(meeting);
            _meetingRepository.SaveChanges();

            return NoContent();
        }


    }   
}