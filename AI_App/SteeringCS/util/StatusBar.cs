using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteeringCS.util
{
    public class StatusBar: ProgressBar, IComponent
    {
        public StatusBar()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = ClientRectangle;
            Graphics g = e.Graphics;
            ProgressBarRenderer.DrawHorizontalBar(g, rect);

            // Border.
            rect.Inflate(-1, -1);

            // Bar.
            if (Value > 0)
            {
                Brush brush = new SolidBrush(ForeColor);
                g.FillRectangle(brush, rect.X, rect.Y, (int)Math.Round((float)Value / Maximum * rect.Width), rect.Height);
            }
            
            string text = Value.ToString() + '/' + Maximum;

            // Text.
            using (Font f = new Font(FontFamily.GenericMonospace, 10))
            {
                SizeF strLen = g.MeasureString(text, f);
                Point location = new Point((int)((rect.Width / 2) - (strLen.Width / 2)), (int)((rect.Height / 2) - (strLen.Height / 2)));
                g.DrawString(text, f, Brushes.Black, location);
            }
        }

        // Prevents flickering of text.
        protected override CreateParams CreateParams {
            get {
                CreateParams result = base.CreateParams;
                result.ExStyle |= 0x02000000; // WS_EX_COMPOSITED 
                return result;
            }
        }
    }
}
