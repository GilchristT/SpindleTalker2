using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SpindleTalker2
{
    public partial class GCSMeter : UserControl
    {

        #region Attributes

        [Description("Title text for control"), Category("Appearance"), DefaultValue("GCS Meter"), Browsable(true)]
        public String Title
        {
            get { return groupBoxBase.Text; }
            set { groupBoxBase.Text = value; }
        }

        private string _Units;
        [Description("Suffix for value"), Category("Appearance"), DefaultValue(""), Browsable(true)]
        public String Units
        {
            get { return _Units; }
            set 
            {
                _Units = value;
                labelValue.Text = lbAnalogMeterBase.Value.ToString() + " " + _Units;
            }
        }

        [Description("Double - Value"), Category("Appearance"), DefaultValue("0D"), Browsable(true)]
        public Double Value
        {
            get { return lbAnalogMeterBase.Value; }
            set
            {
                lbAnalogMeterBase.Value = value;
                labelValue.Text = value.ToString();
                if (!String.IsNullOrEmpty(_Units)) labelValue.Text += " " + _Units;
            }
        }

        [Description("Colour of needle and value text"), Category("Appearance"), DefaultValue("DarkRed"), Browsable(true)]
        public Color Colour
        {
            get { return lbAnalogMeterBase.NeedleColor; }
            set
            {
                lbAnalogMeterBase.NeedleColor = value;
                labelValue.ForeColor = value;
            }
        }

        [Description("Double - Scale minimum"), Category("Appearance"), DefaultValue("0D"), Browsable(true)]
        public Double ScaleMinValue
        {
            get { return lbAnalogMeterBase.MinValue; }
            set { lbAnalogMeterBase.MinValue = value; }
        }

        [Description("Double - Scale maximim"), Category("Appearance"), DefaultValue("100D"), Browsable(true)]
        public Double ScaleMaxValue
        {
            get { return lbAnalogMeterBase.MaxValue; }
            set { lbAnalogMeterBase.MaxValue = value; }
        }

        [Description("Int - Scale divisions"), Category("Appearance"), DefaultValue("5"), Browsable(true)]
        public int ScaleDivisions
        {
            get { return lbAnalogMeterBase.ScaleDivisions; }
            set { lbAnalogMeterBase.ScaleDivisions = value; }
        }

        [Description("Int - Scale subdivisions"), Category("Appearance"), DefaultValue("3"), Browsable(true)]
        public int ScaleSubDivisions
        {
            get { return lbAnalogMeterBase.ScaleSubDivisions; }
            set { lbAnalogMeterBase.ScaleSubDivisions = value; }
        }

        #endregion

        public GCSMeter()
        {
            InitializeComponent();
            resizeMeter();
        }

        private void GCSMeter_SizeChanged(object sender, EventArgs e)
        {
            resizeMeter();
        }

        private void GCSMeter_Load(object sender, EventArgs e)
        {
            resizeMeter();
        }

        private void resizeMeter()
        {
            TableLayoutPanelCellPosition pos = tableLayoutPanelBase.GetCellPosition(lbAnalogMeterBase);
            int width = tableLayoutPanelBase.GetColumnWidths()[pos.Column];
            int height = tableLayoutPanelBase.GetRowHeights()[pos.Row];
            
            if(width > height) lbAnalogMeterBase.Size = new Size(height, height);
            else lbAnalogMeterBase.Size = new Size(width, width);
        }
    }
}
