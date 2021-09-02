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
    public static class LightController
    {
        public static async Task<IEnumerable<Light>> GetAvailableLights(ILocalHueClient HueBridge)
        {
            return await HueBridge.GetLightsAsync().ConfigureAwait(false);
        }

        public static async Task<bool> GetLightOnStatus(ILocalHueClient HueBridge, string lightId)
        {
            var light = await HueBridge.GetLightAsync(lightId).ConfigureAwait(false);
            return light.State.On;
        }
    }
}
