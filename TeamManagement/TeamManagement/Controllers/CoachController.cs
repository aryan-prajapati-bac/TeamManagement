using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamManagement.Interfaces;


namespace TeamDemo.Controllers
{
    [Authorize(Roles ="coach")]
    [Route("api/[controller]/{id:int}")]
    [ApiController]
    public class CoachController : ControllerBase
    {
      
        private readonly ICoachService _coachService;
        public CoachController(ICoachService _coachServicecs)
        {
           
            _coachService = _coachServicecs;

        }

        [HttpPost("addUser")]
        public IActionResult AddUser([FromBody] string userMail, [FromRoute] int id)
        {
            if(userMail == null) return Ok("Provide player data");       
            return Ok(_coachService.AddUser(userMail, id));

        }

        [HttpPut("updateCaptain")]
        public IActionResult UpdateCaptain([FromBody]string captainEmail, [FromRoute] int id) {
            if (captainEmail == null)  return Ok("Provide player data"); 
            return Ok(_coachService.MakeCaptain(captainEmail, id));

        }
    }
}
