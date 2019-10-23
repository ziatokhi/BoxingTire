using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace BoxingTire.App.ViewModels
{
    public class Challenge01ViewModel : INotifyPropertyChanged
    {
        public Challenge01ViewModel()
        {
            //Title = "Challenge";
            Start_Stop_Command = new Command(Start_Stop_Click);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand Start_Stop_Command { get; private set; }

        public int _Punch_Force = 0;
        private bool _isRunning  = false;

        private bool _IsBluetooth_Connected = false;

        public int Punch_Force
        {
            set
            {
                _Punch_Force = value;

                OnPropertyChanged();
            }
            get
            {
                return _Punch_Force;
            }
        }

        public bool IsRunning
        {
            get
            {
                return _isRunning;
            }
            set
            {
                _isRunning = value;
                OnPropertyChanged();
                 
            }
        }

        public int Punch_Speed { get; set; } = 0;
        public int Punch_Count { get; set; } = 0;

        private void Start_Stop_Click(object obj)
        {
            Button btn = obj as Button;
            if (btn.Text == "Start")
            {
                btn.Text = "Stop";
                IsRunning = true;
            }
            else
            {
                btn.Text = "Start";
                IsRunning = false;
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}