using BoxingTire.App.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BoxingTire.App.ViewModels
{
    public class StatusViewModel : INotifyPropertyChanged
    {
        public StatusViewModel()
        {

            try
            {


                using (var db = new BoxingTireDbContext())
                {
                    var UserId = App.UserId;

                    TotalPunch = db.UserScore.Where(x => x.UserAccount_Id == UserId).Sum(x => x.PunchCount);

                    TotalTrainingTime = long.Parse(db.UserScore.Where(x => x.UserAccount_Id == UserId).Sum(x => (x.StopTime - x.StartTime).TotalMinutes).ToString());
                    
                    
                    AvgForce = int.Parse(db.UserScore.Where(x => x.UserAccount_Id == UserId).DefaultIfEmpty().Average(x => x.Force).ToString());

                    AvgSpeed = int.Parse(db.UserScore.Where(x => x.UserAccount_Id == UserId).DefaultIfEmpty().Average(x => x.Speed).ToString());

                }
            }
            catch(Exception ex)
            {
           //     App.Current.MainPage.DisplayAlert("Error", ex.Message, "Ok");

            }
             
        }

        private long _TotalTrainingTime = 0;

        public long TotalTrainingTime
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