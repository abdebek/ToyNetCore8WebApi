using Microsoft.AspNetCore.Mvc;
using Toy.Services;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;

    public UsersController(ILogger<UsersController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    /// <summary>
    /// POST endpoint that accepts snake_case JSON, and returns a deserialized PascalCase json object, serialized snake_case JSON string   
    /// </summary>
    /// <param name="user">The user.</param>
    /// <returns></returns>
    [HttpPost]
    public ActionResult<(UserModel, dynamic)> CreateUser(UserModel user)
    {
        string jsonString = _userService.SerializeUserToJson(user);

        _logger.LogInformation($"Received user: {user.FirstName} {user.LastName}");

        // The response will be automatically serialized back to snake_case
        return Ok((user, jsonString));
    }

    /// <summary>
    /// Gets the user.
    /// </summary>
    /// <param name="id">The identifier.</param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public ActionResult<UserModel> GetUser(int id)
    {
        // Create a sample user (in our C# code we use PascalCase)
        UserModel user = _userService.GenerateUser(id);
        _logger.LogInformation($"Generated user: {user.FirstName} {user.LastName}");

        // This will be serialized to snake_case automatically
        return Ok(user);
    }
}
