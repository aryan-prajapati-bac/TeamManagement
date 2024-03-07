using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamManagement.Controllers;
using TeamManagement.Interfaces;



namespace TeamDemo.Controllers
{
    [Authorize(Roles ="captain")]
    public class CaptainController : BaseController
    {
        #region Service
        private readonly ICaptainService _captainService;
        #endregion

        #region DI
        public CaptainController(ICaptainService captainServices)
        {
            _captainService= captainServices;
        }
        #endregion

        #region APIs
        [HttpGet("selectPlayer")]
        public async Task<IActionResult> AddPlayer([FromBody] string playerEmail)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            int Id = 0;
            if (int.TryParse(userIdClaim.Value, out int userId))
            {
                Id = userId;
            }

            string added =  Convert.ToString(await _captainService.SelectPlayer(playerEmail, Id));
            return Ok(added);

        }
        #endregion
    }
}
