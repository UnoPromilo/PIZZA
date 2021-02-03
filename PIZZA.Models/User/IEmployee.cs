namespace PIZZA.Models.User
{
    public interface IEmployee
    {
        int ID { get; set; }
        string Email { get; set; }
        string PhoneNumber { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string AddressLine { get; set; }
        string PostalCode { get; set; }
        string Town { get; set; }
        string UserName { get; set; }
    }
}
