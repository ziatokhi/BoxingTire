using BoxingTire.App.Models;
using BoxingTire.App.Views;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace BoxingTire.App.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        
        private bool _IsBusy = false;
        private string _UserName;
        private string _Password;

        public ICommand LoginCommand { get; }
        public ICommand SignUpCommand { get; }

        private string _DisplayMessage;
        public string DisplayMessage
        {
            get
            {
                return _DisplayMessage;
            }
            set
            {
                _DisplayMessage = value;
                OnPropertyChanged();
            }
        }

        private string _DisplayMessageColor;
        public string DisplayMessageColor
        {
            get
            {
                return _DisplayMessageColor;
            }
            set
            {
                _DisplayMessageColor = value;
                OnPropertyChanged();
            }
        }
        public string UserName
        {
            get
            {
                return _UserName;
            }
            set
            {
                _UserName = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get
            {
                return _Password;
            }
            set
            {
                _Password = value;
                OnPropertyChanged();
            }
        }

        public Boolean IsBusy
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

        public LoginViewModel()
        {
            

            LoginCommand = new Command(Login_Click);
            SignUpCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PushModalAsync(new SignUpPage()); ;
            });

            //check if user already login  auto login user
           // DirectLoginUser();
        }

        private void DirectLoginUser()
        {
            using (var db = new BoxingTireDbContext())
            {
                if (db.UserAccount.Where(x => x.IsLogin == true).FirstOrDefault().IsLogin)
                {
                    Application.Current.MainPage = new AppShell();
                }
            }
        }

        private void Login_Click(object obj)
        {
            IsBusy = true;
            using (var db = new BoxingTireDbContext())
            {
                if (db.UserAccount.Where(x => x.EmailAddress ==UserName && x.Password ==Password && x.IsEnable ==true).Count() > 0)
                {
                    Application.Current.MainPage = new AppShell();
                }
                else
                {
                    DisplayMessage = "Login Failed Please Check Email Or Password?";
                    DisplayMessageColor = "#ff0000"; // Red Color
                }
            }
            IsBusy =false;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}