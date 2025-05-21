using CentralAPIs.DBConfiguration;
using CentralAPIs.IRepo;
using CentralModels.Administration;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace CentralAPIs.Repo
{
    public class SessionRepo: ISessionRepo
    {
        private readonly AdminstrationDbContext _adminstrationDbContext;

        public SessionRepo(AdminstrationDbContext adminstrationDbContext)
        {
            _adminstrationDbContext = adminstrationDbContext;
        }

        public (bool,string) validateSession(string token)
        {
            var sessionData = _adminstrationDbContext.UserSession
                                .Where(x => x.AccessToken == token)
                                .FirstOrDefault();
            if (sessionData == null)
            {
                return (false, "Provided Token Is Invalid");
            }

            if(sessionData.ExpiresAt <= DateTime.Now)
            {
                return (false, "Token is Already Expried");
            }

            return (true,"");
        }

        public async Task<bool> isSessionExits(string userId)
        {
            UserSession sessionData = new UserSession();
            sessionData = await _adminstrationDbContext.UserSession
                                .Where(x => x.UserId == userId)
                                .FirstOrDefaultAsync();

            if (sessionData != null)
            {
                return true;
            }

            return false;
        }

        public async Task<string> createSessionToken(string UserId)
        {
            string token = "";
            UserSession sessionData = new UserSession();


            if (await isSessionExits(UserId))
            {
                throw new Exception("User Already Login Into The System");
            }

            sessionData.SessionId = Guid.NewGuid().ToString();
            token = sessionData.AccessToken = TokenGenerate();
            sessionData.UserId = UserId;
            sessionData.CreatedAt = DateTime.Now;
            sessionData.ExpiresAt = DateTime.Now.AddMinutes(10);


            // save session
            try
            {
                await _adminstrationDbContext.AddRangeAsync(sessionData);
                await _adminstrationDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return token;
        }

        public async Task deleteSessionToken(string UserId)
        {
            UserSession temp = await _adminstrationDbContext.UserSession
                                .Where(x => x.UserId == UserId)
                                .FirstOrDefaultAsync();

            if (temp != null)
            {
                _adminstrationDbContext.UserSession.Remove(temp);
            }
        }
        private string TokenGenerate()
        {
            string token = (Guid.NewGuid().ToString() + Guid.NewGuid().ToString()).Replace("-", "");
            using var sha = SHA256.Create();
            var bytes = Encoding.UTF8.GetBytes(token);
            var hash = sha.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }
    }
}
