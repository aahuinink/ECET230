using System;
using System.Threading.Tasks;
using Meadow;
using Meadow.Devices;
using Meadow.Foundation;
using Meadow.Foundation.Displays;
using Meadow.Foundation.Graphics;
using Meadow.Foundation.Leds;
using Meadow.Foundation.Sensors.Rotary;
using Meadow.Hardware;
using Meadow.Peripherals.Leds;
using Meadow.Peripherals.Sensors.Rotary;
using Meadow.Units;

namespace MeadowEdgeASketch
{
    // public class MeadowApp : App<F7FeatherV1> <- If you have a Meadow F7v1.*
    public class MeadowApp : App<F7FeatherV2>
    {
        int x, y;
        MicroGraphics graphics;
        RotaryEncoderWithButton rotaryX;
        RotaryEncoderWithButton rotaryY;

        public override Task Initialize()
        {
            var onboardLed = new RgbPwmLed(
                redPwmPin: Device.Pins.OnboardLedRed,
                greenPwmPin: Device.Pins.OnboardLedGreen,
                bluePwmPin: Device.Pins.OnboardLedBlue);
            onboardLed.SetColor(Color.Red);

            var config = new SpiClockConfiguration(
                speed: new Frequency(48000, Frequency.UnitType.Kilohertz),
                mode: SpiClockConfiguration.Mode.Mode3);
            var spiBus = Device.CreateSpiBus(
                clock: Device.Pins.SCK,
                copi: Device.Pins.MOSI,
                cipo: Device.Pins.MISO,
                config: config);
            var st7789 = new St7789(
                spiBus: spiBus,
                chipSelectPin: null,
                dcPin: Device.Pins.D01,
                resetPin: Device.Pins.D00,
                width: 240, height: 240);

            graphics = new MicroGraphics(st7789);
            graphics.Clear(true);
            graphics.DrawRectangle(0, 0, 240, 240, Color.White, true);
            graphics.DrawRectangle(x, y, x+4, y+4, Color.Red);
            graphics.Show();

            x = graphics.Width / 2;
            y = graphics.Height / 2;

            rotaryX = new RotaryEncoderWithButton(
                aPhasePin: Device.Pins.A02,
                bPhasePin: Device.Pins.A01,
                buttonPin: Device.Pins.A00);
            rotaryX.Rotated += RotaryXRotated;

            rotaryY = new RotaryEncoderWithButton(
                aPhasePin: Device.Pins.D04,
                bPhasePin: Device.Pins.D03,
                buttonPin: Device.Pins.D02);
            rotaryY.Rotated += RotaryYRotated;
            rotaryY.Clicked += RotaryYClicked;

            onboardLed.SetColor(Color.Green);

            return base.Initialize();
        }

        void RotaryXRotated(object sender, RotaryChangeResult e)
        {
            if (e.New == RotationDirection.Clockwise)
                x++;
            else
                x--;

            x = Math.Clamp(x, 1, graphics.Width - 1);

            graphics.DrawPixel(x, y + 1, Color.Red);
            graphics.DrawPixel(x, y, Color.Red);
            graphics.DrawPixel(x, y - 1, Color.Red);
        }

        void RotaryYRotated(object sender, RotaryChangeResult e)
        {
            if (e.New == RotationDirection.Clockwise)
                y++;
            else
                y--;

            y = Math.Clamp(y, 1, graphics.Height - 1);

            graphics.DrawPixel(x + 1, y, Color.Red);
            graphics.DrawPixel(x, y, Color.Red);
            graphics.DrawPixel(x - 1, y, Color.Red);
        }

        void RotaryYClicked(object sender, EventArgs e)
        {
            x = graphics.Width / 2;
            y = graphics.Height / 2;

            graphics.DrawRectangle(0, 0, 240, 240, Color.White, true);
            graphics.DrawPixel(x, y, Color.Red);
        }

        public override async Task Run()
        {
            while (true)
            {
                graphics.Show();
                await Task.Delay(500);
            }
        }
    }
}