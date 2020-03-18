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
    }
}