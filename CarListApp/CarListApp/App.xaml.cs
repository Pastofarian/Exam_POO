using CarListApp.Services;

namespace CarListApp;

public partial class App : Application
{
    public static CarServices CarServices
    {
        get; private set;
    }
    public App(CarServices carServices)
    {
        InitializeComponent();

        MainPage = new AppShell();
        CarServices = carServices;
    }
}
