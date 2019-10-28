using BoxingTire.App.Views;
using Plugin.BLE.Abstractions.Contracts;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace BoxingTire.App.ViewModels
{
    public class DeviceListViewModel : INotifyPropertyChanged
    {
      
        private IBluetoothLE _bluetoothLe;
        private IAdapter _adapter;
        public ICommand DeviceCommand { get; private set; }
        public ObservableCollection<IDevice> DeviceList { get; }
        private bool _IsBusy = false;

        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                OnPropertyChanged();
            }
        }

        public DeviceListViewModel()
        {
            //Title = "Challenges List";
            DeviceCommand = new Command(DeviceClick);
            DeviceList = new ObservableCollection<IDevice>();
            _bluetoothLe = Plugin.BLE.CrossBluetoothLE.Current;

            foreach (IDevice device in _bluetoothLe.Adapter.GetSystemConnectedOrPairedDevices())
            {
                DeviceList.Add(device);
            }
        }

        private async void DeviceClick(object obj)
        {
            App._microbit = obj as IDevice;
            await _bluetoothLe.Adapter.ConnectToDeviceAsync(App._microbit);

            await Application.Current.MainPage.Navigation.PopModalAsync();
        }

        /// <summary>
        /// used to instill two-way data binding between a viewmodel's properties and a given view object.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}