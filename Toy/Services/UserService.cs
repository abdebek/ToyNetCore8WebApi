using Toy.Extensions;

namespace Toy.Services;
public interface IUserService
{
    UserModel GenerateUser(int id);
    string SerializeUserToJson(UserModel user);
}

public class UserService : IUserService
{
    public string SerializeUserToJson(UserModel user)
    {
        user.UserId = 123;

        Console.WriteLine($"Deserialized user: {user.FirstName} {user.LastName}");
        Console.WriteLine($"First address: {user.Addresses?[0]?.StreetAddress}");

        var jsonString = user.ToSnakeCaseJson(); // JsonConvert.SerializeObject(user); would convert to PascalCase
        return jsonString;
    }


    public UserModel GenerateUser(int id)
    {
        return new UserModel
        {
            UserId = id,
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 15),
            Addresses = new List<AddressModel>
            {
                new AddressModel
                {
                    AddressId = 1,
                    StreetAddress = "123 Main St",
                    City = "New York",
                    PostalCode = "10001",
                    IsPrimary = true
                },
                new AddressModel
                {
                    AddressId = 2,
                    StreetAddress = "456 Park Ave",
                    City = "New York",
                    PostalCode = "10002",
                    IsPrimary = false
                }
            }
        };
    }
}
