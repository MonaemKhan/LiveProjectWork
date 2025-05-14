using CentralModels.Administration;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CommonUI
{
    public interface ISessionService
    {
        public Task setLoginResponce(LoginResponce loginData);
        public Task<UserInfo> getUserInfo();
        public Task<string> getToken();

    }
    public class SessionService: ISessionService
    {
        private readonly ProtectedSessionStorage _sessionstorage;

        public SessionService(ProtectedSessionStorage sessionstorage)
        {
            _sessionstorage = sessionstorage;
        }

        public async Task setLoginResponce(LoginResponce loginData)
        {
            await _sessionstorage.SetAsync("UserInfo",loginData.userInfo);
            await _sessionstorage.SetAsync("Token",loginData.userInfo);
        }

        public async Task<UserInfo> getUserInfo()
        {
            var data = await _sessionstorage.GetAsync<UserInfo>("UserInfo");
            if (data.Success)
            {
                return data.Value;
            }

            return null;
        }

        public async Task<string> getToken()
        {
            var data = await _sessionstorage.GetAsync<string>("Token");
            if (data.Success)
            {
                return data.Value;
            }

            return null;
        }
    }
}
