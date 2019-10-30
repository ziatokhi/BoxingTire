using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BoxingTire.App.ViewModels
{
    public class StatusViewModel : INotifyPropertyChanged
    {
        public StatusViewModel()
        {



        }

        private int _TotalTrainingTime = 0;

        public int TotalTrainingTime
        {
            get
            {
                return _TotalTrainingTime;
            }
            set
            {
                _TotalTrainingTime = value;
                OnPropertyChanged();
            }
        }

        private int _TotalPunch = 0;

        public int TotalPunch
        {
            get
            {
                return _TotalPunch;
            }
            set
            {
                _TotalPunch = value;
                OnPropertyChanged();
            }
        }

        private int _AvgForce = 0;

        public int AvgForce
        {
            get
            {
                return _AvgForce;
            }
            set
            {
                _AvgForce = value;
                OnPropertyChanged();
            }
        }

        private int _AvgSpeed = 0;

        public int AvgSpeed
        {
            get
            {
                return _AvgSpeed;
            }
            set
            {
                _AvgSpeed = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}