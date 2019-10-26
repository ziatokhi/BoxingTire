using BoxingTire.App.Services;
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
    public partial class DeviceInformationPage : ContentPage
    {
        private DeviceInformationService _service;

        public DeviceInformationPage(DeviceInformationService service)
        {
            InitializeComponent();
            _service = service;
            BindingContext = _service;
        }

        protected override void OnAppearing()
        {
            _service.LoadCharacteristics();
        }
    }
}

