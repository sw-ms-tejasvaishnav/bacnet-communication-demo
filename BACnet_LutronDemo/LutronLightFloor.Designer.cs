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
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblAlarmEventState = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblAlarmInstance = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblAlarmDevice = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label9 = new System.Windows.Forms.Label();
            this.txtScheduleValue = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.ddlScheduleObject = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.ddlScheduleDevice = new System.Windows.Forms.ComboBox();
            this.btnSchedule = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.txtScheduleSeconds = new System.Windows.Forms.TextBox();
            this.txtScheduleMinutes = new System.Windows.Forms.TextBox();
            this.txtScheduleHours = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.ddlScheduleDay = new System.Windows.Forms.ComboBox();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trbkLightSense)).BeginInit();
            this.panel1.SuspendLayout();
            this.pnlFirstFloor.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
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
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.lblAlarmEventState);
            this.panel4.Controls.Add(this.label4);
            this.panel4.Controls.Add(this.lblAlarmInstance);
            this.panel4.Controls.Add(this.label3);
            this.panel4.Controls.Add(this.lblAlarmDevice);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Location = new System.Drawing.Point(25, 301);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(386, 123);
            this.panel4.TabIndex = 25;
            // 
            // lblAlarmEventState
            // 
            this.lblAlarmEventState.AutoSize = true;
            this.lblAlarmEventState.Location = new System.Drawing.Point(87, 92);
            this.lblAlarmEventState.Name = "lblAlarmEventState";
            this.lblAlarmEventState.Size = new System.Drawing.Size(10, 13);
            this.lblAlarmEventState.TabIndex = 5;
            this.lblAlarmEventState.Text = "-";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 92);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(64, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "Event state:";
            // 
            // lblAlarmInstance
            // 
            this.lblAlarmInstance.AutoSize = true;
            this.lblAlarmInstance.Location = new System.Drawing.Point(74, 51);
            this.lblAlarmInstance.Name = "lblAlarmInstance";
            this.lblAlarmInstance.Size = new System.Drawing.Size(10, 13);
            this.lblAlarmInstance.TabIndex = 3;
            this.lblAlarmInstance.Text = "-";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 51);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(51, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Instance:";
            // 
            // lblAlarmDevice
            // 
            this.lblAlarmDevice.AutoSize = true;
            this.lblAlarmDevice.Location = new System.Drawing.Point(67, 17);
            this.lblAlarmDevice.Name = "lblAlarmDevice";
            this.lblAlarmDevice.Size = new System.Drawing.Size(10, 13);
            this.lblAlarmDevice.TabIndex = 1;
            this.lblAlarmDevice.Text = "-";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(17, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(44, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Device:";
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label9);
            this.panel5.Controls.Add(this.txtScheduleValue);
            this.panel5.Controls.Add(this.label8);
            this.panel5.Controls.Add(this.ddlScheduleObject);
            this.panel5.Controls.Add(this.label7);
            this.panel5.Controls.Add(this.ddlScheduleDevice);
            this.panel5.Controls.Add(this.btnSchedule);
            this.panel5.Controls.Add(this.label6);
            this.panel5.Controls.Add(this.txtScheduleSeconds);
            this.panel5.Controls.Add(this.txtScheduleMinutes);
            this.panel5.Controls.Add(this.txtScheduleHours);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Controls.Add(this.ddlScheduleDay);
            this.panel5.Location = new System.Drawing.Point(25, 457);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(386, 231);
            this.panel5.TabIndex = 26;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(4, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 13);
            this.label9.TabIndex = 12;
            this.label9.Text = "Present value";
            // 
            // txtScheduleValue
            // 
            this.txtScheduleValue.Location = new System.Drawing.Point(77, 156);
            this.txtScheduleValue.Name = "txtScheduleValue";
            this.txtScheduleValue.Size = new System.Drawing.Size(48, 20);
            this.txtScheduleValue.TabIndex = 11;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(15, 125);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 10;
            this.label8.Text = "Object";
            // 
            // ddlScheduleObject
            // 
            this.ddlScheduleObject.FormattingEnabled = true;
            this.ddlScheduleObject.Items.AddRange(new object[] {
            "Monday,1",
            "Tuesday,2"});
            this.ddlScheduleObject.Location = new System.Drawing.Point(78, 122);
            this.ddlScheduleObject.Name = "ddlScheduleObject";
            this.ddlScheduleObject.Size = new System.Drawing.Size(287, 21);
            this.ddlScheduleObject.TabIndex = 9;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(14, 89);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(41, 13);
            this.label7.TabIndex = 8;
            this.label7.Text = "Device";
            // 
            // ddlScheduleDevice
            // 
            this.ddlScheduleDevice.FormattingEnabled = true;
            this.ddlScheduleDevice.Items.AddRange(new object[] {
            "Monday,1",
            "Tuesday,2"});
            this.ddlScheduleDevice.Location = new System.Drawing.Point(77, 86);
            this.ddlScheduleDevice.Name = "ddlScheduleDevice";
            this.ddlScheduleDevice.Size = new System.Drawing.Size(121, 21);
            this.ddlScheduleDevice.TabIndex = 7;
            this.ddlScheduleDevice.SelectedIndexChanged += new System.EventHandler(this.ddlScheduleDevice_SelectedIndexChanged);
            // 
            // btnSchedule
            // 
            this.btnSchedule.Location = new System.Drawing.Point(77, 190);
            this.btnSchedule.Name = "btnSchedule";
            this.btnSchedule.Size = new System.Drawing.Size(75, 23);
            this.btnSchedule.TabIndex = 6;
            this.btnSchedule.Text = "Schedule";
            this.btnSchedule.UseVisualStyleBackColor = true;
            this.btnSchedule.Click += new System.EventHandler(this.btnSchedule_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(14, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(30, 13);
            this.label6.TabIndex = 5;
            this.label6.Text = "Time";
            this.label6.Click += new System.EventHandler(this.label6_Click);
            // 
            // txtScheduleSeconds
            // 
            this.txtScheduleSeconds.Location = new System.Drawing.Point(199, 51);
            this.txtScheduleSeconds.Name = "txtScheduleSeconds";
            this.txtScheduleSeconds.Size = new System.Drawing.Size(48, 20);
            this.txtScheduleSeconds.TabIndex = 4;
            this.txtScheduleSeconds.Validating += new System.ComponentModel.CancelEventHandler(this.txtScheduleSeconds_Validating);
            // 
            // txtScheduleMinutes
            // 
            this.txtScheduleMinutes.Location = new System.Drawing.Point(137, 51);
            this.txtScheduleMinutes.Name = "txtScheduleMinutes";
            this.txtScheduleMinutes.Size = new System.Drawing.Size(48, 20);
            this.txtScheduleMinutes.TabIndex = 3;
            this.txtScheduleMinutes.Validating += new System.ComponentModel.CancelEventHandler(this.txtScheduleMinutes_Validating);
            // 
            // txtScheduleHours
            // 
            this.txtScheduleHours.Location = new System.Drawing.Point(77, 51);
            this.txtScheduleHours.Name = "txtScheduleHours";
            this.txtScheduleHours.Size = new System.Drawing.Size(48, 20);
            this.txtScheduleHours.TabIndex = 2;
            this.txtScheduleHours.Validating += new System.ComponentModel.CancelEventHandler(this.txtScheduleHours_Validating);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(14, 18);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(57, 13);
            this.label5.TabIndex = 1;
            this.label5.Text = "Select day";
            // 
            // ddlScheduleDay
            // 
            this.ddlScheduleDay.FormattingEnabled = true;
            this.ddlScheduleDay.Items.AddRange(new object[] {
            "Monday,1",
            "Tuesday,2"});
            this.ddlScheduleDay.Location = new System.Drawing.Point(77, 15);
            this.ddlScheduleDay.Name = "ddlScheduleDay";
            this.ddlScheduleDay.Size = new System.Drawing.Size(121, 21);
            this.ddlScheduleDay.TabIndex = 0;
            // 
            // LutronLightFloor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 727);
            this.Controls.Add(this.panel5);
            this.Controls.Add(this.panel4);
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
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
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
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblAlarmDevice;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblAlarmInstance;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblAlarmEventState;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox ddlScheduleDay;
        private System.Windows.Forms.TextBox txtScheduleHours;
        private System.Windows.Forms.TextBox txtScheduleMinutes;
        private System.Windows.Forms.TextBox txtScheduleSeconds;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnSchedule;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox ddlScheduleDevice;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.ComboBox ddlScheduleObject;
        private System.Windows.Forms.TextBox txtScheduleValue;
        private System.Windows.Forms.Label label9;
    }
}