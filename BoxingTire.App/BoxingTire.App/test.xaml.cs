using BoxingTire.App.Services.Helpers;
using Plugin.BLE.Abstractions.Contracts;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoxingTire.App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class test : ContentPage
    {
        public static Guid DeviceInformationServiceId = new Guid("0000180A00001000800000805F9B34FB");
        public static Guid TemperatureServiceId = new Guid("E95D6100251D470AA062FA1922DFA9A8");
        public static Guid AccelerometerServiceId = new Guid("E95D0753251D470AA062FA1922DFA9A8");
        public static Guid ButtonServiceId = new Guid("E95D9882251D470AA062FA1922DFA9A8");
        public static Guid MagnetometerServiceId = new Guid("E95DF2D8251D470AA062FA1922DFA9A8");
        public static Guid LedServiceId = new Guid("E95DD91D251D470AA062FA1922DFA9A8");


        private static Guid AccelerometerCharacteristicId = new Guid("E95DCA4B251D470AA062FA1922DFA9A8");
        private static Guid AccelerometerPeriodCharacteristicId = new Guid("E95DFB24251D470AA062FA1922DFA9A8");
     
        private IList<ICharacteristic> CharacteristicsToUpdate = new List<ICharacteristic>();
        private HashSet<Guid> SeenCharacteristics= new HashSet<Guid>();
        private double _x = double.NaN;
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

        public test()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
           // b();

            LoadCharacteristics();
        }

        async void b()
        {
            var b = Plugin.BLE.CrossBluetoothLE.Current;

            var p = b.Adapter.GetSystemConnectedOrPairedDevices().FirstOrDefault();
            await b.Adapter.ConnectToDeviceAsync(p);
            var s = await p.GetServicesAsync();

            var a = s.Where(x => x.Id == AccelerometerServiceId).FirstOrDefault();

            var c = await a.GetCharacteristicsAsync();
        }

        public async void LoadCharacteristics()
        {
            var b = Plugin.BLE.CrossBluetoothLE.Current;

            var p = b.Adapter.GetSystemConnectedOrPairedDevices().FirstOrDefault();
            await b.Adapter.ConnectToDeviceAsync(p);
            var s = await p.GetServicesAsync();

            var ServiceInstance = s.Where(x => x.Id == AccelerometerServiceId).FirstOrDefault();

            // var ServiceInstance = await a.GetCharacteristicsAsync();


            IEnumerable<ICharacteristic> characteristics = await ServiceInstance.GetCharacteristicsAsync();
            foreach (ICharacteristic characteristic in characteristics)
            {
                if (characteristic.Id == AccelerometerCharacteristicId)
                {
                    characteristic.ValueUpdated += (sender, e) =>
                    {
                        try
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

                                Debug.Write($"{X} Y{Y} Z{Z}");
                        }
                        catch(Exception ex)
                        {


                        }
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
                        AccelerometerPeriod = 1;
                    }
                    catch (Exception ex)
                    {


                    }
                }
            }

           // StartUpdates();
           
        }

        public void MarkCharacteristicForUpdate(ICharacteristic characteristic)
        {
            if (characteristic == null || SeenCharacteristics.Contains(characteristic.Id))
                return;

            CharacteristicsToUpdate.Add(characteristic);
        }
        public async void StartUpdates()
        {
            foreach (ICharacteristic characteristic in CharacteristicsToUpdate.ToList())
            {
                await characteristic.StartUpdatesAsync();

                // This is due to a bug in the BLE library: https://github.com/xabre/xamarin-bluetooth-le/issues/64
                await Task.Delay(500);
            }
        }

        public async void StopUpdates()
        {
            foreach (ICharacteristic characteristic in CharacteristicsToUpdate)
            {
                await characteristic.StopUpdatesAsync();

                // This is due to a bug in the BLE library: https://github.com/xabre/xamarin-bluetooth-le/issues/64
                await Task.Delay(300);
            }
        }

      
    }
}