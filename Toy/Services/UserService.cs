using Bogus;
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
        var addressFaker = new Faker<AddressModel>()
            .RuleFor(a => a.AddressId, f => f.IndexFaker + 1)
            .RuleFor(a => a.StreetAddress, f => f.Address.StreetAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
            .RuleFor(a => a.IsPrimary, (f, a) => a.AddressId == 1);

        var userFaker = new Faker<UserModel>()
            .RuleFor(u => u.UserId, id)
            .RuleFor(u => u.FirstName, f => f.Name.FirstName())
            .RuleFor(u => u.LastName, f => f.Name.LastName())
            .RuleFor(u => u.DateOfBirth, f => f.Date.Past(30, DateTime.Today.AddYears(-18)))
            .RuleFor(u => u.Addresses, f => addressFaker.Generate(2));

        return userFaker.Generate();
    }
}
