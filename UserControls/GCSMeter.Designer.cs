namespace SpindleTalker2
{
    partial class GCSMeter
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBoxBase = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanelBase = new System.Windows.Forms.TableLayoutPanel();
            this.lbAnalogMeterBase = new LBSoft.IndustrialCtrls.Meters.LBAnalogMeter();
            this.labelValue = new System.Windows.Forms.Label();
            this.groupBoxBase.SuspendLayout();
            this.tableLayoutPanelBase.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBoxBase
            // 
            this.groupBoxBase.Controls.Add(this.tableLayoutPanelBase);
            this.groupBoxBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBoxBase.Location = new System.Drawing.Point(0, 0);
            this.groupBoxBase.Name = "groupBoxBase";
            this.groupBoxBase.Size = new System.Drawing.Size(204, 229);
            this.groupBoxBase.TabIndex = 0;
            this.groupBoxBase.TabStop = false;
            this.groupBoxBase.Text = "GCSMeter";
            // 
            // tableLayoutPanelBase
            // 
            this.tableLayoutPanelBase.ColumnCount = 1;
            this.tableLayoutPanelBase.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBase.Controls.Add(this.labelValue, 0, 1);
            this.tableLayoutPanelBase.Controls.Add(this.lbAnalogMeterBase, 0, 0);
            this.tableLayoutPanelBase.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanelBase.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanelBase.Name = "tableLayoutPanelBase";
            this.tableLayoutPanelBase.RowCount = 2;
            this.tableLayoutPanelBase.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanelBase.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanelBase.Size = new System.Drawing.Size(198, 210);
            this.tableLayoutPanelBase.TabIndex = 0;
            // 
            // lbAnalogMeterBase
            // 
            this.lbAnalogMeterBase.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lbAnalogMeterBase.BackColor = System.Drawing.Color.Transparent;
            this.lbAnalogMeterBase.BodyColor = System.Drawing.SystemColors.Control;
            this.lbAnalogMeterBase.Location = new System.Drawing.Point(24, 6);
            this.lbAnalogMeterBase.MaxValue = 100D;
            this.lbAnalogMeterBase.MeterStyle = LBSoft.IndustrialCtrls.Meters.LBAnalogMeter.AnalogMeterStyle.Circular;
            this.lbAnalogMeterBase.MinValue = 0D;
            this.lbAnalogMeterBase.Name = "lbAnalogMeterBase";
            this.lbAnalogMeterBase.NeedleColor = System.Drawing.Color.DarkRed;
            this.lbAnalogMeterBase.Renderer = null;
            this.lbAnalogMeterBase.ScaleColor = System.Drawing.Color.White;
            this.lbAnalogMeterBase.ScaleDivisions = 6;
            this.lbAnalogMeterBase.ScaleSubDivisions = 3;
            this.lbAnalogMeterBase.Size = new System.Drawing.Size(150, 150);
            this.lbAnalogMeterBase.TabIndex = 0;
            this.lbAnalogMeterBase.Value = 0D;
            this.lbAnalogMeterBase.ViewGlass = false;
            // 
            // label1
            // 
            this.labelValue.AutoSize = true;
            this.labelValue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.labelValue.Font = new System.Drawing.Font("Digital-7", 26.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelValue.ForeColor = System.Drawing.Color.DarkRed;
            this.labelValue.Location = new System.Drawing.Point(3, 172);
            this.labelValue.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.labelValue.Name = "label1";
            this.labelValue.Size = new System.Drawing.Size(192, 35);
            this.labelValue.TabIndex = 1;
            this.labelValue.Text = "000.00 Hz";
            this.labelValue.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GCSMeter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBoxBase);
            this.Name = "GCSMeter";
            this.Size = new System.Drawing.Size(204, 229);
            this.Load += new System.EventHandler(this.GCSMeter_Load);
            this.SizeChanged += new System.EventHandler(this.GCSMeter_SizeChanged);
            this.groupBoxBase.ResumeLayout(false);
            this.tableLayoutPanelBase.ResumeLayout(false);
            this.tableLayoutPanelBase.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBoxBase;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanelBase;
        private LBSoft.IndustrialCtrls.Meters.LBAnalogMeter lbAnalogMeterBase;
        private System.Windows.Forms.Label labelValue;
    }
}
