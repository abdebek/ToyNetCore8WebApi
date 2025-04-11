// First, let's create a basic model class with PascalCase properties
public class AddressModel
{
    public int AddressId { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string PostalCode { get; set; }
    public bool IsPrimary { get; set; }
}
