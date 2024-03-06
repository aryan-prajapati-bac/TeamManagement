using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamManagement.Interfaces;



namespace TeamDemo.Controllers
{
    [Authorize(Roles ="captain")]
    
    [Route("api/[controller]")]
    [ApiController]
    public class CaptainController : ControllerBase
    {
        private readonly ICaptainService _captainService;
        public CaptainController(ICaptainService captainServices)
        {
            _captainService= captainServices;
        }


       
        [HttpGet("selectPlayer")]
        public IActionResult AddPlayer([FromBody] string playerEmail)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            int Id = 0;
            if (int.TryParse(userIdClaim.Value, out int userId))
            {
                Id = userId;
            }

            string added = _captainService.SelectPlayer(playerEmail, Id);
            return Ok(added);

        }
    }
}
