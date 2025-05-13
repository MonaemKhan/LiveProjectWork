using CentralAPIs.DBConfiguration;
using CentralAPIs.IRepo;

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
    }
}
