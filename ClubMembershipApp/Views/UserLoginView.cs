using ClubMembershipApp.Data;
using ClubMembershipApp.FieldValidators;
using ClubMembershipApp.Models;

namespace ClubMembershipApp.Views;

public class UserLoginView:IView
{
    private ILogin _login = null;
    public void RunView()
    {
        Console.Clear();
        CommonOutputText.WriteMainHeading();
        CommonOutputText.WriteLoginHeading();
        Console.WriteLine("Please enter your email address:");
        string email = Console.ReadLine();
        Console.WriteLine("Please enter your password:");
        string password = Console.ReadLine();
        User user = _login.Login(email, password);
        if (user != null)
        {
            WelcomeUserView welcomeUserView = new WelcomeUserView(user);
            welcomeUserView.RunView();
        }
        else
        {
            Console.Clear();
            CommonOutputFormat.ChangeFontColor(FontTheme.Danger);
            Console.WriteLine("The information you entered do not match our records.");
            CommonOutputFormat.ChangeFontColor(FontTheme.Default);
            Console.ReadKey();
        }
    }

    public UserLoginView(ILogin login)
    {
        _login = login;
    }
    public IFieldValidator FieldValidator => null;
}