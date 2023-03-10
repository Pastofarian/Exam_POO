//using AndroidX.Lifecycle;
using CarListApp.Maui.ViewModels;

namespace CarListApp.Maui.Views;

public partial class CarDetailsPage : ContentPage
{
    private readonly CarDetailsViewModel carDetailsViewModel;

    public CarDetailsPage(CarDetailsViewModel carDetailsViewModel)
    {
        InitializeComponent(); //génér automatiquement (affiche les éléments dans le xaml)
        BindingContext = carDetailsViewModel;
        this.carDetailsViewModel = carDetailsViewModel;
    }

    protected override async void OnAppearing() //est appelé par MAUI à chaque fois qu'on charge une nouvelle page
    {
        base.OnAppearing(); //onAppering sur la classe parent
        await carDetailsViewModel.GetCarData(); //on récup les données
    }
}