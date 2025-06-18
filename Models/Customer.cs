namespace Models
{
    public partial class Customer : User
    {

        public string Telephone { get; set; }

        public Customer() { }

        public Customer(string Telephone) : base()
        {

        }

        public Customer(int userID, string fullName, string email, DateOnly birthday, int status, string password, string telephone) : base(userID, fullName, email, birthday, status, password)
        {
            Telephone = telephone;
        }
    }
}
