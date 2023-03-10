using CarListApp.Maui.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CarListApp.Maui.Services;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace CarListApp.Maui.ViewModels
{
    // double héritage pour CarDetailsViewModel
    [QueryProperty(nameof(Id), nameof(Id))]
    public partial class CarDetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly CarApiService carApiService;

        // Le ctor prend une instance de CarApiService pour récupérer les données de car
        public CarDetailsViewModel(CarApiService carApiService)
        {
            this.carApiService = carApiService;
        }

        // je stock l'état de la connectivité du réseau
        NetworkAccess accessType = Connectivity.Current.NetworkAccess;

        [ObservableProperty] // si modif on notifie
        Car car;

        [ObservableProperty] // si modif on notifie
        int id;

        // Je récup l'id de car avec l'URL grâce à IQueryAttributable au travers de ApplyQueryAttributes
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            Id = Convert.ToInt32(HttpUtility.UrlDecode(query["Id"].ToString()));
        }


        public async Task GetCarData()
        {
            if (accessType == NetworkAccess.Internet)
            {
                Car = await carApiService.GetCar(Id); //si réseau on utilise l'API
            }
            else
            {
                Car = App.CarDatabaseService.GetCar(Id); //sinon => db
            }
        }
    }
}

