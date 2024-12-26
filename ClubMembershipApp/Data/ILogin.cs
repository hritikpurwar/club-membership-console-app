using ClubMembershipApp.Models;

namespace ClubMembershipApp.Data;

public interface ILogin
{
    User Login(string email, string password);
}