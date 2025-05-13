using CentralAPIs.IRepo;
using CentralModels.Administration;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CentralAPIs.Controllers.Administration
{
    

    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginRepo _loginRepo;

        public LoginController(ILoginRepo loginRepo)
        {
            _loginRepo = loginRepo;
        }
    
        [HttpPost]
        public IActionResult Post(LoginModel loginData)
        {
            try
            {
                LoginResponce token = _loginRepo.loginValidation(loginData.UserId, loginData.Password, loginData.projectName);
                return Ok(token);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
