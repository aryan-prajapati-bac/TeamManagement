using Microsoft.AspNetCore.Mvc;
using TeamManagement.Models;
using TeamManagement.Services;

namespace TeamDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;
        public LoginController(LoginService service)
        {
            _loginService = service;
        }
        [HttpGet("signin")]
        public IActionResult Login([FromBody] Login obj)
        {
            if (obj == null) 
                return BadRequest("Enter details");

            string token= _loginService.Authenticate(obj);
           
            if (token.Equals("Failed")) 
                return NotFound("User not Found");

            if(!token.Equals(""))  
                Response.Headers.Add("Aunthentication-Token",token); 
            else  
                Response.Headers.Add("No-token", ""); 

            
            return Ok(_loginService.ResponseObj(obj));

        }

    }
}
