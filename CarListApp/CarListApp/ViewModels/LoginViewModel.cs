
using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;


namespace CarListApp.Maui.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        [RelayCommand]
        async Task Login()
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayLoginError();
            }
            else
            {
                var loginSuccessful = true;

                if (loginSuccessful)
                {

                }

                await DisplayLoginError();
            }

        }


        async Task DisplayLoginError()
        {
            await Shell.Current.DisplayAlert("Essai incorrect", "Nom ou mot de passe incorect", "OK");
            Password = string.Empty;
        }
    }
}