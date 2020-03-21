using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Recipe_API.DTO;
using Recipe_API.Models;

namespace Recipe_API.Controllers
{
    [Route("api/meetings")]
    [Produces("application/json")]
    [ApiConventionType(typeof(DefaultApiConventions))]
    [ApiController]
    public class MeetingsController : ControllerBase
    {
        private readonly IMeetingRepository _meetingRepository;

        public MeetingsController(IMeetingRepository context)
        {
            _meetingRepository = context;
        }

        /// <summary>
        /// Geeft alle geplande meetings
        /// </summary>
        /// <returns>Geplande meetings;</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IEnumerable<Meeting> GetMeetings()
        {
            return _meetingRepository.GetAll().OrderBy(m => m.Name);
        }

        /// <summary>
        /// Geeft de meeting met gekozen "id"
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Meeting met gekozen id; </returns>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<Meeting> GetMeeting(int id)
        {
            Meeting meeting = _meetingRepository.GetBy(id);
            if (meeting == null)
                return NotFound();

            return meeting;
        }

        /// <summary>
        /// Geeft de verkoper met het gekozen id van een bepaalde meeting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="verkoperId"></param>
        /// <returns>Verkoper met gekozen id van de bepaalde meeting; </returns>
        [HttpGet("{id}/verkopers/{verkoperId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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

        /// <summary>
        /// Maakt een nieuwe meeting aan
        /// </summary>
        /// <param name="meeting"></param>
        /// <returns>Nieuwe meeting; </returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Meeting> PostMeeting(MeetingDTO meeting)
        {
            Meeting meetingToCreate = new Meeting() { Name = meeting.Name, Planned = meeting.Planned };
            foreach (var i in meeting.Verkopers)
                meetingToCreate.AddVerkoper(new Verkoper(i.Name, i.Title));
            
            _meetingRepository.Add(meetingToCreate);
            _meetingRepository.SaveChanges();

            return CreatedAtAction(nameof(GetMeeting), new { id = meetingToCreate.Id }, meetingToCreate);
        }

        /// <summary>
        /// Voegt een verkoper toe aan een bepaalde meeting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="verkoper"></param>
        /// <returns>Toegevoegde verkoper aan de bepaalde meeting; </returns>
        [HttpPost("{id}/verkopers")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Verkoper> PostVerkoper(int id, VerkoperDTO verkoper)
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

        /// <summary>
        /// Wijzig een bestaande meeting
        /// </summary>
        /// <param name="id"></param>
        /// <param name="meeting"></param>
        /// <returns>Gewijzigde meeting; </returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Verwijderd een bestaande meeting
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Response code; </returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
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