using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using user_program.FormAll;

namespace user_program.FormAll
{
    public class ToggleSwitch : Control
    {
        private bool isOn = false;

        public bool IsOn
        {
            get { return isOn; }
            set
            {
                isOn = value;
                this.Invalidate();
            }
        }

        public Color OnColor { get; set; } = Color.Green;
        public Color OffColor { get; set; } = Color.Gray;
        public Color CircleColor { get; set; } = Color.White;

        public ToggleSwitch()
        {
            this.Size = new Size(50, 25);
            this.DoubleBuffered = true;
            this.Cursor = Cursors.Hand;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int radius = this.Height - 2;

            GraphicsPath path = new GraphicsPath();
            path.StartFigure();
            path.AddArc(0, 0, radius, radius, 90, 180);
            path.AddArc(this.Width - radius - 1, 0, radius, radius, 270, 180);
            path.CloseFigure();

            using (SolidBrush brush = new SolidBrush(isOn ? OnColor : OffColor))
            {
                g.FillPath(brush, path);
            }

            int circleDiameter = this.Height - 6;
            int circleX = isOn ? this.Width - circleDiameter - 2 : 2;
            Rectangle circle = new Rectangle(circleX, 2, circleDiameter, circleDiameter);

            using (SolidBrush circleBrush = new SolidBrush(CircleColor))
            {
                g.FillEllipse(circleBrush, circle);
            }
        }

        // ToggleSwitch.cs 맨 아래에 추가
        public event EventHandler ToggleChanged;

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            IsOn = !IsOn;
            ToggleChanged?.Invoke(this, EventArgs.Empty); // 상태 바뀔 때 이벤트 발생
        }
    }
}
