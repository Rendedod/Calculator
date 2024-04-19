using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Windows.Forms;

namespace Calculator
{

    public class RoudedBtton : System.Windows.Forms.Button
        {
        private StringFormat format = new StringFormat();

        public ushort radius { get; set; } = 100;
        public byte opasity { get; set; } = 20;
        public bool reversMode { get; set; } = false;

        Rectangle rect;
        Graphics graph;
        GraphicsPath rectGp;

        public RoudedBtton()
        {
            SetStyle(
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw |
                ControlStyles.SupportsTransparentBackColor |
                ControlStyles.UserPaint,
                true
                );

            format.Alignment = StringAlignment.Center;
            format.LineAlignment = StringAlignment.Center;

            Cursor = Cursors.Hand;
            ForeColor = Color.WhiteSmoke;
            BackColor = SystemColors.ControlDarkDark;

            MinimumSize = new Size( 30, 30 );

            DoubleBuffered = true;

            radius = new[] { radius, (ushort)Height, (ushort)Width}.Min();
            int opasityCalculating = numberValidator(2.55 * (100 - opasity));
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            int opasityCalculating = numberValidator(2.55 * (100 - opasity));
            graph = e.Graphics;
            graph.SmoothingMode = SmoothingMode.HighQuality;
            radius = new[] { radius, (ushort)Height, (ushort)Width }.Min();

            graph.Clear(Parent.BackColor);
            rect = new Rectangle(0, 0, Width - 1, Height - 1);

            rectGp = RoundedRectangle(rect);

            graph.DrawPath(new Pen(FlatAppearance.BorderColor), rectGp);
            graph.FillPath(new SolidBrush(Color.FromArgb(opasityCalculating, BackColor)), rectGp);

            graph.DrawString(Text, Font, new SolidBrush(ForeColor), rect, format);          
        }

        private GraphicsPath RoundedRectangle(Rectangle rect)
        {
            GraphicsPath gp = new GraphicsPath();

            gp.AddArc(rect.X, rect.Y, radius, radius, 180, 90);
            gp.AddArc(rect.X + rect.Width - radius, rect.Y, radius, radius, 270, 90);
            gp.AddArc(rect.X + rect.Width - radius, rect.Y + rect.Height - radius, radius, radius, 0, 90);
            gp.AddArc(rect.X, rect.Y + rect.Height - radius, radius, radius, 90, 90);

            gp.CloseFigure();

            return gp;
        }

        protected override void OnMouseEnter(EventArgs e) 
        {
            base.OnMouseEnter(e); 
            MouseReaction(40, true);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MouseReaction(40);
        }

        protected override void OnMouseDown(MouseEventArgs mevent)
        {
            base.OnMouseDown(mevent);
            MouseReaction(15, true);
        }

        protected override void OnMouseUp(MouseEventArgs mevent)
        {
            base.OnMouseUp(mevent);
            MouseReaction(15);
        }

        /// <summary>
        /// Укызывается уровень прозрачности обьекта. <br/>
        /// Чем выше значение тем выше прозрачности обьекта.
        /// </summary>
        private void MouseReaction(byte blackout = 0, bool darkMode = false)
        {
            if ( reversMode & darkMode || !reversMode & !darkMode)
            {
                BackColor = Color.FromArgb(numberValidator(BackColor.R + blackout), numberValidator(BackColor.G + blackout), numberValidator(BackColor.B + blackout));
            }
            else
            {
                BackColor = Color.FromArgb(numberValidator(BackColor.R - blackout), numberValidator(BackColor.G - blackout), numberValidator(BackColor.B - blackout));
            }

            InitLayout();
        }

        private int numberValidator(double number)
        {
            if (number < 0) return (byte)0;

            else if (number > 255) return (byte)255;

            else return Convert.ToByte(number);
        }
    }
}


