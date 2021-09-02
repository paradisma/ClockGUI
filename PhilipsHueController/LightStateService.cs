using ClockGui.Logger;
using ClockGui.Logger.Utils;
using Q42.HueApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Configuration;

namespace PhilipsHueController
{
    public class LightStateService
    {
        private HueManager hueManager;
        private int lightUpdateInterval = Convert.ToInt32(ConfigurationManager.AppSettings["LightStateUpdateInterval"]);

        //Dictionary to keep track of all the lights On states that are under control of this HueBridge.
        public Dictionary<string, bool> lightOnStates { get; private set; } = new Dictionary<string, bool>();
        public Dictionary<string, byte> lightBrightnessStates { get; private set; } = new Dictionary<string, byte>();
        //Dictionary to keep track opf all the light objects that are under control of this HueBridge.
        public Dictionary<string, Light> controllableLights { get; private set; } = new Dictionary<string, Light>();

        //Cancellation token for ending the polling status loop.
        private static CancellationTokenSource pollTokenSource = new CancellationTokenSource();
        private static CancellationToken pollToken = pollTokenSource.Token;

        public LightStateService(HueManager _hueManager)
        {
            this.hueManager = _hueManager;
        }

        public async void InitializeService()
        {
            await this.UpdateControllableLightsAsync();
            this.StartPollLightStatesAsync();
        }

        public void StartPollLightStatesAsync()
        {
            Logger.Debug($"[{nameof(LightStateService)}] [{nameof(StartPollLightStatesAsync)}] Beginning to poll light statuses, interval={lightUpdateInterval} ms.");

            IEnumerable<Light> refreshedLights;
            bool isFirstLoop = true;

            Task.Run(() =>
            {
                while (true)
                {
                    refreshedLights = LightController.GetAvailableLights(hueManager.hueBridge).Result.ToList();
                    //Update each light in the dictionary with its current state.
                    foreach (Light light in refreshedLights)
                    {
                        //Only update the dictionary if the value has changed.
                        if (light.State.On != lightOnStates[light.Id] || isFirstLoop == true)
                        {
                            //Log the light state change.
                            Logger.Debug($"[{nameof(LightStateService)}] [{nameof(StartPollLightStatesAsync)}] Light {light.Name} on state changed: {(light.State.On ? "Off -> On" : "On -> Off")}");
                            
                            this.lightOnStates[light.Id] = light.State.On;
                        }

                        if(light.State.Brightness != lightBrightnessStates[light.Id] || isFirstLoop == true)
                        {
                            //Log the light state change.
                            Logger.Debug($"[{nameof(LightStateService)}] [{nameof(StartPollLightStatesAsync)}] Light {light.Name} brightness state changed: {lightBrightnessStates[light.Id]} -> {light.State.Brightness}");

                            this.lightBrightnessStates[light.Id] = light.State.Brightness;
                        }
                        
                        this.controllableLights[light.Id] = light;
                    }
                    isFirstLoop = false;
                    Task.Delay(lightUpdateInterval).Wait();
                }
            }, pollToken);
        }

        public void StopPollLightStatuses()
        {
            Logger.Debug($"[{nameof(HueManager)}] [{nameof(StopPollLightStatuses)}] Stopping poll light statuses");

            pollTokenSource.Cancel();
        }


        private async Task UpdateControllableLightsAsync()
        {
            Logger.Debug($"[{nameof(LightStateService)}] [{nameof(UpdateControllableLightsAsync)}] Updating controllable lights.");

            //Get all the controllable lights for the HueBridge. Add their light.On states to the
            //lightOnStates dictionary and add the light object to the controllableLights dictionary.
            var lightList = await LightController.GetAvailableLights(hueManager.hueBridge);
            foreach(var light in lightList)
            {
                this.controllableLights[light.Id] = light;
                this.lightOnStates[light.Id] = light.State.On;
                this.lightBrightnessStates[light.Id] = light.State.Brightness;
            }

            Logger.Debug($"[{nameof(LightStateService)}] [{nameof(UpdateControllableLightsAsync)}] Controllable lights found {LogStringFormatter.CollectionToString(lightList)}");
        }

        public bool IsLightOn(string lightId)
        {
            try
            {
                return lightOnStates[lightId];
            }
            catch (KeyNotFoundException e)
            {
                throw (e);
            }
        }

    }
}
