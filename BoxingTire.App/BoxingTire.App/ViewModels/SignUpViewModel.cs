using BoxingTire.App.Models;

using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace BoxingTire.App.ViewModels
{
    public class SignUpViewModel : INotifyPropertyChanged
    {
       
        
        private bool _IsBusy = false;
        private string _UserName ;
        private string _Password ;
        private string _PasswordMatch;

        public ICommand SignUpCommand { get; }
        public ICommand LoginCommand { get; }

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

        public string PasswordMatch
        {
            get
            {
                return _PasswordMatch;
            }
            set
            {
                _PasswordMatch = value;

                DisplayMessage = (Password != PasswordMatch) ? "Please Match Password" : "Password Match";
                DisplayMessageColor = (Password != PasswordMatch) ? "#ff0000" : "#002fff";
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

        public SignUpViewModel()
        {
            SignUpCommand = new Command(SignUp_ClickAsync);
            LoginCommand = new Command(() =>
            {
                Application.Current.MainPage.Navigation.PopModalAsync();
            });
        }

        private async void SignUp_ClickAsync(object obj)
        {
            IsBusy = true;

            if ( !String.IsNullOrEmpty(UserName) &&
                !String.IsNullOrEmpty(Password) &&
                !String.IsNullOrEmpty(PasswordMatch) &&



                UserName.Length > 2 && Password == PasswordMatch)
            {
                using (var db = new BoxingTireDbContext())
                {
                    await db.AddAsync(
                        new UserAccount
                        {
                            EmailAddress = UserName,
                            Id = Guid.NewGuid(),
                            IsEnable = true,
                            IsLogin = true,
                            Name = "Name",
                            Password = Password,
                            Role = 1,
                            UpdatedOn = DateTime.Now
                        });

                    // Succsfull saved
                    if (await db.SaveChangesAsync() > 0)
                    {
                        IsBusy = false;
                        Application.Current.MainPage = new AppShell();
                    }
                    else
                    {
                        DisplayMessage = "Save failed";
                    }
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