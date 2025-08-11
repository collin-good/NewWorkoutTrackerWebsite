using System.ComponentModel.DataAnnotations;

namespace WorkoutTrackerWebsite.Models;

public class UserDB_DIT
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }

    public UserDB_DIT() : this("", "", "") { }

    public UserDB_DIT(string email, string username, string password)
    {
        Email = email;
        Username = username;
        //TODO encrypt Password
        Password = password;
    }

    public static implicit operator UserDB_DIT(UserDB user) => new UserDB_DIT(user.Email, user.Username, user.Password);
}