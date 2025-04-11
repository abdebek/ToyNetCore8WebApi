// First, let's create a basic model class with PascalCase properties
// Our model using PascalCase property names
public class UserModel
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public List<AddressModel> Addresses { get; set; }
}
