using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace SpindleTalker2
{
    /// <summary>
    /// Specifies horizontal locking positions
    /// </summary>
    [Flags]
    public enum SmartLocking { None, Left, Middle, Right, All = Left | Middle | Right };

    /// <summary>
    /// Represents a customizable TrackBar like control
    /// </summary>
    [DefaultEvent("ValueChanged")]
    abstract class Slider : Control
    {
        private BufferedGraphics _bufGraphics;
        private Rectangle _knobRect;
        private int _lastX;
        private bool _isMouseOverSlider, _draggingKnob;

        protected Slider()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint
                | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            AllowKeyNavigation = true;
            UpdateGraphicsBuffer();
        }

        #region Overrides
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (!AllowKeyNavigation) return;

            switch (e.KeyCode)
            {
                case Keys.D1: Percent = 0.1f; break;
                case Keys.D2: Percent = 0.2f; break;
                case Keys.D3: Percent = 0.3f; break;
                case Keys.D4: Percent = 0.4f; break;
                case Keys.D5: Percent = 0.5f; break;
                case Keys.D6: Percent = 0.6f; break;
                case Keys.D7: Percent = 0.7f; break;
                case Keys.D8: Percent = 0.8f; break;
                case Keys.D9: Percent = 0.9f; break;
                case Keys.D0: Percent = 0.0f; break;
                case Keys.Oemplus: Percent += 0.1f; break;
                case Keys.OemMinus: Percent -= 0.1f; break;
            }
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            if (AllowQuickTracking && !_draggingKnob)
            {
                KnobX = e.X - _knobBitmap.Width / 2;
                this.Invalidate(); // Need to invalidate everything here
            }
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (_isMouseOverSlider)
            {
                _isMouseOverSlider = false;
                this.Invalidate(_knobRect);
            }
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (SmartLockAmount > 0)
            {
                int wholePercent = (int)((float)_knobRect.Left / (this.Width - _knobRect.Width) * 100 + 0.5);

                // If around 0%
                if (SmartLockingFlags.HasFlag(SmartLocking.Left) && wholePercent < SmartLockAmount)
                {
                    Percent = 0;
                }
                // If around 50%
                else if (SmartLockingFlags.HasFlag(SmartLocking.Middle) && Math.Abs(wholePercent - 50) < SmartLockAmount)
                {
                    Percent = 0.5f;
                }
                // If around 100%
                else if (SmartLockingFlags.HasFlag(SmartLocking.Right) && 100 - wholePercent < SmartLockAmount)
                {
                    Percent = 1f;
                }
            }

            _draggingKnob = false;
            this.Invalidate();
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (UseHandCursorForKnob)
            {
                if (_knobRect.Contains(e.Location))
                {
                    if (!Cursor.Equals(Cursors.Hand))
                        Cursor = Cursors.Hand;
                }
                else if (!Cursor.Equals(Cursors.Hand))
                {
                    Cursor = Cursors.Default;
                }
            }

            bool maxedLeft = e.X < 0 && Percent == 0f;
            bool maxedRight = e.X > this.Width && Percent == 1f;

            // If left mouse button pressed and cursor x is within slider bounds
            if (_draggingKnob && !(maxedLeft || maxedRight))
            {
                KnobX += e.X - _lastX;
                _lastX = e.X;
                this.Invalidate();
            }

            // Only update rollover effects if a change has occured
            if (!_isMouseOverSlider && _knobRect.Contains(e.Location))
            {
                _isMouseOverSlider = true;
                this.Invalidate();
            }
            else if (_isMouseOverSlider && !_knobRect.Contains(e.Location))
            {
                _isMouseOverSlider = false;
                this.Invalidate();
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _lastX = e.X;
            _draggingKnob = (_knobRect.Contains(e.Location) && e.Button == MouseButtons.Left);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            _knobRect.X = (int)((Size.Width - _knobRect.Width) * Percent + 0.5);
            UpdateGraphicsBuffer();

            if (_knobBitmap != null)
            {
                _knobRect.Height = _knobBitmap.Height;
                this.Height = _knobBitmap.Height;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            _bufGraphics.Graphics.Clear(this.BackColor);
            DrawBar(_bufGraphics.Graphics);

            if (_knobHoverBitmap != null && (_isMouseOverSlider || _draggingKnob))
            {
                _bufGraphics.Graphics.DrawImage(_knobHoverBitmap, _knobRect);
            }
            else if (!this.Enabled && _disabledImage != null)
            {
                _bufGraphics.Graphics.DrawImage(_disabledImage, _knobRect);
            }
            else if (_knobBitmap != null)
            {
                _bufGraphics.Graphics.DrawImage(_knobBitmap, _knobRect);
            }

            _bufGraphics.Render(e.Graphics);
        }
        #endregion

        /// <summary>
        /// Gets a string describing the current Value of the control
        /// </summary>
        public string GetValueInfo()
        {
            const string FORMAT = @"Value={0} ({1}%)";
            return String.Format(FORMAT, _value, (int)(Percent * 100 + 0.5));
        }

        private void UpdateGraphicsBuffer()
        {
            if (this.Width > 0 && this.Height > 0)
            {
                BufferedGraphicsContext context = BufferedGraphicsManager.Current;
                context.MaximumBuffer = new Size(this.Width + 1, this.Height + 1);
                _bufGraphics = context.Allocate(this.CreateGraphics(), this.ClientRectangle);
                IncreaseGraphicsQuality(_bufGraphics.Graphics);
            }
        }

        private static void IncreaseGraphicsQuality(Graphics graphics)
        {
            // graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.InterpolationMode = InterpolationMode.HighQualityBilinear;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
        }

        /// <summary>
        /// Updates the knobs x position
        /// </summary>
        private void UpdateKnobX()
        {
            _knobRect.X = (int)((Size.Width - _knobRect.Width) * Percent + 0.5);
        }

        /// <summary>
        /// Draws the back bar or any graphics, behind the knob
        /// </summary>
        protected abstract void DrawBar(Graphics graphics);

        [Description("Occurs when the scroll percentage is changed")]
        public event EventHandler ValueChanged;
        /// <summary>
        /// Raises when the value of the Value property is changed
        /// </summary>
        protected virtual void OnValueChanged()
        {
            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);
        }

        #region Properties
        private Bitmap _disabledImage;
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("The image to be displayed when this control is disabled")]
        public Bitmap DisabledImage
        {
            get { return _disabledImage; }
            set
            {
                _disabledImage = value;
                this.Invalidate(_knobRect);
            }
        }

        private Bitmap _knobBitmap;
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("The image to be displayed when a users mouse leaves the slider")]
        public Bitmap KnobImage
        {
            get { return _knobBitmap; }
            set
            {
                _knobBitmap = value;

                if (_knobBitmap != null)
                {
                    _knobRect.Size = _knobBitmap.Size;
                    this.Height = _knobBitmap.Height;
                }

                this.Invalidate(_knobRect);
            }
        }

        private Bitmap _knobHoverBitmap;
        [Category("Appearance")]
        [DefaultValue(null)]
        [Description("The image to be displayed when a user rolls the mouse over the slider")]
        public Bitmap KnobHoverImage
        {
            get { return _knobHoverBitmap; }
            set
            {
                _knobHoverBitmap = value;
                this.Invalidate(_knobRect);
            }
        }

        /// <summary>
        /// Gets or sets the x position of the knob
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int KnobX
        {
            get { return _knobRect.X; }
            set
            {
                if (value < 0)
                {
                    value = 0;
                }
                else if (value > this.Width - _knobRect.Width)
                {
                    value = this.Width - _knobRect.Width;
                }

                Percent = (float)_knobRect.Left / (this.Width - _knobRect.Width);
                _knobRect.X = value;
            }
        }

        /// <summary>
        /// Gets the center point of the knob
        /// </summary>
        [Browsable(false)]
        public Point KnobCenter
        {
            get
            {
                int x = _knobRect.X - _knobRect.Width;
                int y = _knobRect.Y - _knobRect.Height;
                return new Point(x, y);
            }
        }

        private int _maximum = 100;
        [Description("The highest possible value")]
        [Category("Behavior")]
        [DefaultValue(100)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public int Maximum
        {
            get { return _maximum; }
            set
            {
                if (value <= _minimum)
                    throw new ArgumentOutOfRangeException("Value must be greater than Minimum");

                _maximum = value;

                if (_value > _maximum)
                    Value = _maximum;

                UpdateKnobX();
                this.Invalidate();
            }
        }

        private int _minimum;
        [Description("The lowest possible value")]
        [Category("Behavior")]
        [DefaultValue(0)]
        [RefreshProperties(RefreshProperties.Repaint)]
        public int Minimum
        {
            get { return _minimum; }
            set
            {
                if (value >= _maximum)
                    throw new ArgumentOutOfRangeException("Value must be less than Maximum");

                _minimum = value;

                if (_value < _minimum)
                    Value = _minimum;

                UpdateKnobX();
                this.Invalidate();
            }
        }

        private int _value;
        [Description("The position of the slider")]
        [Category("Behavior")]
        [DefaultValue(0)]
        public int Value
        {
            get { return _value; }
            set
            {
                if (value < _minimum || value > _maximum)
                    throw new ArgumentOutOfRangeException("value must be less than or equal to Maximum and greater than or equal to Minimum");

                bool changed = value != _value;

                if (changed)
                {
                    _value = value;
                    OnValueChanged();
                    this.Invalidate();
                }

                UpdateKnobX();
            }
        }

        // Provides a much simplier interface for adjusting the value internally
        /// <summary>
        /// Gets or sets the current value as a percentage from 0-1
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public float Percent
        {
            get { return (_value - _minimum) / (float)(_maximum - _minimum); }
            set
            {
                if (value > 1) value = 1;
                if (value < 0) value = 0;
                float val = (_maximum - _minimum) * value;
                Value = (int)(val + _minimum + 0.5);
                this.Invalidate();
            }
        }

        [Category("Behavior")]
        [DefaultValue(0)]
        [Description("Determines the range in percentage to snap at the specified locking points")]
        public int SmartLockAmount { get; set; }

        [Category("Behavior")]
        [DefaultValue(SmartLocking.None)]
        [Description("Determines the snapping behavior of the tracker")]
        public SmartLocking SmartLockingFlags { get; set; }

        [Category("Behavior")]
        [DefaultValue(false)]
        [Description("Determines whether to allow the user to set the knob's position by clicking anywhere on the control")]
        public bool AllowQuickTracking { get; set; }

        [Category("Appearance")]
        [DefaultValue(false)]
        [Description("Determines whether to use the hand cursor when the mouse is over the knob")]
        public bool UseHandCursorForKnob { get; set; }

        [Category("Behavior")]
        [DefaultValue(true)]
        [Description("When true, allows the user to slider the knob using 0-9 and -+")]
        public bool AllowKeyNavigation { get; set; }
        #endregion
    }
}
