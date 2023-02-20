using CarListApp.Models;
using CarListApp.Services;
using CarListApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;


namespace CarListApp.ViewModels
{
    public partial class CarListViewModel : BaseViewModel
    {
        public ObservableCollection<Car> Cars
        {
            get; private set;
        } = new();
        public CarListViewModel()
        {
            Title = "Car List";
        }

        [ObservableProperty]
        bool isRefreshing;

        [RelayCommand] //using System.Windows.Input;
        async Task GetCarList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (Cars.Any()) Cars.Clear();

                var cars = App.CarServices.GetCars();

                foreach (var car in cars) Cars.Add(car);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"impossible d'afficher les voitures : {ex.Message}");
                await Shell.Current.DisplayAlert("Erreur", "Impossible de récupérer la liste de voitures", "Ok");
            }
            finally
            {
                IsLoading = false;
                IsRefreshing = false;
            }
        }
        [RelayCommand]
        async Task GetCarDetails(Car car)
        {
            if (car == null) return;

            await Shell.Current.GoToAsync(nameof(CarDetailsPage), true, new Dictionary<string, object>
            {
                {nameof(Car), car }
            });
        }
    }
}
