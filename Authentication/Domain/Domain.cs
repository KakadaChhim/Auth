namespace Authentication.Domain
{
    public class Branch: ShareDomain
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Note { get; set; }
    }
    public class User: ShareDomain
    {
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }

    public class PasswordForStore
    {
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
    }
}
