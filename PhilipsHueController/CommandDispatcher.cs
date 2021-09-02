using Q42.HueApi;
using Q42.HueApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhilipsHueController
{
    public static class CommandDispatcher
    {
        private static readonly string AppKey = Properties.HueBridge.Default.EstablishedAppKey;
        private static string BridgeIP = Properties.HueBridge.Default.EstablishedBridgeIp;

        private static LightCommand OnCommand = new LightCommand().TurnOn();
        private static LightCommand OffCommand = new LightCommand().TurnOff();

        private static ILocalHueClient HueBridge;

        static CommandDispatcher()
        {
            Console.WriteLine("Initializing..");
            try
            {
                HueBridge = new LocalHueClient(BridgeIP);
                HueBridge.Initialize(AppKey);
            }
            catch
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Warning - Failed to initialize the HueClient using Bridge IP {BridgeIP}\n" +
                                  $"The IP has likely changed consider running: PhilipsHueController UpdateBridgeIP\n" +
                                  $"to update to a new bridge IP address.");
                Console.ResetColor();
            }
        }

        public static void UpdateBridgeIP(string newIP)
        {
            BridgeIP = newIP;
            HueBridge = new LocalHueClient(BridgeIP);

            Properties.HueBridge.Default.EstablishedBridgeIp = newIP;
            Properties.HueBridge.Default.Save();
        }

        public static void DisplayAvailableBridges()
        {
            var localBridges = HueEstablisher.GetLocalHueBridges().Result;

            Console.WriteLine("Available Bridge IPs:");
            for (int i = 0; i < localBridges.Count(); i++)
            {
                Console.WriteLine($"{localBridges.ElementAt(i).IpAddress}");
            }
        }

        public static void TurnOffLights()
        {
            HueBridge.SendCommandAsync(OffCommand).Wait();
        }

        public static void TurnOnLights()
        {
            Console.WriteLine("Turning on lights");
            HueBridge.SendCommandAsync(OnCommand).Wait();
        }

    }
}
