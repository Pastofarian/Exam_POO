using CarListApp.Maui.Models;
using CarListApp.Maui.Services;
using CarListApp.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CarListApp.Maui.ViewModels
{
    public partial class CarListViewModel : BaseViewModel //partial à cause du code généré automatiquement
    {
        const string editButtonText = "Mise à jour de la voiture";
        const string createButtonText = "Ajout d'une voiture";
        private readonly CarApiService carApiService;
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;
        string message = string.Empty;

        public ObservableCollection<Car> Cars { get; private set; } = new();

        public CarListViewModel(CarApiService carApiService)
        {
            Title = "Liste de voiture (Examen POO)"; //Binding (F12)
            AddEditButtonText = createButtonText;
            this.carApiService = carApiService;
        }

        [ObservableProperty] //est utilisé pour que la vue puisse mettre à jour ces propriétés automatiquement
        bool isRefreshing;
        [ObservableProperty] //déclenche automatiquement une notification lorsqu'elle est mise à jour
        string make;
        [ObservableProperty]
        string model;
        [ObservableProperty]
        string vin;
        [ObservableProperty]
        string addEditButtonText;
        [ObservableProperty]
        int carId;

        [RelayCommand] //on contrôle sans bloquer le user  !!Avant appelé "[ICommand]"
        async Task GetCarList() //"Dependency injection"
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (Cars.Any()) Cars.Clear();
                var cars = new List<Car>();
                if (accessType == NetworkAccess.Internet) //si du réseau => API
                {
                    cars = await carApiService.GetCars();
                }
                else                                       //sinon => DB
                {
                    cars = App.CarDatabaseService.GetCars();
                }

                foreach (var car in cars) Cars.Add(car);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Impossible d'afficher des voitures: {ex.Message}");
                //await Shell.Current.DisplayAlert("Erreur", "Impossible de récupérer la liste des voitures.", "Ok");
                await ShowAlert("Impossible de récupérer la liste des voitures.");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        async Task GetCarDetails(int id)
        {
            if (id == 0) return;

            await Shell.Current.GoToAsync($"{nameof(CarDetailsPage)}?Id={id}", true);
        }

        [RelayCommand]
        async Task SaveCar()
        {
            if (string.IsNullOrEmpty(Make) || string.IsNullOrEmpty(Model) || string.IsNullOrEmpty(Vin))
            {
                //await Shell.Current.DisplayAlert("Données non valides ", " Veuillez insérer des données valides", "Veuillez insérer des données valides", "Ok");
                await ShowAlert("Veuillez insérer des données valides");
                return;
            }

            var car = new Car
            {
                Id = CarId,
                Make = Make,
                Model = Model,
                Vin = Vin
            };

            if (CarId != 0)
            {
                //    car.Id = CarId;
                //    App.CarDatabaseService.UpdateCar(car);
                //    await Shell.Current.DisplayAlert("Info", App.CarDatabaseService.StatusMessage, "Ok");
                //}
                //else
                //{
                //    App.CarDatabaseService.AddCar(car);
                //    await Shell.Current.DisplayAlert("Info", App.CarDatabaseService.StatusMessage, "Ok");
                if (accessType == NetworkAccess.Internet)
                {
                    await carApiService.UpdateCar(CarId, car);
                    message = carApiService.StatusMessage;
                }
                else
                {
                    App.CarDatabaseService.UpdateCar(car);
                    message = App.CarDatabaseService.StatusMessage;
                }
            }
            else
            {
                if (accessType == NetworkAccess.Internet)
                {
                    await carApiService.AddCar(car);
                    message = carApiService.StatusMessage;
                }
                else
                {
                    App.CarDatabaseService.AddCar(car);
                    message = App.CarDatabaseService.StatusMessage;
                }
            }

            await ShowAlert(message);
            await GetCarList();
            await ClearForm();
        }

        [RelayCommand]
        async Task DeleteCar(int id)
        {
            if (id == 0)
            {
                //    await Shell.Current.DisplayAlert("Enregistrement non valide", "Veuillez réessayer", "Ok");
                //    return;
                //}
                //var result = App.CarDatabaseService.DeleteCar(id);
                //if (result == 0) await Shell.Current.DisplayAlert("Echec", "Veuillez insérer des données valides", "Ok");
                //else
                //{
                //    await Shell.Current.DisplayAlert("Suppression réussie", "Enregistrement supprimé avec succès", "Ok");
                //    await GetCarList();
                //}
                await ShowAlert("Please try again");
                return;
            }

            if (accessType == NetworkAccess.Internet)
            {
                await carApiService.DeleteCar(id);
                message = carApiService.StatusMessage;
            }
            else
            {
                App.CarDatabaseService.DeleteCar(id);
                message = App.CarDatabaseService.StatusMessage;
            }
            await ShowAlert(message);
            await GetCarList();
        }

        [RelayCommand]
        async Task UpdateCar(int id)
        {
            AddEditButtonText = editButtonText;
            return;
        }

        [RelayCommand]
        async Task SetEditMode(int id)
        {
            AddEditButtonText = editButtonText;
            CarId = id;
            //var car = App.CarDatabaseService.GetCar(id);
            Car car;
            if (accessType == NetworkAccess.Internet)
            {
                car = await carApiService.GetCar(CarId);
            }
            else
            {
                car = App.CarDatabaseService.GetCar(CarId);
            }
            Make = car.Make;
            Model = car.Model;
            Vin = car.Vin;
        }

        [RelayCommand]
        async Task ClearForm()
        {
            AddEditButtonText = createButtonText;
            CarId = 0;
            Make = string.Empty;
            Model = string.Empty;
            Vin = string.Empty;
        }
        private async Task ShowAlert(string message)
        {
            await Shell.Current.DisplayAlert("Info", message, "Ok");
        }
    }
}
