using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;
using Auction.DataAccess.Postgres;

namespace Auction.App.Models;

public class UserModel
{
    private readonly AuctionDbContext _dbContext;
    public const int MIN_PASSWORD_LENGTH = 8;
    
    private UserModel(Guid id, string email, string password, string nickname, AuctionDbContext dbContext)
    {
        _dbContext = dbContext;
        Id = id;
        Email = email;
        Password = password;
        Nickname = nickname;
    }
    public Guid Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty ;
    public string Nickname { get; set; } = string.Empty;

    public static async Task<(UserModel User, string Error)> Create(Guid id, string email, string password, string nickname, AuctionDbContext dbContext)    
    {
        var error = string.Empty;
        var existingUser = dbContext.Users.FirstOrDefault(u => u.Email == email || u.Nickname == nickname);
        if (existingUser != null)
        {
            error = "User already exists";
            return (null, error);
        }
        else if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(nickname))
        {
            error = "Fields cannot be empty";
            return (null, error);
        }
        else if(!IsValidEmail(email))
        {
            error = "Invalid email";
            return (null, error);
        }
        else if (password.Length < MIN_PASSWORD_LENGTH)
        {
            error = "Password must be more then 8 symbols";
        }
        var user = new UserModel(id, email, password, nickname,dbContext);
        return (user, error);
    }

    private static bool IsValidEmail(string email)
    {
        string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
        return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
    }
    public static async Task<(Guid Id, string error)> Login(string email, string password, AuctionDbContext dbContext)
    {
        var error = string.Empty;
        var user = dbContext.Users.FirstOrDefault(u => u.Email == email);
        if (user == null)
        {
            error = "User with such email not found";
            return (Guid.Empty, error);
        }
        else if (user.Password!= password)
        {
            error = "Invalid password";
            return (Guid.Empty, error);
        }
        return (user.Id, error);
    }
    
}