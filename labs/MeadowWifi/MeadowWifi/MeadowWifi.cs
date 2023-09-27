using System;
using System.IO;
using System.Threading.Tasks;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Leds;
using Meadow.Peripherals.Leds;
using Meadow.Foundation.Displays;
using Meadow.Peripherals.Displays;
using YamlDotNet.Serialization;

namespace MeadowWifi
{
    public class MeadowConfigFile
    {
        public Device Device { get; set; } = new Device();
        public Coprocessor Coprocessor { get; set; } = new Coprocessor();
        public Network Network { get; set; } = new Network();
    }
    public class Device
    {
        public string Name { get; set; } = "MeadowWifi";
    }
    public class Coprocessor
    {
        public bool AutomaticallyStartNetwork { get; set; } = true;
        public bool AutomaticallyReconnect { get; set; } = true;
        public int MaximumRetryCount { get; set; } = 7;
    }
    public class Network
    {
        public int GetNetworkTimeAtStartup { get; set; } = 1;

        public int NtpRefreshPeriod { get; set; } = 600;

        public string[] NtpServers { get; set; } =
        {
            "0.pool.ntp.org",
            "1.pool.ntp.org",
            "2.pool.ntp.org",
            "3.pool.ntp.org",
        };

        public string[] DnsServers { get; set; } =
        {
            "1.1.1.1",
            "8.8.8.8"
        };
    }

    public class WifiConfigFile
    {
        public Credentials Credentials { get; set; }

        public WifiConfigFile(string ssid, string password)
        {
            Credentials = new Credentials()
            {
                Ssid = ssid,
                Password = password
            };
        }
    }
    public class Credentials
    {
        public string Ssid { get; set; }
        public string Password { get; set; }
    }

    public static class ConfigFileManager
    {
        public static void CreateConfigFiles(string ssid, string password)
        {
            CreateMeadowConfigFile();
            CreateWifiConfigFile(ssid, password);
        }

        public static void DeleteConfigFiles()
        {
            DeleteMeadowConfigFile();
            DeleteWifiConfigFile();
        }

        private static void CreateMeadowConfigFile()
        {
            try
            {
                var configFile = new MeadowConfigFile();

                var serializer = new SerializerBuilder().Build();
                var yaml = serializer.Serialize(configFile);

                using (var fs = File.CreateText(Path.Combine(MeadowOS.FileSystem.UserFileSystemRoot, "meadow.config.yaml")))
                {
                    fs.WriteLine(yaml);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void CreateWifiConfigFile(string ssid, string password)
        {
            try
            {
                var configFile = new WifiConfigFile(ssid, password);

                var serializer = new SerializerBuilder().Build();
                var yaml = serializer.Serialize(configFile);

                using (var fs = File.CreateText(Path.Combine(MeadowOS.FileSystem.UserFileSystemRoot, "wifi.config.yaml")))
                {
                    fs.WriteLine(yaml);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void DeleteMeadowConfigFile()
        {
            try
            {
                File.Delete($"{MeadowOS.FileSystem.UserFileSystemRoot}meadow.config.yaml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        private static void DeleteWifiConfigFile()
        {
            try
            {
                File.Delete($"{MeadowOS.FileSystem.UserFileSystemRoot}wifi.config.yaml");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }

    public class DisplayController
    {
        private static readonly Lazy<DisplayController> instance =
            new Lazy<DisplayController>(() => new DisplayController());
        public static DisplayController Instance => instance.Value;

        MicroGraphics graphics;

        public DisplayController()
        {
            Initialize();
        }

        void Initialize()
        {
            var display = new (
                i2cBus: MeadowApp.Device.CreateI2cBus(),
                displayType: Ssd130xBase.DisplayType.OLED128x64);

            graphics = new MicroGraphics(display)
            {
                Stroke = 1,
                CurrentFont = new Font8x12(),
            };

            UpdateStatus();
        }

        public void UpdateStatus()
        {
            graphics.Clear();
            graphics.DrawText(3, 3, "Bluetooth:");
            graphics.DrawText(7, 18, "Not Paired");
            graphics.DrawText(3, 33, "WIFI:");
            graphics.DrawText(7, 48, "Disconnected");
            graphics.Show();
        }

        public void UpdateBluetoothStatus(string status)
        {
            graphics.DrawRectangle(3, 18, 122, 12, false, true);
            graphics.DrawText(7, 18, status);
            graphics.Show();
        }

        public void UpdateWifiStatus(string status)
        {
            graphics.DrawRectangle(3, 48, 122, 12, false, true);
            graphics.DrawText(7, 48, status);
            graphics.Show();
        }
    }

}
