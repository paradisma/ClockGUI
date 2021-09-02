using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using PhilipsHueController;
using Weather;
using ClockGUI.Utils;
using ClockGui.Logger;
using Weather.WeatherObjects;
using System.Configuration;

namespace ClockGUI
{
    public partial class Form1 : Form
    {
        private HueManager hueManager;
        private Timer inactiveTimer = new Timer();
        private string hueBridgeIp = ConfigurationManager.AppSettings["EstablishedHueIp"];
        private string hueAppKey = ConfigurationManager.AppSettings["HueAppKey"];
        private int inactiveInterval = Convert.ToInt32(ConfigurationManager.AppSettings["InactiveReturnToClockTimeout"]);

        public Form1()
        {
            Logger.Debug($"Initializing ClockGUI...");

            hueManager = new HueManager(hueBridgeIp, hueAppKey);
            hueManager.InitializeManager();

            Image background = new Bitmap(Properties.Resources.background_scaled);

            InitializeComponent();
            this.clockTab.BackgroundImage = background;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Setup the inactivity timer.
            inactiveTimer.Interval = inactiveInterval;
            inactiveTimer.Tick += new EventHandler(returnToClockTab);
        }

        private void returnToClockTab(object sender, EventArgs e)
        {
            Logger.Debug($"[{nameof(Form1)}] [{nameof(returnToClockTab)}] Inactive timeout reached, returning to clock tab.");
            this.tabControl1.SelectedTab = this.clockTab;
            inactiveTimer.Stop();
        }

        private void restartInactiveTimer()
        {
            this.inactiveTimer.Stop();
            this.inactiveTimer.Start();
        }


        private void OnTabIndexChanged(object sender, EventArgs e)
        {
            var selectedTabName = (sender as TabControl).SelectedTab.AccessibilityObject.Name;

            Logger.Debug($"[{nameof(Form1)}] [{nameof(OnTabIndexChanged)}] Selected tab changed to [{selectedTabName}]");

            if(selectedTabName == "Hue Controls")
            {
                Logger.Debug($"[{nameof(Form1)}] [{nameof(OnTabIndexChanged)}] Hue control tab selected, starting inactive timer.");
                inactiveTimer.Start();
            }
        }


        private void OnButton_Click(object sender, EventArgs e)
        {
            this.restartInactiveTimer();

            hueManager.TurnOneOrMoreLightsOn();
        }

        private void OffButton_Click(object sender, EventArgs e)
        {
            this.restartInactiveTimer();

            hueManager.TurnOneOrMoreLightsOff();
        }

        private void BrightnessSlider_Scroll(object sender, EventArgs e)
        {
            this.restartInactiveTimer();

            var slider = sender as TrackBar;
            int brightness = slider.Value * 10;

            hueManager.DimLights(brightness);
        }

        private void hueControlTabClicked(object sender, EventArgs e)
        {
            this.restartInactiveTimer();
        }


        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
