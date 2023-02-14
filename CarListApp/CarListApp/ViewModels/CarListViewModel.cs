using CarListApp.Models;
using CarListApp.Services;
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
        private readonly CarServices carService;
        public ObservableCollection<Car> Cars
        {
            get; private set;
        } = new();
        public CarListViewModel(CarServices carService)
        {
            Title = "Car List";
            this.carService = carService;
        }

        [RelayCommand] //using System.Windows.Input;
        async Task GetCarList()
        {
            if (IsLoading) return;
            try
            {
                IsLoading = true;
                if (Cars.Any()) Cars.Clear();

                var cars = carService.GetCars();
                foreach (var car in cars)
                {
                    Cars.Add(car);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"impossible d'afficher les voitures : {ex.Message}");
                await Shell.Current.DisplayAlert("Erreur", "Impossible de récupérer la liste de voitures", "Ok");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}
