using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiSolar

{
    internal class Solar
    {
        private int[] _analogValues;
        private float _panelVoltage;
        private float _panelCurrent;
        private float _batteryVoltage;
        private float _batteryCurrent;
        private string _batteryStatus;
        private float _redLEDCurrent;
        private float _greenLEDCurrent;

        public Solar() { }

        // ----- Encapsulations ----- //
        /// <summary>
        /// The panel voltage
        /// </summary>
        public float PanelVoltage { get => _panelVoltage; }
        /// <summary>
        /// the panel current
        /// </summary>
        public float PanelCurrent { get => _panelCurrent; }
        /// <summary>
        /// The battery/super cap voltage.
        /// </summary>
        public float BatteryVoltage { get => _batteryVoltage; }
        /// <summary>
        /// The battery/super cap current. Positive indicates charging.
        /// </summary>
        public float BatteryCurrent { get => _batteryCurrent; set => _batteryCurrent = value; }
        /// <summary>
        /// The charging status of the battery.
        /// </summary>
        public string BatteryStatus { get => _batteryStatus; set => _batteryStatus = value; }
        /// <summary>
        /// The current through the red LED.
        /// </summary>
        public float RedLEDCurrent { get => _redLEDCurrent; set => _redLEDCurrent = value; }
        /// <summary>
        /// The current through the green LED.
        /// </summary>
        public float GreenLEDCurrent { get => _greenLEDCurrent; set => _greenLEDCurrent = value; }
        /// <summary>
        /// Analog values from the meadow board.
        /// </summary>
        public int[] AnalogValues { get => _analogValues; set => _analogValues = value; }
    }
}
