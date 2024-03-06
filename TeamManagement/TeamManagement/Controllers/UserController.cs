using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamManagement.Interfaces;
using TeamManagement.Models;

namespace TeamManagement.Controllers
{   
    public class UserController : BaseController
    {
        #region Service
        private readonly IUserService _userService;
        #endregion

        #region DI
        public UserController(IUserService userservice)
        {
            _userService = userservice;
            
        }
        #endregion

        #region APIs

        [HttpPost("register")]
        public IActionResult Register([FromBody] User user)
        {

            if (user == null) 
                return BadRequest("Provide data");

           
            return Ok(_userService.Register(user));
            
        }

        [Authorize]
        [HttpPut("change/pwd")]
        public IActionResult ChangePwd([FromBody] Login login)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "UserId");
            int Id = 0;
            if (int.TryParse(userIdClaim.Value, out int userId))
            {
                Id = userId;
            }
            return Ok(_userService.ChangePwd(login,Id));
        }

        #endregion
    }
}
