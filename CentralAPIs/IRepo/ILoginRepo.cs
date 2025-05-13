using CentralModels.Administration;

namespace CentralAPIs.IRepo
{
    public interface ILoginRepo
    {
        public LoginResponce loginValidation(string userid, string password,string projectName);
    }
}
