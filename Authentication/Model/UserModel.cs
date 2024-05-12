
namespace Authentication.Model
{
    public class UserModel
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string PasswordHash { get; set; }
        public string PasswordSalt { get; set; }
        public string Email { get; set; }
    }
    public class UserAddModel 
    {
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
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
