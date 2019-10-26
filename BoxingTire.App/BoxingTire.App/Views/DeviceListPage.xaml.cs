using BoxingTire.App.ViewModels;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoxingTire.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DeviceListPage : ContentPage
    {
        private DevicesViewModel vm;
        private bool firstTime = true;

        public DeviceListPage()
        {
            InitializeComponent();
            vm = new DevicesViewModel();
            BindingContext = vm;

            DevicesList.ItemSelected +=   (object sender, SelectedItemChangedEventArgs e) =>
            {
                if (e.SelectedItem == null)
                    return;

                IDevice selectedDevice = e.SelectedItem as IDevice;
               Application.Current.MainPage= new NavigationPage(new DeviceServicesPage(selectedDevice));
                ((ListView)sender).SelectedItem = null;
            };

            InstructionsButton.Clicked += (sender, e) =>
            {
                Navigation.PushAsync(new InstructionsPage());
            };
        }

        protected override async void OnAppearing()
        {
            try
            {
                var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
                if (status != PermissionStatus.Granted)
                {
                    if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
                    {
                        await DisplayAlert("Need location", "Using Bluetooth LE needs location permission.", "OK");
                    }

                    var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
                    status = results[Permission.Location];
                }

                if (status != PermissionStatus.Granted)
                {
                    await DisplayAlert("Could not get location permission", "Using Bluetooth LE requires Location permission. Please grant Location permission to this app from the settings and try again.", "OK");
                    return;
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Exception", ex.ToString(), "OK");
            }

            if (firstTime)
            {
                firstTime = false;
                vm.StartScanning();
            }
            else
            {
                vm.UpdateList();
            }
        }

        protected override void OnDisappearing()
        {
            vm.StopScanning();
        }
    }
}

