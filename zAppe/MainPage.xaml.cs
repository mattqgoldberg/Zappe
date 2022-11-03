using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Plugin.BLE.Abstractions.Exceptions;
using Plugin.BLE.Abstractions.Extensions;
using Xamarin.Forms;

namespace zAppe
{
    public partial class MainPage : ContentPage
    {
        private List<IDevice> _gattDevices = new List<IDevice>();
        IBluetoothLE ble;
        IAdapter _adapter;
        public MainPage()
        {
            
            InitializeComponent();
            ble = CrossBluetoothLE.Current;
            _adapter = CrossBluetoothLE.Current.Adapter;
            BluetoothState State = ble.State;
            state.Text = State.ToString();

            ble.StateChanged += (s, e) =>
            {
                State = ble.State;
                state.Text = "Bluetooth is " + State.ToString();                
            };

            _adapter.DeviceDiscovered += (obj, a) =>
            {
                _gattDevices.Add(a.Device);
            };  
        }

        private async void ConnectButton_Clicked(object sender, EventArgs e)
        {
            try
            {
                await _adapter.ConnectToKnownDeviceAsync(Guid.Parse("" +
                    "8E2EBC8D-2B02-9EDC-6A90-DE4CE0BC9659"));
                connectionStatus.Text = "Connected";

            }
            catch
            {
                //could not connect
            }

        }

        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            try
            {
                if (_adapter.ConnectedDevices.Count > 0)
                {
                    for (int i = 0; i < _adapter.ConnectedDevices.Count; i++)
                    {
                        
                        if (_adapter.ConnectedDevices[i].Name == "SH-HC-08")
                        {
                            
                            try
                            {
                                await _adapter.ConnectedDevices[i].GetServicesAsync();
                            }
                            catch
                            {
                                testing.Text += "here";
                            }

                            testing.Text += "A";
                            var service = await _adapter.ConnectedDevices[i].
                                GetServiceAsync(Guid.Parse("0000ffe0-0000-1000-8000-00805f9b34fb"));
                            testing.Text += "b";
                            var characteristic = await service.
                                GetCharacteristicAsync(Guid.Parse("0000ffe1-0000-1000-8000-00805f9b34fb"));
                           

                      
                            
                            byte[] onByte = new byte[1];
                            byte[] offByte = new byte[1];
                            onByte[0] = 72;
                            offByte[0] = 0;
                            if (theSwitch.IsToggled)
                            {
                                testing.Text += "gg";
                                await characteristic.WriteAsync(onByte);
                            }
                            else
                            {
                                testing.Text += "gg";
                                await characteristic.WriteAsync(offByte);

                            }
                        }
                    }
                }
            }
            catch
            {
                testing.Text += "L";
            }
        }
    }
}
