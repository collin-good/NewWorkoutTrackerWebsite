using System.ComponentModel.DataAnnotations;

namespace WorkoutTrackerWebsite.Models;

public class UserDB
{
    public int UserID { get; set; }
    [Required]
    public string Email{ get; set; }
    [Required]
    public string Username { get; set; }
    [Required]
    [MinLength(8)]
    public string Password { get; set; }

    public UserDB(string email, string username, string password)
    {
        Email = email;
        Username = username;
        //TODO encrypt password
        Password = password;
    }

    public static implicit operator UserDB(UserDB_DIT u) => new UserDB(u.Email, u.Username, u.Password);
}