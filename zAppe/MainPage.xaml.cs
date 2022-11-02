using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.BLE;
using Plugin.BLE.Abstractions.Contracts;
using Xamarin.Forms;

namespace zAppe
{
    public partial class MainPage : ContentPage
    {
        IBluetoothLE ble;
        IAdapter adapter;
        public MainPage()
        {
            InitializeComponent();
            ble = CrossBluetoothLE.Current;
            BluetoothState State = ble.State;
            state.Text = State.ToString();

            ble.StateChanged += (s, e) =>
            {
                State = ble.State;
                state.Text = State.ToString(); ;
            };
        }


    }
}
