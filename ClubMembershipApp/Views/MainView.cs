using ClubMembershipApp.FieldValidators;

namespace ClubMembershipApp.Views;

public class MainView: IView
{
    public MainView(IView registerView, IView loginView)
    {
        _registerView = registerView;
        _loginView = loginView;
    }
    public void RunView()
    {
        CommonOutputText.WriteMainHeading();
        Console.WriteLine("Please press 'l' to login or if you are not registered press 'r'");
        ConsoleKey key = Console.ReadKey().Key;
        if (key == ConsoleKey.L)
        {
            RunLoginView();
        }
        else if (key == ConsoleKey.R)
        {
            RunRegisterationView();
            RunLoginView();
        }
        else
        {
            Console.Clear();
            Console.WriteLine("Goodbye");
            Console.ReadKey();
        }
    }

    private void RunRegisterationView()
    {
        _registerView.RunView();
    }

    private void RunLoginView()
    {
        _loginView.RunView();
    }

    public IFieldValidator FieldValidator => null;
    private IView _loginView;
    private IView _registerView;
}