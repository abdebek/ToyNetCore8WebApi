using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;

namespace Toy.Extensions;

public static class JsonExtensions
{
    public static readonly JsonSerializerSettings SnakeCaseSettings = new JsonSerializerSettings
    {
        ContractResolver = new DefaultContractResolver
        {
            NamingStrategy = new SnakeCaseNamingStrategy()
        },
        Formatting = Formatting.Indented
    };

    public static string ToSnakeCaseJson(this object obj) =>
        JsonConvert.SerializeObject(obj, SnakeCaseSettings);
}
