using InfnetEcommerceContext.User.API.Models.Entities;
using InfnetEcommerceContext.User.API.Repository.DataContext;
using InfnetEcommerceContext.User.API.Services;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InfnetEcommerceContext.User.API.Models.DTOs;


namespace InfnetEcommerceContext.User.API.Controllers;

public class UserDataAndAccessTokenResponse 
{
    /// <summary>
    /// The value is always "Bearer" which indicates this response provides a "Bearer" token
    /// in the form of an opaque <see cref="AccessToken"/>.
    /// </summary>
    /// <remarks>
    /// This is serialized as "tokenType": "Bearer" using <see cref="JsonSerializerDefaults.Web"/>.
    /// </remarks>
    public string TokenType { get; } = "Bearer";

    /// <summary>
    /// The opaque bearer token to send as part of the Authorization request header.
    /// </summary>
    /// <remarks>
    /// This is serialized as "accessToken": "{AccessToken}" using <see cref="JsonSerializerDefaults.Web"/>.
    /// </remarks>
    public required string AccessToken { get; init; }

    /// <summary>
    /// The number of seconds before the <see cref="AccessToken"/> expires.
    /// </summary>
    /// <remarks>
    /// This is serialized as "expiresIn": "{ExpiresInSeconds}" using <see cref="JsonSerializerDefaults.Web"/>.
    /// </remarks>
    public required long ExpiresIn { get; init; }

    /// <summary>
    /// If set, this provides the ability to get a new access_token after it expires using a refresh endpoint.
    /// </summary>
    /// <remarks>
    /// This is serialized as "refreshToken": "{RefreshToken}" using using <see cref="JsonSerializerDefaults.Web"/>.
    /// </remarks>
    public required string RefreshToken { get; init; }
    
    public UserEntityDTO User { get; init; }
}
public static class Roles
{
    public const string AdminRole = "Admin";
    public const string UserRole = "User";
    
}

[Route("[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly UserService userService;

    public UserController(UserService userService)
    {
        this.userService = userService;
    }

    [HttpGet("info")]
    [Authorize(Roles=$"{Roles.UserRole}")]
    public async Task<IActionResult> GetUserInfoAsync()
    {
        if (User.Identity is { IsAuthenticated: false })
        {
            return Unauthorized();
        }

        if (User.IsInRole(Roles.UserRole))
        {
            var userEmail = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            var userInfo = await userService.GetByEmail(userEmail);

            return Ok(userInfo);
        }
        
        return Forbid();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<UserDataAndAccessTokenResponse>> LoginUserCommandRouteAsync2([FromBody] LoginUserCommand loginUserCommand, [FromServices] UserContext userContext)
    {

        var login = loginUserCommand.UserName;
        var password = loginUserCommand.Password; 

        var userLogin = await userContext.Users.FirstOrDefaultAsync(c => c.UserName.Equals(login) && c.Password.Equals(password));

        if (userLogin == null)
        {
            return BadRequest();
        }

        var token = GenerateToken(userLogin);
        var userData = new UserEntityDTO(userLogin);

        
        var tkResponse = new UserDataAndAccessTokenResponse()
        {
            AccessToken = token,
            ExpiresIn = DateTimeOffset.Now.AddHours(2).ToUnixTimeMilliseconds(),
            RefreshToken = token,
            User = userData
        };

        return Ok(tkResponse);
    }

    public static string GenerateToken(UserEntity user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(SettingsApi.Secret);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserName.ToString()),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim("id", user.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }

}

public class LoginUserCommand
{
    public string UserName { get; set; }
    public string Password { get; set; }
}