using ClubMembershipApp.FieldValidators;

namespace ClubMembershipApp.Views;

public interface IView
{
    void RunView();
    IFieldValidator FieldValidator { get; }
}