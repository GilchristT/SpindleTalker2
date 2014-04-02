namespace SpindleTalker2
{
    partial class MainWindow
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            this.statusStripBase = new System.Windows.Forms.StatusStrip();
            this.groupBoxSpindleControl = new System.Windows.Forms.GroupBox();
            this.checkBoxReverse = new System.Windows.Forms.CheckBox();
            this.groupBoxCOMPort = new System.Windows.Forms.GroupBox();
            this.groupBoxQuickSets = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanelQuickSets = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanelSpindle = new System.Windows.Forms.TableLayoutPanel();
            this.groupBoxSpindleSpeed = new System.Windows.Forms.GroupBox();
            this.gTrackBarSpindleSpeed = new gTrackBar.gTrackBar();
            this.toolTipBase = new System.Windows.Forms.ToolTip(this.components);
            this.panelMDI = new System.Windows.Forms.Panel();
            this.tableLayoutPanelBase = new System.Windows.Forms.TableLayoutPanel();
            this.panelMain = new System.Windows.Forms.Panel();
            this.timerInitialPoll = new System.Windows.Forms.Timer(this.components);
            this.toolStripStatusLabelVFDStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.buttonTerminal = new System.Windows.Forms.Button();
            this.buttonGraphs = new System.Windows.Forms.Button();
            this.buttonConnect = new System.Windows.Forms.Button();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.toolStripStatusLabelComPort = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusRPM = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStripBase.SuspendLayout();
            this.groupBoxSpindleControl.SuspendLayout();
            this.groupBoxCOMPort.SuspendLayout();
            this.groupBoxQuickSets.SuspendLayout();
            this.tableLayoutPanelSpindle.SuspendLayout();
            this.groupBoxSpindleSpeed.SuspendLayout();
            this.panelMDI.SuspendLayout();
            this.tableLayoutPanelBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStripBase
            // 
            this.statusStripBase.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabelComPort,
            this.toolStripStatusLabelVFDStatus,
            this.toolStripStatusRPM});
            this.statusStripBase.Location = new System.Drawing.Point(0, 340);
            this.statusStripBase.Name = "statusStripBase";
            this.statusStripBase.Size = new System.Drawing.Size(734, 22);
            this.statusStripBase.TabIndex = 2;
            this.statusStripBase.Text = "statusStrip1";
            // 
            // groupBoxSpindleControl
            // 
            this.groupBoxSpindleControl.CausesValidation = false;
            this.groupBoxSpindleControl.Controls.Add(this.checkBoxReverse);
            this.groupBoxSpindleControl.Controls.Add(this.buttonStart);
            this.groupBoxSpindleControl.Controls.Add(this.buttonStop);
            this.groupBoxSpindleControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSpindleControl.Enabled = false;
            this.groupBoxSpindleControl.Location = new System.Drawing.Point(82, 3);
            this.groupBoxSpindleControl.Name = "groupBoxSpindleControl";
            this.groupBoxSpindleControl.Size = new System.Drawing.Size(101, 79);
            this.groupBoxSpindleControl.TabIndex = 5;
            this.groupBoxSpindleControl.TabStop = false;
            this.groupBoxSpindleControl.Text = "Spindle Control";
            // 
            // checkBoxReverse
            // 
            this.checkBoxReverse.AutoSize = true;
            this.checkBoxReverse.Location = new System.Drawing.Point(19, 59);
            this.checkBoxReverse.Name = "checkBoxReverse";
            this.checkBoxReverse.Size = new System.Drawing.Size(66, 17);
            this.checkBoxReverse.TabIndex = 2;
            this.checkBoxReverse.Text = "Reverse";
            this.checkBoxReverse.UseVisualStyleBackColor = true;
            this.checkBoxReverse.CheckedChanged += new System.EventHandler(this.checkBoxReverse_CheckedChanged);
            // 
            // groupBoxCOMPort
            // 
            this.groupBoxCOMPort.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.groupBoxCOMPort.CausesValidation = false;
            this.groupBoxCOMPort.Controls.Add(this.buttonConnect);
            this.groupBoxCOMPort.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxCOMPort.Location = new System.Drawing.Point(3, 3);
            this.groupBoxCOMPort.Name = "groupBoxCOMPort";
            this.groupBoxCOMPort.Size = new System.Drawing.Size(73, 79);
            this.groupBoxCOMPort.TabIndex = 6;
            this.groupBoxCOMPort.TabStop = false;
            this.groupBoxCOMPort.Text = "COM Port";
            // 
            // groupBoxQuickSets
            // 
            this.groupBoxQuickSets.CausesValidation = false;
            this.groupBoxQuickSets.Controls.Add(this.flowLayoutPanelQuickSets);
            this.groupBoxQuickSets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxQuickSets.Enabled = false;
            this.groupBoxQuickSets.Location = new System.Drawing.Point(372, 3);
            this.groupBoxQuickSets.Name = "groupBoxQuickSets";
            this.groupBoxQuickSets.Size = new System.Drawing.Size(353, 79);
            this.groupBoxQuickSets.TabIndex = 7;
            this.groupBoxQuickSets.TabStop = false;
            this.groupBoxQuickSets.Text = "Speed Quicksets (RPM)";
            // 
            // flowLayoutPanelQuickSets
            // 
            this.flowLayoutPanelQuickSets.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanelQuickSets.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanelQuickSets.Name = "flowLayoutPanelQuickSets";
            this.flowLayoutPanelQuickSets.Size = new System.Drawing.Size(347, 60);
            this.flowLayoutPanelQuickSets.TabIndex = 5;
            // 
            // tableLayoutPanelSpindle
            // 
            this.tableLayoutPanelSpindle.ColumnCount = 4;
            this.tableLayoutPanelSpindle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelSpindle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelSpindle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanelSpindle.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSpindle.Controls.Add(this.groupBoxQuickSets, 3, 0);
            this.tableLayoutPanelSpindle.Controls.Add(this.groupBoxCOMPort, 0, 0);
            this.tableLayoutPanelSpindle.Controls.Add(this.groupBoxSpindleControl, 1, 0);
            this.tableLayoutPanelSpindle.Controls.Add(this.groupBoxSpindleSpeed, 2, 0);
            this.tableLayoutPanelSpindle.Location = new System.Drawing.Point(3, 252);
            this.tableLayoutPanelSpindle.Name = "tableLayoutPanelSpindle";
            this.tableLayoutPanelSpindle.RowCount = 1;
            this.tableLayoutPanelSpindle.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelSpindle.Size = new System.Drawing.Size(728, 85);
            this.tableLayoutPanelSpindle.TabIndex = 7;
            // 
            // groupBoxSpindleSpeed
            // 
            this.groupBoxSpindleSpeed.CausesValidation = false;
            this.groupBoxSpindleSpeed.Controls.Add(this.gTrackBarSpindleSpeed);
            this.groupBoxSpindleSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxSpindleSpeed.Enabled = false;
            this.groupBoxSpindleSpeed.Location = new System.Drawing.Point(189, 3);
            this.groupBoxSpindleSpeed.Name = "groupBoxSpindleSpeed";
            this.groupBoxSpindleSpeed.Padding = new System.Windows.Forms.Padding(10, 3, 10, 3);
            this.groupBoxSpindleSpeed.Size = new System.Drawing.Size(177, 79);
            this.groupBoxSpindleSpeed.TabIndex = 8;
            this.groupBoxSpindleSpeed.TabStop = false;
            this.groupBoxSpindleSpeed.Text = "Spindle Speed";
            // 
            // gTrackBarSpindleSpeed
            // 
            this.gTrackBarSpindleSpeed.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.gTrackBarSpindleSpeed.BackColor = System.Drawing.SystemColors.Control;
            this.gTrackBarSpindleSpeed.ChangeLarge = 1000;
            this.gTrackBarSpindleSpeed.ChangeSmall = 100;
            this.gTrackBarSpindleSpeed.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gTrackBarSpindleSpeed.JumpToMouse = true;
            this.gTrackBarSpindleSpeed.Label = "Speed";
            this.gTrackBarSpindleSpeed.Location = new System.Drawing.Point(10, 16);
            this.gTrackBarSpindleSpeed.Margin = new System.Windows.Forms.Padding(3, 3, 2, 3);
            this.gTrackBarSpindleSpeed.MaxValue = 24000;
            this.gTrackBarSpindleSpeed.Name = "gTrackBarSpindleSpeed";
            this.gTrackBarSpindleSpeed.Size = new System.Drawing.Size(157, 60);
            this.gTrackBarSpindleSpeed.SliderCapEnd = System.Drawing.Drawing2D.LineCap.NoAnchor;
            this.gTrackBarSpindleSpeed.SliderSize = new System.Drawing.Size(15, 15);
            this.gTrackBarSpindleSpeed.SliderWidthHigh = 3F;
            this.gTrackBarSpindleSpeed.SliderWidthLow = 3F;
            this.gTrackBarSpindleSpeed.TabIndex = 0;
            this.gTrackBarSpindleSpeed.TabStop = false;
            this.gTrackBarSpindleSpeed.TickInterval = 2400;
            this.gTrackBarSpindleSpeed.TickThickness = 1F;
            this.gTrackBarSpindleSpeed.UpDownShow = false;
            this.gTrackBarSpindleSpeed.Value = 6000;
            this.gTrackBarSpindleSpeed.ValueAdjusted = 6000F;
            this.gTrackBarSpindleSpeed.ValueDivisor = gTrackBar.gTrackBar.eValueDivisor.e1;
            this.gTrackBarSpindleSpeed.ValueStrFormat = null;
            this.gTrackBarSpindleSpeed.ValueChanged += new gTrackBar.gTrackBar.ValueChangedEventHandler(this.gTrackBarSpindleSpeed_ValueChanged);
            this.gTrackBarSpindleSpeed.DoubleClick += new System.EventHandler(this.gTrackBarSpindleSpeed_DoubleClick);
            // 
            // panelMDI
            // 
            this.panelMDI.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panelMDI.Controls.Add(this.buttonSettings);
            this.panelMDI.Controls.Add(this.buttonTerminal);
            this.panelMDI.Controls.Add(this.buttonGraphs);
            this.panelMDI.Location = new System.Drawing.Point(0, 0);
            this.panelMDI.Margin = new System.Windows.Forms.Padding(0);
            this.panelMDI.Name = "panelMDI";
            this.panelMDI.Size = new System.Drawing.Size(734, 61);
            this.panelMDI.TabIndex = 9;
            // 
            // tableLayoutPanelBase
            // 
            this.tableLayoutPanelBase.ColumnCount = 1;
            this.tableLayoutPanelBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBase.Controls.Add(this.panelMDI, 0, 0);
            this.tableLayoutPanelBase.Controls.Add(this.tableLayoutPanelSpindle, 0, 2);
            this.tableLayoutPanelBase.Controls.Add(this.panelMain, 0, 1);
            this.tableLayoutPanelBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelBase.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanelBase.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanelBase.Name = "tableLayoutPanelBase";
            this.tableLayoutPanelBase.RowCount = 3;
            this.tableLayoutPanelBase.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBase.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelBase.Size = new System.Drawing.Size(734, 340);
            this.tableLayoutPanelBase.TabIndex = 10;
            // 
            // panelMain
            // 
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 61);
            this.panelMain.Margin = new System.Windows.Forms.Padding(0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(734, 188);
            this.panelMain.TabIndex = 10;
            // 
            // timerInitialPoll
            // 
            this.timerInitialPoll.Interval = 500;
            this.timerInitialPoll.Tick += new System.EventHandler(this.timerInitialPoll_Tick);
            // 
            // toolStripStatusLabelVFDStatus
            // 
            this.toolStripStatusLabelVFDStatus.Image = global::SpindleTalker2.Properties.Resources.redLED;
            this.toolStripStatusLabelVFDStatus.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.toolStripStatusLabelVFDStatus.Name = "toolStripStatusLabelVFDStatus";
            this.toolStripStatusLabelVFDStatus.Size = new System.Drawing.Size(136, 17);
            this.toolStripStatusLabelVFDStatus.Text = "VFD (Not Connected)";
            // 
            // buttonSettings
            // 
            this.buttonSettings.AutoSize = true;
            this.buttonSettings.FlatAppearance.BorderSize = 0;
            this.buttonSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonSettings.Image = global::SpindleTalker2.Properties.Resources.Settings48x48;
            this.buttonSettings.Location = new System.Drawing.Point(135, -3);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(75, 70);
            this.buttonSettings.TabIndex = 2;
            this.buttonSettings.TabStop = false;
            this.buttonSettings.Tag = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.MDI_Select_Click);
            // 
            // buttonTerminal
            // 
            this.buttonTerminal.AutoSize = true;
            this.buttonTerminal.FlatAppearance.BorderSize = 0;
            this.buttonTerminal.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonTerminal.Image = global::SpindleTalker2.Properties.Resources.terminal48x48;
            this.buttonTerminal.Location = new System.Drawing.Point(66, -3);
            this.buttonTerminal.Name = "buttonTerminal";
            this.buttonTerminal.Size = new System.Drawing.Size(75, 70);
            this.buttonTerminal.TabIndex = 1;
            this.buttonTerminal.TabStop = false;
            this.buttonTerminal.Tag = "Terminal";
            this.buttonTerminal.UseVisualStyleBackColor = true;
            this.buttonTerminal.Click += new System.EventHandler(this.MDI_Select_Click);
            // 
            // buttonGraphs
            // 
            this.buttonGraphs.AutoSize = true;
            this.buttonGraphs.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonGraphs.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.buttonGraphs.FlatAppearance.BorderSize = 0;
            this.buttonGraphs.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonGraphs.Image = global::SpindleTalker2.Properties.Resources.gauge48x48;
            this.buttonGraphs.Location = new System.Drawing.Point(6, 5);
            this.buttonGraphs.Name = "buttonGraphs";
            this.buttonGraphs.Size = new System.Drawing.Size(54, 54);
            this.buttonGraphs.TabIndex = 0;
            this.buttonGraphs.TabStop = false;
            this.buttonGraphs.Tag = "Graphs";
            this.buttonGraphs.UseVisualStyleBackColor = false;
            this.buttonGraphs.Click += new System.EventHandler(this.MDI_Select_Click);
            // 
            // buttonConnect
            // 
            this.buttonConnect.AutoSize = true;
            this.buttonConnect.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonConnect.FlatAppearance.BorderSize = 0;
            this.buttonConnect.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonConnect.Image = global::SpindleTalker2.Properties.Resources.disconnect2;
            this.buttonConnect.Location = new System.Drawing.Point(17, 20);
            this.buttonConnect.Name = "buttonConnect";
            this.buttonConnect.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonConnect.Size = new System.Drawing.Size(38, 38);
            this.buttonConnect.TabIndex = 1;
            this.buttonConnect.TabStop = false;
            this.toolTipBase.SetToolTip(this.buttonConnect, "Click connect or disconnect COM port");
            this.buttonConnect.UseVisualStyleBackColor = true;
            this.buttonConnect.Click += new System.EventHandler(this.buttonConnect_Click);
            // 
            // buttonStart
            // 
            this.buttonStart.AutoSize = true;
            this.buttonStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonStart.FlatAppearance.BorderSize = 0;
            this.buttonStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStart.Image = global::SpindleTalker2.Properties.Resources.Start;
            this.buttonStart.Location = new System.Drawing.Point(6, 18);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.buttonStart.Size = new System.Drawing.Size(38, 38);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.TabStop = false;
            this.toolTipBase.SetToolTip(this.buttonStart, "Click to start spindle");
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.AutoSize = true;
            this.buttonStop.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.buttonStop.Enabled = false;
            this.buttonStop.FlatAppearance.BorderSize = 0;
            this.buttonStop.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.buttonStop.Image = global::SpindleTalker2.Properties.Resources.Stop;
            this.buttonStop.Location = new System.Drawing.Point(50, 18);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(38, 38);
            this.buttonStop.TabIndex = 1;
            this.buttonStop.TabStop = false;
            this.toolTipBase.SetToolTip(this.buttonStop, "Click to stop spindle");
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // toolStripStatusLabelComPort
            // 
            this.toolStripStatusLabelComPort.DoubleClickEnabled = true;
            this.toolStripStatusLabelComPort.Image = global::SpindleTalker2.Properties.Resources.redLED;
            this.toolStripStatusLabelComPort.Name = "toolStripStatusLabelComPort";
            this.toolStripStatusLabelComPort.Size = new System.Drawing.Size(131, 17);
            this.toolStripStatusLabelComPort.Text = "COMx Disconnected";
            // 
            // toolStripStatusRPM
            // 
            this.toolStripStatusRPM.Image = global::SpindleTalker2.Properties.Resources.orangeLED;
            this.toolStripStatusRPM.Margin = new System.Windows.Forms.Padding(10, 3, 0, 2);
            this.toolStripStatusRPM.Name = "toolStripStatusRPM";
            this.toolStripStatusRPM.Size = new System.Drawing.Size(237, 17);
            this.toolStripStatusRPM.Text = "Current RPM Unknown (Not Connected)";
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(734, 362);
            this.Controls.Add(this.tableLayoutPanelBase);
            this.Controls.Add(this.statusStripBase);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainWindow";
            this.Text = "Spindle Talker 2";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SpindleTalker_FormClosing);
            this.Load += new System.EventHandler(this.SpindleTalker_Load);
            this.statusStripBase.ResumeLayout(false);
            this.statusStripBase.PerformLayout();
            this.groupBoxSpindleControl.ResumeLayout(false);
            this.groupBoxSpindleControl.PerformLayout();
            this.groupBoxCOMPort.ResumeLayout(false);
            this.groupBoxCOMPort.PerformLayout();
            this.groupBoxQuickSets.ResumeLayout(false);
            this.tableLayoutPanelSpindle.ResumeLayout(false);
            this.groupBoxSpindleSpeed.ResumeLayout(false);
            this.panelMDI.ResumeLayout(false);
            this.panelMDI.PerformLayout();
            this.tableLayoutPanelBase.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.StatusStrip statusStripBase;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelComPort;
        private System.Windows.Forms.GroupBox groupBoxSpindleControl;
        private System.Windows.Forms.GroupBox groupBoxCOMPort;
        private System.Windows.Forms.GroupBox groupBoxQuickSets;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanelQuickSets;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelSpindle;
        private System.Windows.Forms.GroupBox groupBoxSpindleSpeed;
        private System.Windows.Forms.ToolTip toolTipBase;
        private System.Windows.Forms.Panel panelMDI;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonTerminal;
        private System.Windows.Forms.Button buttonGraphs;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBase;
        private System.Windows.Forms.Panel panelMain;
        private gTrackBar.gTrackBar gTrackBarSpindleSpeed;
        private System.Windows.Forms.CheckBox checkBoxReverse;
        private System.Windows.Forms.Button buttonConnect;
        private System.Windows.Forms.Timer timerInitialPoll;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelVFDStatus;
        public System.Windows.Forms.ToolStripStatusLabel toolStripStatusRPM;
    }
}

