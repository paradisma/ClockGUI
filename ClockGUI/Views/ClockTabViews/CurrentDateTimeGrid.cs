using System;
using System.Windows.Forms;
using ClockGUI.Utils;
using ClockGui.Logger;

namespace ClockGUI.Views.ClockTabViews
{
    public partial class CurrentDateTimeGrid : GroupBox
    {
        public CurrentDateTimeGrid()
        {
            //Setup the GroupBox params.
            this.Text = "Today";
            this.Size = new System.Drawing.Size(560, 175);
            this.BackColor = System.Drawing.Color.Transparent;
            this.ForeColor = System.Drawing.SystemColors.HighlightText;

            //Setup Current Time.
            this.currentTime.AutoSize = true;
            this.currentTime.Font = new System.Drawing.Font("Microsoft Sans Serif", 70.25F);
            this.currentTime.ForeColor = System.Drawing.Color.White;
            this.currentTime.Location = new System.Drawing.Point(6, 31);
            this.currentTime.Name = "currentTime";
            this.currentTime.Size = new System.Drawing.Size(409, 107);
            this.currentTime.TabIndex = 0;
            this.currentTime.Text = "00:00:00";

            //Setup Current Date.
            this.currentDate.AutoSize = true;
            this.currentDate.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.currentDate.Location = new System.Drawing.Point(19, 134);
            this.currentDate.Name = "currentDate";
            this.currentDate.Size = new System.Drawing.Size(161, 29);
            this.currentDate.TabIndex = 1;
            this.currentDate.Text = "00/00/0000";

            //Connect date and time controls.
            this.Controls.Add(currentTime);
            this.Controls.Add(currentDate);

            InitializeComponent();
            InitializeClock();
        }

        private void InitializeClock()
        {
            //Setup the clock timer.
            refreshTimer.Interval = ConfigurationProvider.TimeUpdateInterval;
            refreshTimer.Tick += UpdateTime;
            refreshTimer.Start();
            UpdateTime(null, null);
        }

        private void UpdateTime(object sender, EventArgs e)
        {
            var time = DateTime.Now;
            this.currentTime.Text = time.ToLongTimeString();
            this.currentDate.Text = time.ToLongDateString();
        }

        private Label currentTime = new Label();
        private Label currentDate = new Label();
        private Timer refreshTimer = new Timer();
    }
}
