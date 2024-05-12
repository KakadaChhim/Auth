
namespace Authentication.Model
{
    public class UserModel
    {
        public string Username { get; set; }
    }
    public class UserAddModel 
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class UserEditModel : UserModel
    {
        public long Id { get; set; }
    }
    public class UserListModel : UserModel
    {
        public long Id { get; set; }
    }
    public class UserViewModel : UserModel
    {
        public long Id { get; set; }
    }
}
