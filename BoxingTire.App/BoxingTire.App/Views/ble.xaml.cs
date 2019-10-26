using Plugin.BluetoothLE;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BoxingTire.App.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ble : ContentPage
    {
        public static String ACCELEROMETERSERVICE_SERVICE_UUID = "E95D0753251D470AA062FA1922DFA9A8";
        public static String ACCELEROMETERDATA_CHARACTERISTIC_UUID = "E95DCA4B251D470AA062FA1922DFA9A8";
        public static String ACCELEROMETERPERIOD_CHARACTERISTIC_UUID = "E95DFB24251D470AA062FA1922DFA9A8";

        private List<IDevice> deviceList = new List<IDevice>();
        private IDevice device;

        public ble()
        {
            InitializeComponent();

            Paired_Device();
        }

        private void Paired_Device()
        {
            var d = CrossBleAdapter.Current.GetPairedDevices().ToList();

            var x = CrossBleAdapter.Current.GetPairedDevices().Subscribe(paired =>
            {
                device = paired.FirstOrDefault();
            });
           
                
                device.WhenAnyCharacteristicDiscovered().Subscribe(ch =>
            {
                if (ch.Value != null)
                {
                    byte[] b = ch.Value;
                    byte[] x_bytes = new byte[2];
                    byte[] y_bytes = new byte[2];
                    byte[] z_bytes = new byte[2];
                    System.Array.Copy(b, 0, x_bytes, 0, 2);
                    System.Array.Copy(b, 2, y_bytes, 0, 2);
                    System.Array.Copy(b, 4, z_bytes, 0, 2);
                    //short raw_x = Convert.ToSingle (x_bytes);
                    //short raw_y = Utility.shortFromLittleEndianBytes(y_bytes);
                    //short raw_z = Utility.shortFromLittleEndianBytes(z_bytes);
                }

            });

            device.Connect();
        }

        private void ble0()
        {
            deviceList.Clear();

            CrossBleAdapter.Current.ScanExtra().Subscribe(scanResult =>
            {
                dynamic d = scanResult.Device.NativeDevice;

                var address = d.Address;

                if (!deviceList.Contains(scanResult.Device))
                {
                    deviceList.Add(scanResult.Device);
                    //   Debug.WriteLine($"e.Device.AdvertisementRecords =={System.Text.Encoding.UTF8.GetString(scanResult.AdvertisementData.ManufacturerData)}");
                    Debug.WriteLine($"New Device == {scanResult.Device.Name}");

                    CvList.ItemsSource = deviceList.ToList();
                }
            });
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            IDevice device = (IDevice)((TappedEventArgs)e).Parameter;

            device.Connect();
            device.PairingRequest("1234");
            Debug.WriteLine(device.PairingStatus);

            device.WhenAnyCharacteristicDiscovered().Subscribe(async characteristic =>
            {
                // read, write, or subscribe to notifications here
                var result = await characteristic.Read(); // use result.Data to see response
                                                          // await characteristic.Write(bytes);
                Debug.WriteLine($"e.Device.AdvertisementRecords =={System.Text.Encoding.UTF8.GetString(result.Data)}");

                await characteristic.EnableNotifications();
                //characteristic.WhenNotificationReceived().Subscribe(results => {
                //    //result.Data to get at response
                //});
            });
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            CvList.ItemsSource = deviceList.ToList();
        }
    }
}