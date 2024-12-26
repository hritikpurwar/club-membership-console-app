using ClubMembershipApp.Data;
using ClubMembershipApp.FieldValidators;
using ClubMembershipApp.Views;

namespace ClubMembershipApp;

public static class Factory
{
    public static IView GetMainViewObject()
    {
        ILogin login = new LoginUser();
        IRegister register = new RegisterUser();
        IFieldValidator fieldValidator = new UserRegistrationValidator(register);
        fieldValidator.InitializeValidatorDelegates();
        IView registerView = new UserRegistrationView(register, fieldValidator);
        IView loginView = new UserLoginView(login);
        IView mainView = new MainView(registerView, loginView);
        
        return mainView;
    }   
}