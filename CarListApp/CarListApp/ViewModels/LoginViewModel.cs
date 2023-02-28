using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CarListApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarListApp.Maui.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        public LoginViewModel(CarApiService carApiService)
        {
            this.carApiService = carApiService;
        }

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;
        private CarApiService carApiService;

        [RelayCommand]
        async Task Login()
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                await DisplayLoginMessage("Tentative de connection échouée");
            }
            else
            {
                var loginModel = new LoginModel(username, password);

                var response = await carApiService.Login(loginModel);

                await DisplayLoginMessage(carApiService.StatusMessage);

                if (!string.IsNullOrEmpty(response.Token))
                {
                    await SecureStorage.SetAsync("Token", response.Token);

                    var jsonToken = new JwtSecurityTokenHandler().ReadToken(response.Token) as JwtSecurityToken;

                    var role = jsonToken.Claims.FirstOrDefault(q => q.Type.Equals(ClaimTypes.Role))?.Value;

                    App.UserInfo = new UserInfo()
                    {
                        Username = Username,
                        Role = role
                    };
                    await Shell.Current.GoToAsync($"//{nameof(MainPage)}");
                }
                else
                {
                    await DisplayLoginMessage("Tentative de connection non valide");
                }
            }
        }

        async Task DisplayLoginMessage(string message)
        {
            await Shell.Current.DisplayAlert("Résultat de la tentative de connection", message, "OK");
            Password = string.Empty;
        }
    }
}