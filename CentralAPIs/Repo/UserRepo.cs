using CentralAPIs.DBConfiguration;
using CentralAPIs.IRepo;
using CentralModels.Administration;
using Microsoft.EntityFrameworkCore;

namespace CentralAPIs.Repo
{
    public class UserRepo : IUserRepo
    {
        private readonly AdminstrationDbContext _adminstrationDbContext;

        public UserRepo(AdminstrationDbContext adminstrationDbContext)
        {
            _adminstrationDbContext = adminstrationDbContext;
        }

        public List<UserDetails> getAllUserList()
        {
            return _adminstrationDbContext.UserDetails.ToList();
        }

        public List<UserDetailsView> getAllUserListView(string projectName = "SuperAdmin", int allUser = 0, int alldeactive = 0)
        {
            //// by defaulty we only get the active user            
            return _adminstrationDbContext.UserDetailsView
                    .FromSqlRaw($"exec _user.getUserList @projectName = {projectName}, @allUser = {allUser}, @allDeactiveUser = {alldeactive}").ToList();
        }
    }
}
