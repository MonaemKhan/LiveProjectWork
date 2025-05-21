using CentralModels.Administration;

namespace CentralAPIs.IRepo
{
    public interface ILoginRepo
    {
        public Task<LoginResponce> loginValidation(string userid, string password,string projectName);
    }
}
