using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Q42.HueApi;
using Q42.HueApi.Interfaces;
using Q42.HueApi.Models.Bridge;
using ClockGui.Logger;

namespace PhilipsHueController
{
    public static class HueEstablisher
    {
        /// <summary>
        /// Querries the network and retreives all the IPs of available Hue Sync Bridges.
        /// </summary>
        public static async Task<IEnumerable<LocatedBridge>> GetLocalHueBridges()
        {
            
            //Logger.Info($"[{nameof(HueEstablisher)}] [{nameof(GetLocalHueBridges)}] Attempting to locate bridges on the local network.");
            IBridgeLocator locator = new HttpBridgeLocator();
            var locatedBridges = await locator.LocateBridgesAsync(TimeSpan.FromSeconds(5));

            string bridgesString = "[";
            var lastBridge = locatedBridges.Last();
            foreach(LocatedBridge LB in locatedBridges)
            {
                bridgesString += $"[BridgeId={LB.BridgeId}, BridgeIP={LB.IpAddress}]";
                bridgesString += LB.Equals(lastBridge) ? "]" : ", ";
            }

            //Logger.Debug($"[{nameof(HueEstablisher)}] [{nameof(GetLocalHueBridges)}] Available bridges found = {bridgesString}");

            return locatedBridges;
        }

        /// <summary>
        /// Generates an established App Key for the application and the specified HueBridge.
        /// </summary>
        /// <param name="HueBridge">The bridge to querry.</param>
        /// <param name="AppName">Name of the application.</param>
        /// <param name="DeviceName">Name of the device connecting.</param>
        /// <returns>Generated appkey from querrying the bridge. 
        /// Returns an empty string if it was uable to retreive an AppKey</returns>
        public static async Task<string> GenerateAppKey(ILocalHueClient HueBridge, string AppName = "DefaultAppName", string DeviceName = "DefaultDeviceName")
        {
            try
            {
                Logger.Debug($"[{nameof(HueEstablisher)}] [{nameof(GenerateAppKey)}] Attempting to get app key. AppName=[{AppName}], DeviceName=[{DeviceName}]");
                string AppKey = await HueBridge.RegisterAsync(AppName, DeviceName);
                Logger.Debug($"[{nameof(HueEstablisher)}] [{nameof(GenerateAppKey)}] Key successfully generated. AppKey=[{AppKey}]");

                return AppKey;
            }
            catch (Exception e)
            {
                Logger.Error($"[{nameof(HueEstablisher)}] [{nameof(GenerateAppKey)}] Failed to get app key. Exception was thrown, was hue sync button pressed?:\n{e.Message}");
                return string.Empty;
            }
        }
    }
}
