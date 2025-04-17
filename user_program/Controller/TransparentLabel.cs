using System;
using System.Drawing;
using System.Windows.Forms;

namespace user_program.trans
{
    public class TransparentLabel : Label
    {
        public TransparentLabel()
        {
            this.SetStyle(ControlStyles.Opaque, true);
         this.BackColor = Color.Transparent;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                    cp.ExStyle |= 0x00000020; // WS_EX_TRANSPARENT
                return cp;
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            // 배경을 그리지 않음 → 진짜 투명하게 처리
        }
    }
}