using Microsoft.AspNetCore.Mvc;
using dotnet.Db;
using BCrypt.Net;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
namespace dotnet.Controllers;


public class UserRequest
{
    public required string Username { get; set; }
    public required string Password { get; set; }
}


[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly MyDbContext _context;
    public AuthController(MyDbContext context)
    {
        _context = context;
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] UserRequest request)
    {
        if (request == null || string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest(new { Message = "Username and password are required" });
        }
        try
        {
            var user = _context.Users.FirstOrDefault(u => u.Username == request.Username);
            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                return Unauthorized(new { Message = "Invalid username or password" });
            }
            var access_token = GenerateAccessToken(user.Username);
            var refresh_token = GenerateRefreshToken();
            user.RefreshToken = refresh_token;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            user.UpdatedAt = DateTime.UtcNow;
            _context.Users.Update(user);
            _context.SaveChanges();

            Response.Cookies.Append("refresh_token", refresh_token, new CookieOptions
            {
                HttpOnly = true,
                Secure = false, // Set to true if using HTTPS
                SameSite = SameSiteMode.Strict, // Adjust as needed
                Expires = DateTimeOffset.UtcNow.AddDays(7) // Set expiration for the cookie
            });

            return Ok(new
            {
                Message = "Login successful",
                AccessToken = access_token,
                RefreshToken = refresh_token,
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred", Error = ex.Message });
        }
    }

    [HttpPost("refresh-token")]
    public IActionResult Refresh()
    {
        
        var refreshToken = Request.Cookies["refresh_token"];
        if (string.IsNullOrEmpty(refreshToken))
        {
            return Unauthorized(new { message = "Refresh token missing" });
        }
        var user = _context.Users.FirstOrDefault(u => u.RefreshToken == refreshToken);
        if (user == null || user.RefreshTokenExpiryTime < DateTime.UtcNow)
        {
            return Unauthorized(new { message = "Invalid or expired refresh token" });
        }
        var newAccessToken = GenerateAccessToken(user.Username); // ฟังก์ชันของคุณ


        return Ok(new 
        {
            AccessToken = newAccessToken,
        });
    }


    private string GenerateAccessToken(string username)
    {
        var config = new ConfigurationBuilder()
             .AddJsonFile("appsettings.json")
             .Build();
        var jwt = config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
         {
            new Claim(ClaimTypes.Name, username),
        };

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(jwt["ExpiresInMinutes"])),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    private string GenerateRefreshToken()
    {
        var randomNumber = new byte[32];
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
        {
            rng.GetBytes(randomNumber);
        }
        return Convert.ToBase64String(randomNumber);
    }
}
