using ClockGui.Logger;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PhilipsHueController
{
    public class HueManager
    {
        public ILocalHueClient hueBridge { get; private set; }
        public string bridgeIP { get; private set; } = string.Empty;
        public string appKey { get; private set; } = string.Empty;

        public LightStateService lightStateService;

        /// <summary>
        /// Default constructor takes in the ip of the bridge and the appkey to use.
        /// </summary>
        public HueManager(string _bridgeIP, string _appKey)
        {
            bridgeIP = _bridgeIP;
            appKey = _appKey;
        }

        /// <summary>
        /// Initializes all the components of the hue bridge so that it can be used for communication and control.
        /// </summary>
        public void InitializeManager()
        {
            Logger.Debug($"[{nameof(HueManager)}] [{nameof(InitializeManager)}] Attemptng to initializing manager. BridgeIP={bridgeIP}, Appkey={appKey}.");
            try
            {
                this.hueBridge = new LocalHueClient(bridgeIP);
                this.hueBridge.Initialize(appKey);

                this.lightStateService = new LightStateService(this);
                this.lightStateService.InitializeService();
                
            }
            catch (Exception e)
            {
                Logger.Error($"[{nameof(HueManager)}] [{nameof(InitializeManager)}] An error occurred when trying to initialize the manager: {e.Message}");
                return;
            }

            Logger.Debug($"[{nameof(HueManager)}] [{nameof(InitializeManager)}] Manager successfully initialized.");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lightList"></param>
        public void TurnOneOrMoreLightsOn(List<string> lightList = null)
        {
            if (lightList == null)
            {
                Logger.Debug($"[{nameof(HueManager)}] [{nameof(TurnOneOrMoreLightsOn)}] Turning on all available lights.");
                this.hueBridge.SendCommandAsync(new LightCommand().TurnOn()).Wait();
            }
            else
            {
                Logger.Debug($"[{nameof(HueManager)}] [{nameof(TurnOneOrMoreLightsOn)}] Turning on {lightList.Count} available lights.");
                this.hueBridge.SendCommandAsync(new LightCommand().TurnOn(), lightList).Wait();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lightList"></param>
        public void TurnOneOrMoreLightsOff(List<string> lightList = null)
        {
            if (lightList == null)
            {
                Logger.Debug($"[{nameof(HueManager)}] [{nameof(TurnOneOrMoreLightsOff)}] Turning off all available lights.");
                this.hueBridge.SendCommandAsync(new LightCommand().TurnOff()).Wait();
            }
            else
            {
                Logger.Debug($"[{nameof(HueManager)}] [{nameof(TurnOneOrMoreLightsOff)}] Turning off {lightList.Count} available lights.");
                this.hueBridge.SendCommandAsync(new LightCommand().TurnOff(), lightList).Wait();
            }
        }

        public void DimLights(int brightness)
        {
            brightness = brightness > 100 ? 100 : brightness;
            brightness = brightness < 0 ? 0 : brightness;
            Logger.Debug($"[{nameof(HueManager)}] [{nameof(DimLights)}] Dimming lights to {brightness} %.");

            brightness = Convert.ToInt32(brightness * 2.55);
            LightCommand dimLight = new LightCommand();
            dimLight.Brightness = Convert.ToByte(brightness);

            hueBridge.SendCommandAsync(dimLight).Wait();
        }
    }
}
