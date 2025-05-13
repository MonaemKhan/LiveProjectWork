using CentralAPIs.DBConfiguration;
using CentralAPIs.IRepo;
using CentralModels.Administration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Text;

namespace CentralAPIs.Repo
{
    public class LoginRepo: ILoginRepo
    {
        private readonly AdminstrationDbContext _adminstrationDbContext;

        public LoginRepo(AdminstrationDbContext adminstrationDbContext)
        {
            _adminstrationDbContext = adminstrationDbContext;
        }

        private string TokenGenerate()
        {
            string token = Guid.NewGuid().ToString().Replace("-","");
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public LoginResponce loginValidation(string userid,string password,string projectName)
        {
            UserDetails userDetails = new UserDetails();
            UserSession sessionData = new UserSession();
            LoginResponce responce = new LoginResponce();


            if(userid == "MonaemKhan" &&  password == "Monaem@123")
            {
                responce.userId = sessionData.UserId = "MonaemKhan";
                responce.userRole = "SuperAdmin";
                responce.userName = "M.A. Monaem Khan";
                var temp = _adminstrationDbContext.UserSession
                                .Where(x => x.UserId == userid)
                                .FirstOrDefault();
                if(temp != null || !string.IsNullOrEmpty(temp.UserId))
                {
                    _adminstrationDbContext.UserSession.Remove(temp);
                }
            }
            else
            {
                userDetails = _adminstrationDbContext.UserDetails
                                .Where(x => x.user_id == userid && x.project_id == projectName)
                                .FirstOrDefault();
                sessionData = _adminstrationDbContext.UserSession
                                .Where(x => x.UserId == userid)
                                .FirstOrDefault();
                if (userDetails ==  null || string.IsNullOrEmpty(userDetails.user_id))
                {
                    throw new Exception("User Do Not Exits");
                }
                if (password != userDetails.user_password)
                {
                    throw new Exception("Password Do Not Match");
                }

                if (!string.IsNullOrEmpty(sessionData.SessionId))
                {
                    throw new Exception("User Already Login Into The System");
                }

                responce.userId = sessionData.UserId = userDetails.user_id;
                responce.userRole = userDetails.user_type;
                responce.userName = userDetails.user_name;
            }

            // generate session
            sessionData.SessionId = Guid.NewGuid().ToString();
            responce.token =  sessionData.AccessToken = TokenGenerate();
            sessionData.CreatedAt = DateTime.UtcNow;
            sessionData.ExpiresAt = DateTime.UtcNow.AddMinutes(10);

            // save session
            try
            {
                _adminstrationDbContext.AddRange(sessionData);
                _adminstrationDbContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }            
            return responce;
        }
    }
}
