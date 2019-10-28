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
    public partial class Challenge01 : ContentPage
    {
        
           
     

        public Challenge01()
        {
            InitializeComponent();
           
        }

        protected override void OnAppearing()
        {
          //  _service.LoadCharacteristics();
        }

        protected override void OnDisappearing()
        {
          //  _service.StopUpdates();
        }
    }
}