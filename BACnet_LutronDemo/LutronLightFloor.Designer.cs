namespace BACnet_LutronDemo
{
    partial class LutronLightFloor
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
            this.panel3 = new System.Windows.Forms.Panel();
            this.rbLight2 = new System.Windows.Forms.RadioButton();
            this.btnSwitch = new System.Windows.Forms.Button();
            this.rbLight1 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ddlFloor = new System.Windows.Forms.ComboBox();
            this.lblSensPer = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.trbkLightSense = new System.Windows.Forms.TrackBar();
            this.lblLight1 = new System.Windows.Forms.Label();
            this.lblLight2 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lbl2Floor = new System.Windows.Forms.Label();
            this.lbl26 = new System.Windows.Forms.Label();
            this.lbl25 = new System.Windows.Forms.Label();
            this.lbl24 = new System.Windows.Forms.Label();
            this.lbl23 = new System.Windows.Forms.Label();
            this.lbl22 = new System.Windows.Forms.Label();
            this.lbl21 = new System.Windows.Forms.Label();
            this.pnlFirstFloor = new System.Windows.Forms.Panel();
            this.lbl1Floor = new System.Windows.Forms.Label();
            this.lbl6 = new System.Windows.Forms.Label();
            this.lbl5 = new System.Windows.Forms.Label();
            this.lbl4 = new System.Windows.Forms.Label();
            this.lbl3 = new System.Windows.Forms.Label();
            this.lbl2 = new System.Windows.Forms.Label();
            this.lbl1 = new System.Windows.Forms.Label();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbkLightSense)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlFirstFloor.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.rbLight2);
            this.panel3.Controls.Add(this.btnSwitch);
            this.panel3.Controls.Add(this.rbLight1);
            this.panel3.Location = new System.Drawing.Point(235, 167);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(176, 100);
            this.panel3.TabIndex = 24;
            // 
            // rbLight2
            // 
            this.rbLight2.AutoSize = true;
            this.rbLight2.Location = new System.Drawing.Point(90, 12);
            this.rbLight2.Name = "rbLight2";
            this.rbLight2.Size = new System.Drawing.Size(57, 17);
            this.rbLight2.TabIndex = 18;
            this.rbLight2.TabStop = true;
            this.rbLight2.Text = "Light 2";
            this.rbLight2.UseVisualStyleBackColor = true;
            this.rbLight2.CheckedChanged += new System.EventHandler(this.rbLight2_CheckedChanged);
            // 
            // btnSwitch
            // 
            this.btnSwitch.Location = new System.Drawing.Point(14, 50);
            this.btnSwitch.Name = "btnSwitch";
            this.btnSwitch.Size = new System.Drawing.Size(142, 45);
            this.btnSwitch.TabIndex = 11;
            this.btnSwitch.Text = "On/Off";
            this.btnSwitch.UseVisualStyleBackColor = true;
            this.btnSwitch.Click += new System.EventHandler(this.btnSwitch_Click);
            // 
            // rbLight1
            // 
            this.rbLight1.AutoSize = true;
            this.rbLight1.Location = new System.Drawing.Point(14, 11);
            this.rbLight1.Name = "rbLight1";
            this.rbLight1.Size = new System.Drawing.Size(57, 17);
            this.rbLight1.TabIndex = 17;
            this.rbLight1.TabStop = true;
            this.rbLight1.Text = "Light 1";
            this.rbLight1.UseVisualStyleBackColor = true;
            this.rbLight1.CheckedChanged += new System.EventHandler(this.rbLight1_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.ddlFloor);
            this.panel2.Controls.Add(this.lblSensPer);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.trbkLightSense);
            this.panel2.Location = new System.Drawing.Point(25, 167);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(180, 100);
            this.panel2.TabIndex = 23;
            // 
            // ddlFloor
            // 
            this.ddlFloor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlFloor.FormattingEnabled = true;
            this.ddlFloor.Location = new System.Drawing.Point(80, 8);
            this.ddlFloor.Name = "ddlFloor";
            this.ddlFloor.Size = new System.Drawing.Size(82, 21);
            this.ddlFloor.TabIndex = 19;
            this.ddlFloor.SelectedIndexChanged += new System.EventHandler(this.ddlFloor_SelectedIndexChanged);
            // 
            // lblSensPer
            // 
            this.lblSensPer.AutoSize = true;
            this.lblSensPer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSensPer.Location = new System.Drawing.Point(141, 50);
            this.lblSensPer.Name = "lblSensPer";
            this.lblSensPer.Size = new System.Drawing.Size(14, 13);
            this.lblSensPer.TabIndex = 18;
            this.lblSensPer.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 17;
            this.label1.Text = "Light Sense";
            // 
            // trbkLightSense
            // 
            this.trbkLightSense.Location = new System.Drawing.Point(14, 35);
            this.trbkLightSense.Maximum = 100;
            this.trbkLightSense.Name = "trbkLightSense";
            this.trbkLightSense.Size = new System.Drawing.Size(128, 45);
            this.trbkLightSense.TabIndex = 16;
            this.trbkLightSense.TickStyle = System.Windows.Forms.TickStyle.Both;
            this.trbkLightSense.Scroll += new System.EventHandler(this.trbkLightSense_Scroll);
            this.trbkLightSense.MouseUp += new System.Windows.Forms.MouseEventHandler(this.trbkLightSense_MouseUp);
            // 
            // lblLight1
            // 
            this.lblLight1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblLight1.Location = new System.Drawing.Point(297, 25);
            this.lblLight1.Name = "lblLight1";
            this.lblLight1.Size = new System.Drawing.Size(114, 45);
            this.lblLight1.TabIndex = 22;
            this.lblLight1.Text = "Light 1";
            this.lblLight1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLight2
            // 
            this.lblLight2.BackColor = System.Drawing.SystemColors.ControlDark;
            this.lblLight2.Location = new System.Drawing.Point(297, 85);
            this.lblLight2.Name = "lblLight2";
            this.lblLight2.Size = new System.Drawing.Size(114, 46);
            this.lblLight2.TabIndex = 21;
            this.lblLight2.Text = "Light 2";
            this.lblLight2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.lbl2Floor);
            this.panel1.Controls.Add(this.lbl26);
            this.panel1.Controls.Add(this.lbl25);
            this.panel1.Controls.Add(this.lbl24);
            this.panel1.Controls.Add(this.lbl23);
            this.panel1.Controls.Add(this.lbl22);
            this.panel1.Controls.Add(this.lbl21);
            this.panel1.Location = new System.Drawing.Point(158, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(114, 123);
            this.panel1.TabIndex = 20;
            // 
            // lbl2Floor
            // 
            this.lbl2Floor.AutoSize = true;
            this.lbl2Floor.Location = new System.Drawing.Point(10, 9);
            this.lbl2Floor.Name = "lbl2Floor";
            this.lbl2Floor.Size = new System.Drawing.Size(70, 13);
            this.lbl2Floor.TabIndex = 7;
            this.lbl2Floor.Text = "Second Floor";
            // 
            // lbl26
            // 
            this.lbl26.BackColor = System.Drawing.Color.Silver;
            this.lbl26.Location = new System.Drawing.Point(52, 83);
            this.lbl26.Name = "lbl26";
            this.lbl26.Size = new System.Drawing.Size(32, 20);
            this.lbl26.TabIndex = 6;
            this.lbl26.Text = "6";
            // 
            // lbl25
            // 
            this.lbl25.BackColor = System.Drawing.Color.Silver;
            this.lbl25.Location = new System.Drawing.Point(52, 60);
            this.lbl25.Name = "lbl25";
            this.lbl25.Size = new System.Drawing.Size(32, 20);
            this.lbl25.TabIndex = 5;
            this.lbl25.Text = "5";
            // 
            // lbl24
            // 
            this.lbl24.BackColor = System.Drawing.Color.Silver;
            this.lbl24.Location = new System.Drawing.Point(52, 36);
            this.lbl24.Name = "lbl24";
            this.lbl24.Size = new System.Drawing.Size(32, 20);
            this.lbl24.TabIndex = 4;
            this.lbl24.Text = "4";
            // 
            // lbl23
            // 
            this.lbl23.BackColor = System.Drawing.Color.Silver;
            this.lbl23.Location = new System.Drawing.Point(14, 83);
            this.lbl23.Name = "lbl23";
            this.lbl23.Size = new System.Drawing.Size(32, 20);
            this.lbl23.TabIndex = 3;
            this.lbl23.Text = "3";
            // 
            // lbl22
            // 
            this.lbl22.BackColor = System.Drawing.Color.Silver;
            this.lbl22.Location = new System.Drawing.Point(14, 60);
            this.lbl22.Name = "lbl22";
            this.lbl22.Size = new System.Drawing.Size(32, 20);
            this.lbl22.TabIndex = 2;
            this.lbl22.Text = "2";
            // 
            // lbl21
            // 
            this.lbl21.BackColor = System.Drawing.Color.Silver;
            this.lbl21.Location = new System.Drawing.Point(14, 36);
            this.lbl21.Name = "lbl21";
            this.lbl21.Size = new System.Drawing.Size(32, 20);
            this.lbl21.TabIndex = 0;
            this.lbl21.Text = "1";
            // 
            // pnlFirstFloor
            // 
            this.pnlFirstFloor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFirstFloor.Controls.Add(this.lbl1Floor);
            this.pnlFirstFloor.Controls.Add(this.lbl6);
            this.pnlFirstFloor.Controls.Add(this.lbl5);
            this.pnlFirstFloor.Controls.Add(this.lbl4);
            this.pnlFirstFloor.Controls.Add(this.lbl3);
            this.pnlFirstFloor.Controls.Add(this.lbl2);
            this.pnlFirstFloor.Controls.Add(this.lbl1);
            this.pnlFirstFloor.Location = new System.Drawing.Point(25, 25);
            this.pnlFirstFloor.Name = "pnlFirstFloor";
            this.pnlFirstFloor.Size = new System.Drawing.Size(114, 123);
            this.pnlFirstFloor.TabIndex = 19;
            // 
            // lbl1Floor
            // 
            this.lbl1Floor.AutoSize = true;
            this.lbl1Floor.Location = new System.Drawing.Point(10, 9);
            this.lbl1Floor.Name = "lbl1Floor";
            this.lbl1Floor.Size = new System.Drawing.Size(52, 13);
            this.lbl1Floor.TabIndex = 7;
            this.lbl1Floor.Text = "First Floor";
            // 
            // lbl6
            // 
            this.lbl6.BackColor = System.Drawing.Color.Silver;
            this.lbl6.Location = new System.Drawing.Point(52, 83);
            this.lbl6.Name = "lbl6";
            this.lbl6.Size = new System.Drawing.Size(32, 20);
            this.lbl6.TabIndex = 6;
            this.lbl6.Text = "6";
            // 
            // lbl5
            // 
            this.lbl5.BackColor = System.Drawing.Color.Silver;
            this.lbl5.Location = new System.Drawing.Point(52, 60);
            this.lbl5.Name = "lbl5";
            this.lbl5.Size = new System.Drawing.Size(32, 20);
            this.lbl5.TabIndex = 5;
            this.lbl5.Text = "5";
            // 
            // lbl4
            // 
            this.lbl4.BackColor = System.Drawing.Color.Silver;
            this.lbl4.Location = new System.Drawing.Point(52, 36);
            this.lbl4.Name = "lbl4";
            this.lbl4.Size = new System.Drawing.Size(32, 20);
            this.lbl4.TabIndex = 4;
            this.lbl4.Text = "4";
            // 
            // lbl3
            // 
            this.lbl3.BackColor = System.Drawing.Color.Silver;
            this.lbl3.Location = new System.Drawing.Point(14, 83);
            this.lbl3.Name = "lbl3";
            this.lbl3.Size = new System.Drawing.Size(32, 20);
            this.lbl3.TabIndex = 3;
            this.lbl3.Text = "3";
            // 
            // lbl2
            // 
            this.lbl2.BackColor = System.Drawing.Color.Silver;
            this.lbl2.Location = new System.Drawing.Point(14, 60);
            this.lbl2.Name = "lbl2";
            this.lbl2.Size = new System.Drawing.Size(32, 20);
            this.lbl2.TabIndex = 2;
            this.lbl2.Text = "2";
            // 
            // lbl1
            // 
            this.lbl1.BackColor = System.Drawing.Color.Silver;
            this.lbl1.Location = new System.Drawing.Point(14, 36);
            this.lbl1.Name = "lbl1";
            this.lbl1.Size = new System.Drawing.Size(32, 20);
            this.lbl1.TabIndex = 0;
            this.lbl1.Text = "1";
            // 
            // LutronLightFloor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 294);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.lblLight1);
            this.Controls.Add(this.lblLight2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pnlFirstFloor);
            this.Name = "LutronLightFloor";
            this.Text = "LutronLightFloor";
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbkLightSense)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnlFirstFloor.ResumeLayout(false);
            this.pnlFirstFloor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton rbLight2;
        private System.Windows.Forms.Button btnSwitch;
        private System.Windows.Forms.RadioButton rbLight1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox ddlFloor;
        private System.Windows.Forms.Label lblSensPer;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TrackBar trbkLightSense;
        private System.Windows.Forms.Label lblLight1;
        private System.Windows.Forms.Label lblLight2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lbl2Floor;
        private System.Windows.Forms.Label lbl26;
        private System.Windows.Forms.Label lbl25;
        private System.Windows.Forms.Label lbl24;
        private System.Windows.Forms.Label lbl23;
        private System.Windows.Forms.Label lbl22;
        private System.Windows.Forms.Label lbl21;
        private System.Windows.Forms.Panel pnlFirstFloor;
        private System.Windows.Forms.Label lbl1Floor;
        private System.Windows.Forms.Label lbl6;
        private System.Windows.Forms.Label lbl5;
        private System.Windows.Forms.Label lbl4;
        private System.Windows.Forms.Label lbl3;
        private System.Windows.Forms.Label lbl2;
        private System.Windows.Forms.Label lbl1;
    }
}