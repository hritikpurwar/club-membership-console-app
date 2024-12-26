using ClubMembershipApp.Models;

namespace ClubMembershipApp.Data;

public class LoginUser : ILogin
{
    public User Login(string email, string password)
    {
        User user = null;
        using (var dbContext = new ClubMembershipDbContext())
        {
            user = dbContext.Users.FirstOrDefault(u =>
                u.Email.Trim().ToLower() == email.Trim().ToLower() && u.Password.Equals(password));
        }
        return user;
    }
}