using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace BoxingTire.App.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private string _Message;
        private bool _IsBusy = false;
        private string _UserName;
        private string _Password;

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
        }

        private void Login_Click(object obj)
        {
            IsBusy = true;



        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}