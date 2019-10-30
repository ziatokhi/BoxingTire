using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
 
using BoxingTire.App.Views;
using BoxingTire.App.ViewModels;
using Plugin.BLE.Abstractions.Contracts;

namespace BoxingTire.App
{
    public partial class App : Application
    {
        public static IDevice _microbit;
        public static Guid  UserId;
        public App()
        {
            InitializeComponent();

            //      DependencyService.Register<MockDataStore>();
              MainPage = new AppShell();


            //   MainPage = new Challenge01();
            //  MainPage = new MasterPageMenu();

              MainPage = new Login() ;



            // MainPage = new AccelerometerPage(x.);

            //   MainPage = new DeviceListPage();

          //  MainPage = new test();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
