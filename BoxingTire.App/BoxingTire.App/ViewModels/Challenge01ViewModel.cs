using BoxingTire.App.Models;
using BoxingTire.App.Services.Helpers;
using Microsoft.EntityFrameworkCore;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BoxingTire.App.ViewModels
{
    public class Challenge01ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

      
        public static Guid DeviceInformationServiceId = new Guid("0000180A00001000800000805F9B34FB");
        public static Guid TemperatureServiceId = new Guid("E95D6100251D470AA062FA1922DFA9A8");
        public static Guid AccelerometerServiceId = new Guid("E95D0753251D470AA062FA1922DFA9A8");
        public static Guid ButtonServiceId = new Guid("E95D9882251D470AA062FA1922DFA9A8");
        public static Guid MagnetometerServiceId = new Guid("E95DF2D8251D470AA062FA1922DFA9A8");
        public static Guid LedServiceId = new Guid("E95DD91D251D470AA062FA1922DFA9A8");

        private static Guid AccelerometerCharacteristicId = new Guid("E95DCA4B251D470AA062FA1922DFA9A8");
        private static Guid AccelerometerPeriodCharacteristicId = new Guid("E95DFB24251D470AA062FA1922DFA9A8");

        private IList<ICharacteristic> CharacteristicsToUpdate = new List<ICharacteristic>();
        private HashSet<Guid> SeenCharacteristics = new HashSet<Guid>();
        private double _x = double.NaN;

        private int _ChallengeCategory_Id = App.ChallengeCategory_Id;

        private DateTime _StartTime;

        public Double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                //   OnPropertyChanged();
            }
        }

        private double _y = double.NaN;

        public Double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                //    OnPropertyChanged();
            }
        }

        private double _z = double.NaN;

        public Double Z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value;
                //     OnPropertyChanged();
            }
        }

        private int _accelerometerPeriod = int.MinValue;

        public int AccelerometerPeriod
        {
            get
            {
                return _accelerometerPeriod;
            }
            set
            {
                _accelerometerPeriod = value;
                //   OnPropertyChanged();
            }
        }

      //  public Accelerometer _Accelerometer;

        public ICommand Start_Stop_Command { get; private set; }

        public ICommand SoundCommnd { get; private set; }
        public ICommand VibrateCommnd { get; private set; }

        public bool IsSound = false;
        public bool IsVibrate = false;


        private bool _isRunning = false;

        private bool _IsBluetooth_Connected = false;

        public int _Punch_Force = 0;

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

        public int _Punch_Speed { get; set; } = 0;

        public int Punch_Speed
        {
            set
            {
                _Punch_Speed = value;

                OnPropertyChanged();
            }
            get
            {
                return _Punch_Speed;
            }
        }

        public int _Punch_Count = 0;

        public int Punch_Count
        {
            set
            {
                _Punch_Count = value;

                OnPropertyChanged();
            }
            get
            {
                return _Punch_Count;
            }
        }


        public Challenge01ViewModel()
        {

            _StartTime = DateTime.Now;

            _ChallengeCategory_Id = App.ChallengeCategory_Id;
            //Title = "Challenge";
            Start_Stop_Command = new Command(Start_Stop_Click);
           VibrateCommnd = new Command(Vibrate_click);
            SoundCommnd = new Command(Sound_click);

        }

        private void Sound_click(object obj)
        {
            Button btn = obj as Button;
            if (IsSound == false)
            {
                IsSound = true;
                btn.BackgroundColor = Color.Blue;
            }
            else
            {
                IsSound = false;
                btn.BackgroundColor = Color.Black;

            }
        }

        private void Vibrate_click(object obj)
        {
            Button btn = obj as Button;
            if (IsVibrate == false)
            {
                IsVibrate = true;
                btn.BackgroundColor = Color.Blue ;
            }
            else
            {
                IsVibrate =false;
                btn.BackgroundColor = Color.Black;

            }
        }

        private void Start_Stop_Click(object obj)
        {
            Button btn = obj as Button;
            if (btn.Text == "Start")
            {
                LoadCharacteristics();

                btn.Text = "Stop";
                IsRunning = true;
            }
            else
            {

                btn.Text = "Start";
                SaveData();
                IsRunning = false;
            }
        }


     async   void  SaveData()
        {

            //save data to database ;
            if (Punch_Count + Punch_Force + Punch_Speed > 0)
            { 
                using (var db = new BoxingTireDbContext())
                {

                    db.UserScore.Add(new UserScore
                    {
                        ChallengeCategory_Id = _ChallengeCategory_Id,
                         Date =DateTime.Now,
                          Force = Punch_Force,
                           PunchCount = Punch_Count,
                            Speed  = Punch_Speed,
                             StartTime = _StartTime,
                              StopTime = DateTime.Now,
                               Id = Guid.NewGuid(),
                               UserAccount_Id = App.UserId,

                    }) ;
                 await  db.SaveChangesAsync();

                 await   App.Current.MainPage.DisplayAlert("Training", $"You current Session Punchs {Punch_Count} Avg-Force {Punch_Force} Avg-Speed {Punch_Speed}", "Ok");
                }

        }

        }


        public async void LoadCharacteristics()
        {

            try
            {
                var s = await App.microbit.GetServicesAsync();

                var ServiceInstance = s.Where(x => x.Id == AccelerometerServiceId).FirstOrDefault();

                // var ServiceInstance = await a.GetCharacteristicsAsync();

                IEnumerable<ICharacteristic> characteristics = await ServiceInstance.GetCharacteristicsAsync();
                foreach (ICharacteristic characteristic in characteristics)
                {
                    if (characteristic.Id == AccelerometerCharacteristicId)
                    {
                        characteristic.ValueUpdated += (sender, e) =>
                        {
                            byte[] rawBytes = e.Characteristic.Value;
                            if (rawBytes.Length != 6)
                                throw new InvalidDataException("Accelerometer characteristic should have 6 bytes");

                            short rawX = ConversionHelpers.ByteArrayToShort16BitLittleEndian(new byte[] { rawBytes[0], rawBytes[1] });
                            short rawY = ConversionHelpers.ByteArrayToShort16BitLittleEndian(new byte[] { rawBytes[2], rawBytes[3] });
                            short rawZ = ConversionHelpers.ByteArrayToShort16BitLittleEndian(new byte[] { rawBytes[4], rawBytes[5] });

                            X = ((double)rawX) / 1000.0;
                            Y = ((double)rawY) / 1000.0;
                            Z = ((double)rawZ) / 1000.0;




                            if (X > 1)
                            {
                                Punch_Count++;
                                Punch_Speed = 0;
                                Punch_Force = 0;
                                if (IsVibrate == true)
                                {
                                    Device.BeginInvokeOnMainThread(() =>
                                    {
                                        Vibration.Vibrate(rawX);
                                    });
                                }



                            }






                            Debug.Write($"{X} Y{Y} Z{Z}");
                            Debug.Write($"RAW X {rawX} Y{rawY} Z{rawZ}");
                        };
                        //   MarkCharacteristicForUpdate(characteristic);
                        await characteristic.StartUpdatesAsync();
                        //await Task.Delay(500);
                    }
                    else if (characteristic.Id == AccelerometerPeriodCharacteristicId)
                    {
                        try
                        {
                            byte[] val = await characteristic.ReadAsync();
                            int period = ConversionHelpers.ByteArrayToShort16BitLittleEndian(val);
                            AccelerometerPeriod = period;
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }

                // StartUpdates();

            }
            catch (Exception ex)
            {

                Debug.Write($"Error: {ex.Message}");
            }
        }

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}