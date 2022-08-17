
namespace MCBA.src.Model.Login
{
    class Login
    {
        public string LoginId { get; set; }
        public int CustomerID { get; set; }
        public string PasswordHash { get; set; }

        public Login(string loginId, int customerID, string passwordHash)
        {
            this.LoginId = loginId;
            this.CustomerID = customerID;
            this.PasswordHash = passwordHash;
        }
    }
}
