namespace Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateOnly Birthday { get; set; }
        public int Status { get; set; }
        public string Password { get; set; }

        public User() { }

        public User(int userID, string fullName, string email, DateOnly birthday, int status, string password)
        {
            UserID = userID;
            FullName = fullName;
            Email = email;
            Birthday = birthday;
            Status = status;
            Password = password;
        }
    }
}
