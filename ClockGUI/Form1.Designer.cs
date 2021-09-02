using System;
using System.Windows.Forms;
using ClockGUI.Views.ClockTabViews;

namespace ClockGUI
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.currentDateTime = new CurrentDateTimeGrid();
            this.currentDateTime.Location = new System.Drawing.Point(8, 6);

            this.currentWeather = new CurrentWeatherGrid();
            this.currentWeather.Location = new System.Drawing.Point(8, 185);

            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.clockTab = new System.Windows.Forms.TabPage();
            this.hueTab = new System.Windows.Forms.TabPage();
            this.exitButton = new System.Windows.Forms.Button();
            this.brightnessLabel = new System.Windows.Forms.Label();
            this.brightnessSlider = new System.Windows.Forms.TrackBar();
            this.offButton = new System.Windows.Forms.Button();
            this.onButton = new System.Windows.Forms.Button();
            this.tabControl1.SuspendLayout();
            this.clockTab.SuspendLayout();
            this.hueTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessSlider)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.clockTab);
            this.tabControl1.Controls.Add(this.hueTab);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.tabControl1.Location = new System.Drawing.Point(0, -1);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(720, 480);
            this.tabControl1.TabIndex = 0;
            // 
            // clockTab
            // 
            this.clockTab.Controls.Add(this.currentWeather);
            this.clockTab.Controls.Add(currentDateTime);
            this.clockTab.Location = new System.Drawing.Point(4, 38);
            this.clockTab.Name = "clockTab";
            this.clockTab.Padding = new System.Windows.Forms.Padding(3);
            this.clockTab.Size = new System.Drawing.Size(712, 438);
            this.clockTab.TabIndex = 0;
            this.clockTab.Text = "Clock";
            // 
            // hueTab
            // 
            this.hueTab.BackColor = System.Drawing.Color.Black;
            this.hueTab.Controls.Add(this.exitButton);
            this.hueTab.Controls.Add(this.brightnessLabel);
            this.hueTab.Controls.Add(this.brightnessSlider);
            this.hueTab.Controls.Add(this.offButton);
            this.hueTab.Controls.Add(this.onButton);
            this.hueTab.Location = new System.Drawing.Point(4, 38);
            this.hueTab.Name = "hueTab";
            this.hueTab.Padding = new System.Windows.Forms.Padding(3);
            this.hueTab.Size = new System.Drawing.Size(712, 438);
            this.hueTab.TabIndex = 1;
            this.hueTab.Text = "Hue Controls";
            // 
            // exitButton
            // 
            this.exitButton.Location = new System.Drawing.Point(332, 307);
            this.exitButton.Name = "exitButton";
            this.exitButton.Size = new System.Drawing.Size(104, 38);
            this.exitButton.TabIndex = 4;
            this.exitButton.Text = "Exit";
            this.exitButton.UseVisualStyleBackColor = true;
            this.exitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // brightnessLabel
            // 
            this.brightnessLabel.AutoSize = true;
            this.brightnessLabel.Location = new System.Drawing.Point(24, 274);
            this.brightnessLabel.Name = "brightnessLabel";
            this.brightnessLabel.Size = new System.Drawing.Size(201, 29);
            this.brightnessLabel.TabIndex = 3;
            this.brightnessLabel.Text = "Brightness Level";
            // 
            // brightnessSlider
            // 
            this.brightnessSlider.BackColor = System.Drawing.Color.Gray;
            this.brightnessSlider.Location = new System.Drawing.Point(27, 300);
            this.brightnessSlider.Name = "brightnessSlider";
            this.brightnessSlider.Size = new System.Drawing.Size(229, 45);
            this.brightnessSlider.TabIndex = 2;
            this.brightnessSlider.Scroll += new System.EventHandler(this.BrightnessSlider_Scroll);
            // 
            // offButton
            // 
            this.offButton.BackColor = System.Drawing.Color.Gray;
            this.offButton.Location = new System.Drawing.Point(27, 153);
            this.offButton.Name = "offButton";
            this.offButton.Size = new System.Drawing.Size(229, 91);
            this.offButton.TabIndex = 1;
            this.offButton.Text = "Turn Lights Off";
            this.offButton.UseVisualStyleBackColor = false;
            this.offButton.Click += new System.EventHandler(this.OffButton_Click);
            // 
            // onButton
            // 
            this.onButton.BackColor = System.Drawing.Color.Gray;
            this.onButton.Location = new System.Drawing.Point(27, 16);
            this.onButton.Name = "onButton";
            this.onButton.Size = new System.Drawing.Size(229, 91);
            this.onButton.TabIndex = 0;
            this.onButton.Text = "Turn Lights On";
            this.onButton.UseVisualStyleBackColor = false;
            this.onButton.Click += new System.EventHandler(this.OnButton_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(720, 480);
            this.Controls.Add(this.tabControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.Text = "Form1";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.tabControl1.ResumeLayout(false);
            this.clockTab.ResumeLayout(false);
            this.hueTab.ResumeLayout(false);
            this.hueTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.brightnessSlider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private TabControl tabControl1;
        private TabPage clockTab;
        private TabPage hueTab;
        private Button offButton;
        private Button onButton;
        private Label brightnessLabel;
        private TrackBar brightnessSlider;
        
        private Button exitButton;
        private CurrentDateTimeGrid currentDateTime;
        private CurrentWeatherGrid currentWeather;
    }
}

