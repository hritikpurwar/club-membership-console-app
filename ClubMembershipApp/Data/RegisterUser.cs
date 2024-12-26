using System.Reflection.Metadata;
using ClubMembershipApp.FieldValidators;
using ClubMembershipApp.Models;

namespace ClubMembershipApp.Data;

public class RegisterUser: IRegister
{
    public bool Register(string[] fields)
    {
        using (var dbContext = new ClubMembershipDbContext())
        {
            User user = new()
            {
                Email = fields[(int)FieldConstants.UserRegistrationField.Email],
                FirstName = fields[(int)FieldConstants.UserRegistrationField.FirstName],
                LastName = fields[(int)FieldConstants.UserRegistrationField.LastName],
                Password = fields[(int)FieldConstants.UserRegistrationField.Password],
                DateOfBirth = DateTime.Parse(fields[(int)FieldConstants.UserRegistrationField.DateOfBirth]),
                PhoneNumber = fields[(int)FieldConstants.UserRegistrationField.PhoneNumber],
                AddressFirstLine = fields[(int)FieldConstants.UserRegistrationField.AddressFirstLine],
                AddressSecondLine = fields[(int)FieldConstants.UserRegistrationField.AddressSecondLine],
                AddressCity = fields[(int)FieldConstants.UserRegistrationField.AddressCity],
                PostCode = fields[(int)FieldConstants.UserRegistrationField.PostCode]
            };
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
        }
        
        return true;
    }

    public bool EmailExists(string email)
    {
        using var dbContext = new ClubMembershipDbContext();
        return dbContext.Users.Any(u => u.Email.ToLower().Trim() == email.ToLower().Trim());
    }
}