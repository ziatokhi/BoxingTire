using BoxingTire.App.ViewModels;
using Plugin.BLE.Abstractions.Contracts;
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
    public partial class DeviceServicesPage : ContentPage
    {
        private DeviceServicesViewModel vm;

        public DeviceServicesPage(IDevice device)
        {
            InitializeComponent();

            vm = new DeviceServicesViewModel(device);

            BindingContext = vm;

            ServicesList.ItemSelected += ServicesList_ItemSelected;
        }

        async void ServicesList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            IMicrobitServiceProvider serviceProvider = e.SelectedItem as IMicrobitServiceProvider;

          App.IMicrobitServiceProvider_Active = serviceProvider;

          //  App.Current.MainPage = new AppShell();
            await Navigation.PushAsync(serviceProvider.GetServiceInstance().Page);
           ((ListView)sender).SelectedItem = null;
        }
    }
}

