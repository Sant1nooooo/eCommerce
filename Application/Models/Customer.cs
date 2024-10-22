namespace Server.Application.Models
{
    public class Customer:User
    {
        public double Balance { get; set; }
        public int Points { get; set; }
        public bool IsMember { get; set; }

        public Customer(string FirstName, string LastName, string Email, string Password) : base(FirstName, LastName, Email, Password, "Customer")
        {
            this.Balance = 0;
            this.Points = 0;
            this.IsMember = false;
        }
    }
}
