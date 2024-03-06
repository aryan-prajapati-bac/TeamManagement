using Microsoft.AspNetCore.Mvc;
using TeamManagement.Controllers;
using TeamManagement.Models;
using TeamManagement.Services;

namespace TeamDemo.Controllers
{
    public class LoginController : BaseController
    {
        #region Service
        private readonly LoginService _loginService;
        #endregion

        #region DI
        public LoginController(LoginService service)
        {
            _loginService = service;
        }
        #endregion

        #region APIs
        [HttpGet("signin")]
        public IActionResult Login([FromBody] Login obj)
        {
            if (obj == null) 
                return BadRequest("Enter details");

            string token= _loginService.Authenticate(obj);
           
            if (token.Equals("Failed")) 
                return NotFound("User not Found");
            if (token.Equals("Invalid Credentials")) return Ok("Invalid Credentials");
            if(!token.Equals(""))  
                Response.Headers.Add("Aunthentication-Token",token); 
            else  
                Response.Headers.Add("No-token", ""); 

            
            return Ok(_loginService.ResponseObj(obj));

        }
        #endregion

    }
}
