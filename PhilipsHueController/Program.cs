using Q42.HueApi;
using Q42.HueApi.Interfaces;
using Q42.HueApi.Models.Bridge;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Configuration;
using ClockGui.Logger;

namespace PhilipsHueController
{
    class Program
    {
        private static LightCommand OnCommand = new LightCommand().TurnOn();
        private static LightCommand OffCommand = new LightCommand().TurnOff();

        private static string CurrentBridgeIP = "192.168.1.187";
        private static readonly string AppKey = ConfigurationManager.AppSettings["EstablishedAppKey"];

        private static string[] inputArgs;

        public static void Main(string[] args)
        {
            inputArgs = args;
            ProcessCommandArgs(args[0]);
        }

        private static void ProcessCommandArgs(string commandType)
        {
            Console.WriteLine($"Command: {commandType}");

            switch (commandType)
            {
                case "lightsOff":
                    CommandDispatcher.TurnOffLights();
                    break;

                case "lightsOn":
                    CommandDispatcher.TurnOnLights();
                    break;

                case "displayBridges":
                    CommandDispatcher.DisplayAvailableBridges();
                    break;

                case "updateBridgeIP":
                    CommandDispatcher.UpdateBridgeIP(inputArgs[1]);
                    break;

                case "displayPromptMenu":
                    DisplayStartupPrompt();
                    break;

                case "help":
                    DisplayHelpMenu();
                    break;
            }
        }


        private static void DisplayHelpMenu()
        {
            Console.WriteLine(
                $"Commands:\n" +
                $"lightsOff - Turns all lights off connected to the bridge.\n" +
                $"lightsOn  - Turns on all lights connected to the bridge.\n" +
                $"updateBridgeAddress [newBridgeIP] - Updates the default bridge IP to the one entered.\n" +
                $"displayPromptMenu - Displays a menu prompt of options you can use.\n" +
                $"displayBridges - Displays all available Hue bridges on the network.");
        }

        private static void DisplayStartupPrompt()
        {
            
            string usersChoice = string.Empty;
            do
            {
                Console.Clear();
                Console.Write("Welcome. What would you like to do?\n" +
                              "1) Use established bridge IP.\n" +
                              "2) Search for available bridge IPs.\n" +
                              "> ");
                usersChoice = Console.ReadLine();

                switch (usersChoice)
                {
                    case "1":
                        BridgeControllPrompt();
                        break;

                    case "2":
                        SearchForIPsPrompt();
                        break;
                }

            } while (!usersChoice.Equals("q"));
        }

        private static void SearchForIPsPrompt()
        {
            Console.Clear();
            Console.WriteLine("Searching for IPs on the network..");

            var localBridges = HueEstablisher.GetLocalHueBridges().Result;

            if (localBridges.Count() > 0)
            {
                //Display all posible bridges to connect to.
                Console.WriteLine("Select which bridge to use:");
                for (int i = 0; i < localBridges.Count(); i++)
                {
                    Console.WriteLine($"{i}) {localBridges.ElementAt(i).IpAddress}");
                }

                //Get which bridge to use from the user. Loop until they give a valid option.
                int userChoice = -1;
                do
                {
                    Console.Write("\r> ");
                    userChoice = Convert.ToInt32(Console.ReadLine());
                } while (userChoice >= localBridges.Count() || userChoice < 0);

                //Set the global BridgeIP to use, to the IP the user chose.
                CurrentBridgeIP = localBridges.ElementAt(userChoice).IpAddress;
                BridgeControllPrompt();
            }
            else
            {
                Console.WriteLine("No bridges found.");
            }
        }

        private static void BridgeControllPrompt()
        {
            //Don't go further if IP was empty
            if (CurrentBridgeIP.Equals(string.Empty))
            {
                Console.WriteLine("ERROR Bridge IP was empty.");
                Thread.Sleep(500);
                return;
            }
            //IP was not empty create new Bridge instance.
            else
            {
                ILocalHueClient HueBridge = new LocalHueClient(CurrentBridgeIP);
                HueBridge.Initialize(AppKey);

                string usersChoice = string.Empty;
                do
                {
                    Console.Clear();
                    Console.Write($"Bridge @{CurrentBridgeIP}\n" +
                        $"1) Turn off lights.\n" +
                        $"2) Turn on lights.\n" +
                        $"3) Dim lights.\n" +
                        $"> ");
                    usersChoice = Console.ReadLine();

                    switch (usersChoice)
                    {
                        case "1":
                            HueBridge.SendCommandAsync(OffCommand).Wait();
                            break;

                        case "2":
                            HueBridge.SendCommandAsync(OnCommand).Wait();
                            break;
                        case "3":
                            Console.Write("Enter brightness level. (0 - 100)\n" +
                                "> ");
                            DimLights(Console.ReadLine(), HueBridge);
                            break;
                    }
                } while (!usersChoice.Equals("q"));
            }
        }

        private static void DimLights(string brightness, ILocalHueClient HueBridge)
        {
            bool wasValid = Int32.TryParse(brightness, out int brightnessAsInt);

            if (wasValid)
            {
                DimLights(brightnessAsInt, HueBridge);
            }
        }

        private static void DimLights(int brightness, ILocalHueClient HueBridge)
        {
            brightness = brightness > 100 ? 100 : brightness;
            brightness = brightness < 0 ? 0 : brightness;
            brightness = Convert.ToInt32(brightness * 2.55);

            LightCommand dimLight = new LightCommand();
            dimLight.Brightness = Convert.ToByte(brightness);

            HueBridge.SendCommandAsync(dimLight).Wait();
        }
    }
}
