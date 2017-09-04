namespace BACnet_LutronDemo
{
    partial class LutronLight
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
            this.chkLightOnOff = new System.Windows.Forms.CheckBox();
            this.trackBarLightingLevel = new System.Windows.Forms.TrackBar();
            this.lblCurrentLightLevelLable = new System.Windows.Forms.Label();
            this.lblCurrentLightLevelValue = new System.Windows.Forms.Label();
            this.btnReadLutronLightLevel = new System.Windows.Forms.Button();
            this.ddlDeviceList = new System.Windows.Forms.ComboBox();
            this.lblDeviceLabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLightingLevel)).BeginInit();
            this.SuspendLayout();
            // 
            // chkLightOnOff
            // 
            this.chkLightOnOff.Appearance = System.Windows.Forms.Appearance.Button;
            this.chkLightOnOff.AutoSize = true;
            this.chkLightOnOff.Location = new System.Drawing.Point(220, 12);
            this.chkLightOnOff.Name = "chkLightOnOff";
            this.chkLightOnOff.Size = new System.Drawing.Size(72, 23);
            this.chkLightOnOff.TabIndex = 0;
            this.chkLightOnOff.Text = "Light on/off";
            this.chkLightOnOff.UseVisualStyleBackColor = true;
            this.chkLightOnOff.CheckedChanged += new System.EventHandler(this.chkLightOnOff_CheckedChanged);
            // 
            // trackBarLightingLevel
            // 
            this.trackBarLightingLevel.Location = new System.Drawing.Point(80, 87);
            this.trackBarLightingLevel.Maximum = 100;
            this.trackBarLightingLevel.Name = "trackBarLightingLevel";
            this.trackBarLightingLevel.Size = new System.Drawing.Size(104, 45);
            this.trackBarLightingLevel.TabIndex = 1;
            this.trackBarLightingLevel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trackBarLightingLevel_MouseUp);
            // 
            // lblCurrentLightLevelLable
            // 
            this.lblCurrentLightLevelLable.AutoSize = true;
            this.lblCurrentLightLevelLable.Location = new System.Drawing.Point(44, 151);
            this.lblCurrentLightLevelLable.Name = "lblCurrentLightLevelLable";
            this.lblCurrentLightLevelLable.Size = new System.Drawing.Size(94, 13);
            this.lblCurrentLightLevelLable.TabIndex = 2;
            this.lblCurrentLightLevelLable.Text = "Current light level: ";
            // 
            // lblCurrentLightLevelValue
            // 
            this.lblCurrentLightLevelValue.AutoSize = true;
            this.lblCurrentLightLevelValue.Location = new System.Drawing.Point(136, 151);
            this.lblCurrentLightLevelValue.Name = "lblCurrentLightLevelValue";
            this.lblCurrentLightLevelValue.Size = new System.Drawing.Size(10, 13);
            this.lblCurrentLightLevelValue.TabIndex = 3;
            this.lblCurrentLightLevelValue.Text = "-";
            // 
            // btnReadLutronLightLevel
            // 
            this.btnReadLutronLightLevel.Location = new System.Drawing.Point(177, 145);
            this.btnReadLutronLightLevel.Name = "btnReadLutronLightLevel";
            this.btnReadLutronLightLevel.Size = new System.Drawing.Size(105, 23);
            this.btnReadLutronLightLevel.TabIndex = 4;
            this.btnReadLutronLightLevel.Text = "Read light level";
            this.btnReadLutronLightLevel.UseVisualStyleBackColor = true;
            this.btnReadLutronLightLevel.Click += new System.EventHandler(this.btnReadLutronLightLevel_Click);
            // 
            // ddlDeviceList
            // 
            this.ddlDeviceList.FormattingEnabled = true;
            this.ddlDeviceList.Location = new System.Drawing.Point(80, 12);
            this.ddlDeviceList.Name = "ddlDeviceList";
            this.ddlDeviceList.Size = new System.Drawing.Size(121, 21);
            this.ddlDeviceList.TabIndex = 5;
            // 
            // lblDeviceLabel
            // 
            this.lblDeviceLabel.AutoSize = true;
            this.lblDeviceLabel.Location = new System.Drawing.Point(0, 17);
            this.lblDeviceLabel.Name = "lblDeviceLabel";
            this.lblDeviceLabel.Size = new System.Drawing.Size(74, 13);
            this.lblDeviceLabel.TabIndex = 6;
            this.lblDeviceLabel.Text = "Select Device";
            // 
            // LutronLight
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(345, 261);
            this.Controls.Add(this.lblDeviceLabel);
            this.Controls.Add(this.ddlDeviceList);
            this.Controls.Add(this.btnReadLutronLightLevel);
            this.Controls.Add(this.lblCurrentLightLevelValue);
            this.Controls.Add(this.lblCurrentLightLevelLable);
            this.Controls.Add(this.trackBarLightingLevel);
            this.Controls.Add(this.chkLightOnOff);
            this.Name = "LutronLight";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBarLightingLevel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox chkLightOnOff;
        private System.Windows.Forms.TrackBar trackBarLightingLevel;
        private System.Windows.Forms.Label lblCurrentLightLevelLable;
        private System.Windows.Forms.Label lblCurrentLightLevelValue;
        private System.Windows.Forms.Button btnReadLutronLightLevel;
        private System.Windows.Forms.ComboBox ddlDeviceList;
        private System.Windows.Forms.Label lblDeviceLabel;
    }
}

