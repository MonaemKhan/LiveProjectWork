using CentralAPIs.CustomeActionFilter;
using CentralAPIs.IRepo;
using CentralModels.Administration;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CentralAPIs.Controllers.Administration
{
    [Route("apiV1/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepo _userRepo;

        public UserController(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpGet]
        [Validate]
        public IEnumerable<UserDetails> Get()
        {
            return _userRepo.getAllUserList();
        }
        [HttpPost]
        [Route("GetDetails")]
        [Validate]
        public IEnumerable<UserDetailsView> GetDetails(UserDetailsView Data)
        {
            return _userRepo.getAllUserListView();
        }
    }
}
