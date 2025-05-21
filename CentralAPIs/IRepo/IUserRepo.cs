using CentralModels.Administration;

namespace CentralAPIs.IRepo
{
    public interface IUserRepo
    {
        public List<UserDetails> getAllUserList();
        public List<UserDetailsView> getAllUserListView(string projectName = "SuperAdmin", int allUser = 0, int alldeactive = 0);
    }
}
