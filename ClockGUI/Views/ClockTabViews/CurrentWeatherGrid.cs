using ClockGUI.Utils;
using System;
using System.Windows.Forms;
using Weather;
using Weather.WeatherObjects;

namespace ClockGUI.Views.ClockTabViews
{
    public partial class CurrentWeatherGrid : GroupBox
    {
        public CurrentWeatherGrid()
        {
            //GroupBox params.
            this.Text = "Current Weather";
            this.Size = new System.Drawing.Size(250, 250);
            this.ForeColor = System.Drawing.SystemColors.HighlightText;
            this.BackColor = System.Drawing.Color.Transparent;

            //Temp Label.
            this.currentTemp.AutoSize = true;
            this.currentTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.currentTemp.Location = new System.Drawing.Point(7, 35);
            this.currentTemp.Name = "currentTemp";
            this.currentTemp.Text = "unknown";

            //Humidity Label.
            this.currentHumidity.AutoSize = true;
            this.currentTemp.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.currentHumidity.Location = new System.Drawing.Point(7, 65);
            this.currentHumidity.Name = "currentHumidity";
            this.currentHumidity.Text = "unknown";

            //Weather description.
            this.currentWeatherDescription.AutoSize = true;
            this.currentWeatherDescription.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F);
            this.currentWeatherDescription.Location = new System.Drawing.Point(7, 90);
            this.currentWeatherDescription.Name = "currentWeatherDescription";
            this.currentWeatherDescription.Text = "unknown";

            //Sunrise.
            this.sunriseTime.AutoSize = true;
            this.sunriseTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.sunriseTime.Location = new System.Drawing.Point(7, 150);
            this.sunriseTime.Name = "sunriseTime";
            this.sunriseTime.Text = "unknown";

            //Sunset.
            this.sunsetTime.AutoSize = true;
            this.sunsetTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F);
            this.sunsetTime.Location = new System.Drawing.Point(7, 170);
            this.sunsetTime.Name = "sunsetTime";
            this.sunsetTime.Text = "unknown";

            //Updated time.
            this.updatedTime.AutoSize = true;
            this.updatedTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F);
            this.updatedTime.Location = new System.Drawing.Point(7, 230);
            this.updatedTime.Name = "updatedTime";
            this.updatedTime.Text = "unknown";

            //Add controls to groupbox.
            this.Controls.Add(currentTemp);
            this.Controls.Add(currentHumidity);
            this.Controls.Add(currentWeatherDescription);
            this.Controls.Add(sunriseTime);
            this.Controls.Add(sunsetTime);
            this.Controls.Add(updatedTime);

            InitializeComponent();
            InitializeWeatherComponents();
        }

        private void InitializeWeatherComponents()
        {
            //Setup weather timer.
            refreshTimer.Interval = ConfigurationProvider.WeatherUpdateInterval;
            refreshTimer.Tick += UpdateWeatherData;
            refreshTimer.Start();
            UpdateWeatherData(null, null);
        }

        private async void UpdateWeatherData(object sender, EventArgs e)
        {
            var weatherResults = await weather.GetWeatherAsync();

            if (weatherResults != null)
            {
                currentTemp.Text = $"{(int)weatherResults.main.temp} °F";
                currentHumidity.Text = $"{weatherResults.main.humidity} % Humidity";
                currentWeatherDescription.Text = $"{weatherResults.weather[0].description}";
                sunriseTime.Text = $"Sunrise: {TimeConvertor.UTCToLocalDateTime(weatherResults.sys.sunrise).ToShortTimeString()}";
                sunsetTime.Text = $"Sunset:  {TimeConvertor.UTCToLocalDateTime(weatherResults.sys.sunset).ToShortTimeString()}";
                updatedTime.Text = $"Updated At: {DateTime.Now.ToShortTimeString()}";
            }
        }

        private Timer refreshTimer = new Timer();
        private WeatherTracker weather = new WeatherTracker();

        private Label currentTemp = new Label();
        private Label currentHumidity = new Label();
        private Label currentWeatherDescription = new Label();
        private Label sunriseTime = new Label();
        private Label sunsetTime = new Label();
        private Label updatedTime = new Label();

    }
}
