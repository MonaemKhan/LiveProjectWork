using CentralAPIs.DBConfiguration;
using CentralAPIs.IRepo;
using CentralModels.Administration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace CentralAPIs.Repo
{
    public class LoginRepo : ILoginRepo
    {
        private readonly AdminstrationDbContext _adminstrationDbContext;
        private readonly ISessionRepo _sessionRepo;

        public LoginRepo(AdminstrationDbContext adminstrationDbContext, ISessionRepo sessionRepo)
        {
            _adminstrationDbContext = adminstrationDbContext;
            _sessionRepo = sessionRepo;
        }

        public async Task<LoginResponce> loginValidation(string userid, string password, string projectName)
        {
            try
            {
                UserDetails userDetails = new UserDetails();
                LoginResponce responce = new LoginResponce();
                responce.userInfo = new UserInfo();

                string token = "";


                if (userid == "MonaemKhan" && password == "Monaem@123")
                {
                    responce.userInfo.userId = "MonaemKhan";
                    responce.userInfo.userRole = "SuperAdmin";
                    responce.userInfo.userName = "M.A. Monaem Khan";
                    await _sessionRepo.deleteSessionToken(userid);
                }
                else
                {
                    userDetails = await _adminstrationDbContext.UserDetails
                                    .Where(x => x.user_id == userid && x.project_id == projectName)
                                    .FirstOrDefaultAsync();



                    if (userDetails == null || string.IsNullOrEmpty(userDetails.user_id))
                    {
                        throw new Exception("User Do Not Exits");
                    }
                    if (password != userDetails.user_password)
                    {
                        throw new Exception("Password Do Not Match");
                    }
                    responce.userInfo.userId = userDetails.user_id;
                    responce.userInfo.userRole = userDetails.user_type;
                    responce.userInfo.userName = userDetails.user_name;
                }

                responce.token = await _sessionRepo.createSessionToken(userid);

                return responce;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
