using ClubMembershipApp.FieldValidators;
using ClubMembershipApp.Models;

namespace ClubMembershipApp.Views;

public class WelcomeUserView: IView
{
    private User _user = null;

    public WelcomeUserView(User user)
    {
        _user = user;
    }
    public void RunView()
    {
        Console.Clear();
        CommonOutputFormat.ChangeFontColor(FontTheme.Success);
        Console.WriteLine($"Welcome {_user.FirstName}");
        CommonOutputFormat.ChangeFontColor(FontTheme.Default);
        Console.ReadKey();
    }

    public IFieldValidator FieldValidator => null;
}