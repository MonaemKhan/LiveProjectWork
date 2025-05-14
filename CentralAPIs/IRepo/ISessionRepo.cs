using CentralModels.Administration;

namespace CentralAPIs.IRepo
{
    public interface ISessionRepo
    {
        public (bool, string) validateSession(string token);
        public Task<bool> isSessionExits(string userId);
        public Task<string> createSessionToken(string UserId);
        public Task deleteSessionToken(string UserId);
    }
}
