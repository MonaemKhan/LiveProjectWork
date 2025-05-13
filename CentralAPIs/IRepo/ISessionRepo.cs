namespace CentralAPIs.IRepo
{
    public interface ISessionRepo
    {
        public (bool, string) validateSession(string token);
    }
}
