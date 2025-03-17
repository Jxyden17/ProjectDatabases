

namespace SomerenMVC.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string MobileNumber { get; set; }
        public string EmailAdress {  get; set; }
        public string Password { get; set; }


        public User()
        {

        }
        public User(int userId, string userName, string mobileNumber, string emailAdress)
        {
            UserId = userId;
            UserName = userName;
            MobileNumber = mobileNumber;
            EmailAdress = emailAdress;
            //Password = password;
        }

        public User(int userId, string userName, string mobileNumber, string emailAdress, string password)
        {
            UserId = userId;
            UserName = userName;
            MobileNumber = mobileNumber;
            EmailAdress = emailAdress;
            Password = password;
        }

    }
}
