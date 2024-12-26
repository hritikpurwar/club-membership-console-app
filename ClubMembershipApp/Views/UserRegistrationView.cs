using ClubMembershipApp.Data;
using ClubMembershipApp.FieldValidators;

namespace ClubMembershipApp.Views;

public class UserRegistrationView : IView
{
    public void RunView()
    {
        CommonOutputText.WriteMainHeading();
        CommonOutputText.WriteRegistrationHeading();
        _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.Email] =
            GetInputFromUser(FieldConstants.UserRegistrationField.Email, "Please enter your email address");
        _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.FirstName] =
            GetInputFromUser(FieldConstants.UserRegistrationField.FirstName, "Please enter your First Name");
        _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.LastName] =
            GetInputFromUser(FieldConstants.UserRegistrationField.LastName, "Please enter your Last Name");
        _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.Password] =
            GetInputFromUser(FieldConstants.UserRegistrationField.Password, "Please enter your password");
        _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.PasswordCompare] =
            GetInputFromUser(FieldConstants.UserRegistrationField.PasswordCompare, "Please re-enter your password");
        _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.DateOfBirth] =
            GetInputFromUser(FieldConstants.UserRegistrationField.DateOfBirth, "Please enter your date of birth");
        _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.PhoneNumber] =
            GetInputFromUser(FieldConstants.UserRegistrationField.PhoneNumber, "Please enter your phone number");
        _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.AddressFirstLine] =
            GetInputFromUser(FieldConstants.UserRegistrationField.AddressFirstLine, "Please enter first line of your address");
        _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.AddressSecondLine] =
            GetInputFromUser(FieldConstants.UserRegistrationField.AddressSecondLine, "Please enter second line of your address");
        _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.AddressCity] =
            GetInputFromUser(FieldConstants.UserRegistrationField.AddressCity, "Please enter your city");
        _fieldValidator.FieldArray[(int)FieldConstants.UserRegistrationField.PostCode] =
            GetInputFromUser(FieldConstants.UserRegistrationField.PostCode, "Please enter your post code");
        
        RegisterUser();
    }

    private void RegisterUser()
    {
        _register.Register(_fieldValidator.FieldArray);
        CommonOutputFormat.ChangeFontColor(FontTheme.Success);
        Console.WriteLine("You have successfully registered.Press any key to login.");
        CommonOutputFormat.ChangeFontColor(FontTheme.Default);
        Console.ReadKey();
    }

    private IFieldValidator _fieldValidator = null;
    IRegister _register = null;

    public IFieldValidator FieldValidator
    {
        get => _fieldValidator;
    }

    public UserRegistrationView(IRegister register, IFieldValidator fieldValidator)
    {
        _fieldValidator = fieldValidator;
        _register = register;
    }

    private string GetInputFromUser(FieldConstants.UserRegistrationField field, string promptText)
    {
        string fieldVal = string.Empty;
        do
        {
            Console.WriteLine(promptText);
            fieldVal = Console.ReadLine();
        } while (!FieldValid(field, fieldVal));

        return fieldVal;
    }

    private bool FieldValid(FieldConstants.UserRegistrationField field, string fieldValue)
    {
        if (!_fieldValidator.ValidatorDel((int)field, fieldValue, _fieldValidator.FieldArray, out string invalidMessage))
        {
            CommonOutputFormat.ChangeFontColor(FontTheme.Danger);
            Console.WriteLine(invalidMessage);
            CommonOutputFormat.ChangeFontColor(FontTheme.Default);
            return false;
        }

        return true;
    }
}