

namespace MauiSolar

{
    internal class Solar
    {
        private double _batteryResistor;
        private double _ledResistor;
        private double _refVoltage;
        private double _adcResolution;
        private double[] _analogValues = new double[5];
        private double _panelVoltage;
        private double _panelCurrent;
        private double _batteryVoltage;
        private double _batteryCurrent;
        private string _batteryStatus;
        private double _redLEDCurrent;
        private double _greenLEDCurrent;

        /// <summary>
        /// Solar class constructor
        /// </summary>
        /// <param name="batteryResistor">The battery current limiting resistor value in ohms</param>
        /// <param name="ledResistor">The LED current limiting resistor value in ohms</param>
        /// <param name="refVoltage">The reference voltage used by the ADC</param>
        /// <param name="adcResolution">The resolution of the ADC (2^n - 1, where n is the number of bits of the ADC)</param>
        public Solar(double batteryResistor, double ledResistor, double refVoltage, double adcResolution)
        {
            _batteryResistor = batteryResistor;
            _ledResistor = ledResistor;
            _refVoltage = refVoltage;
            _adcResolution = adcResolution;
        }

        // ----- Encapsulations ----- //
        /// <summary>
        /// The panel voltage
        /// </summary>
        public double PanelVoltage { get => _panelVoltage; }
        /// <summary>
        /// the panel current
        /// </summary>
        public double PanelCurrent { get => _panelCurrent; }
        /// <summary>
        /// The battery/super cap voltage.
        /// </summary>
        public double BatteryVoltage { get => _batteryVoltage; }
        /// <summary>
        /// The battery/super cap current. Positive indicates charging.
        /// </summary>
        public double BatteryCurrent { get => _batteryCurrent; }
        /// <summary>
        /// The charging status of the battery.
        /// </summary>
        public string BatteryStatus { get => _batteryStatus; }
        /// <summary>   
        /// The current through the red LED.
        /// </summary>
        public double RedLEDCurrent { get => _redLEDCurrent; }
        /// <summary>
        /// The current through the green LED.
        /// </summary>
        public double GreenLEDCurrent { get => _greenLEDCurrent; }
        /// <summary>
        /// Analog values from the meadow board.
        /// </summary>
        public double[] AnalogValues
        {
            get => _analogValues;
            set
            {
                _analogValues = value;
                // convert analog values to current and voltage values
                _panelVoltage = RefVoltage * value[0] / AdcResolution;
                _batteryVoltage = RefVoltage * value[2] / AdcResolution;
                _batteryCurrent = 1000 * RefVoltage * (value[1] - value[2]) / (BatteryResistor * AdcResolution);
                _greenLEDCurrent = 1000 * RefVoltage * (value[1] - value[3]) / (AdcResolution * LedResistor);
                _redLEDCurrent = 1000 * RefVoltage * (value[1] - value[4]) / (AdcResolution * LedResistor);
                _panelCurrent = BatteryCurrent + GreenLEDCurrent + RedLEDCurrent;
                _batteryStatus = (BatteryCurrent > 0) ? "Charging" : "Discharging";
            }
        }

        public double BatteryResistor { get => _batteryResistor; set => _batteryResistor = value; }
        public double LedResistor { get => _ledResistor; set => _ledResistor = value; }
        public double RefVoltage { get => _refVoltage; set => _refVoltage = value; }
        public double AdcResolution { get => _adcResolution; set => _adcResolution = value; }
    }
}
