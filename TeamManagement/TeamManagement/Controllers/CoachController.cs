using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamManagement.Controllers;
using TeamManagement.Interfaces;


namespace TeamDemo.Controllers
{
    [Authorize(Roles ="coach")]    
    public class CoachController : BaseController
    {
        #region Service
        private readonly ICoachService _coachService;
        #endregion

        #region DI
        public CoachController(ICoachService _coachServicecs)
        {
           
            _coachService = _coachServicecs;

        }
        #endregion

        #region APIs

        [HttpPost("{id:int}/addUser")]
        public async Task<IActionResult> AddUser([FromBody] string userMail, [FromRoute] int id)
        {
            if(userMail == null) 
                return Ok("Provide player data");       
            
            
            return Ok(await _coachService.AddUser(userMail, id));

        }

        [HttpPut("{id:int}/updateCaptain")]
        public async Task<IActionResult> UpdateCaptain([FromBody]string captainEmail, [FromRoute] int id)
        {
            if (captainEmail == null)  
                return Ok("Provide player data"); 
            
            
            return Ok(await _coachService.MakeCaptain(captainEmail, id));

        }

        #endregion
    }
}
