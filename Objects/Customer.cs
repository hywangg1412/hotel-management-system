namespace Objects
{
    public partial class Customer
    {
        public int CustomerId { get; set; }
        public string CustomerFullName { get; set; }
        public string Telephone { get; set; }
        public string EmailAddress { get; set; }
        public DateOnly CustomerDOB { get; set; }
        public string Status { get; set; }
        public string Password { get; set; }

        public Customer() { }

        public Customer(int customerId, string customerFullname, string telephone, string emailAddress,
            DateOnly customerDOB, string status, string password)
        {
            CustomerId = customerId;
            CustomerFullName = customerFullname;
            Telephone = telephone;
            EmailAddress = emailAddress;
            CustomerDOB = customerDOB;
            Status = status;
            Password = password;
        }
    }
}
