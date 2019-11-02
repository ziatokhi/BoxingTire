using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
 
using BoxingTire.App.Views;
using BoxingTire.App.ViewModels;
using Plugin.BLE.Abstractions.Contracts;
using BoxingTire.App.Models;
using System.Linq;
 

namespace BoxingTire.App
{
    public partial class App : Application
    {
        public static IDevice microbit;
        public static Guid  UserId;
        public static Accelerometer Calibrated_Accelerometer;
        public static string  DbPath;
        public static int ChallengeCategory_Id;

        public App()
        {
            InitializeComponent();

            //      DependencyService.Register<MockDataStore>();
            //  MainPage = new AppShell();

            //    MainPage = new Challenge01();
            //  MainPage = new MasterPageMenu();

              DirectLoginUser();

       //    MainPage = new SoundPage(); 
                //    MainPage = new LoginPage();

            // MainPage = new SignUpPage();

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

        private void DirectLoginUser()
        {

            bool IsLogin = false;
            using (var db = new BoxingTireDbContext())
            {
              foreach( var User in db.UserAccount.Where(x => x.IsLogin == true).ToList())
                {
                    MainPage = new AppShell();
                    IsLogin = User.IsLogin;

                    UserId = User.Id;
                }
            }

            if(IsLogin ==false)
            {

                MainPage = new LoginPage();
            }
        }
    }
}
