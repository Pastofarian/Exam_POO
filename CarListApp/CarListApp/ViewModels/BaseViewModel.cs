using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CarListApp.Maui.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        string title; //si on change le titre => notification 

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotLoading))] //!! Avant appelé [AlsoNotifyChangeFor]
        bool isLoading;

        public bool IsNotLoading => !isLoading; // Determine la valeur de IsNotLoading à partir de isLoading
    }
}
