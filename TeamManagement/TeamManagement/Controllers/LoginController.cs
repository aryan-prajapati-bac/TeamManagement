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
        public async Task<IActionResult> Login([FromBody] Login obj)
        {
            if (obj == null) 
                return BadRequest("Enter details");

            string token= await _loginService.Authenticate(obj);
           
            if (token.Equals("Failed")) 
                return NotFound("User not Found");
            if (token.Equals("Invalid Credentials")) return Ok("Invalid Credentials");
            if(!token.Equals(""))  
                Response.Headers.Add("Aunthentication-Token",token); 
            else  
                Response.Headers.Add("No-token", ""); 

            
            return Ok(await _loginService.ResponseObj(obj));

        }
        #endregion

    }
}
