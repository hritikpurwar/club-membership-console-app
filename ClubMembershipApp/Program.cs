// See https://aka.ms/new-console-template for more information

using ClubMembershipApp;
using ClubMembershipApp.Views;

IView mainView = Factory.GetMainViewObject();
mainView.RunView();
Console.ReadKey();