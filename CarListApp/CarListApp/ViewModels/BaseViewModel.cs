using CommunityToolkit.Mvvm.ComponentModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;


namespace CarListApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject //autre partie de la classe BaseViewModel dans le toolkit MVVM
    {
        [ObservableProperty]
        string title;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(isNotLoading))]
        bool isLoading;

        public bool isNotLoading => !isLoading;

        //nuget package community toolkit MVVM fait le boulot :)
        //    public bool IsBusy
        //    {
        //        get => _isBusy;
        //        set
        //        {
        //            if (_isBusy == value)
        //                return;
        //            _isBusy = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //    public string Title
        //    {
        //        get => _title;
        //        set
        //        {
        //            if (_title == value)
        //                return;
        //            _title = value;
        //            OnPropertyChanged();
        //        }
        //    }
        //    public event PropertyChangedEventHandler PropertyChanged;

        //    public void OnPropertyChanged([CallerMemberName] string name = null)
        //    {
        //        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        //    }
    }
}
