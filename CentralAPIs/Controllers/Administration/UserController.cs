using CentralAPIs.IRepo;
using CentralModels.Administration;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CentralAPIs.Controllers.Administration
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _loginRepo;

        public UserController(IUserRepo loginRepo)
        {
            _loginRepo = loginRepo;
        }

        [HttpGet]
        public IEnumerable<UserDetails> Get()
        {
            return _loginRepo.getAllUserList();
        }
        [HttpGet]
        [Route("/GetDetails")]
        public IEnumerable<UserDetailsView> GetDetails()
        {
            return _loginRepo.getAllUserListView();
        }
    }
}
