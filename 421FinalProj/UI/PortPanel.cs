// PortPanel.cs
using System.Drawing;
using System.Windows.Forms;

namespace _421FinalProj
{
    public class PortPanel : Panel
    {
        public PortPanel(string position, Panel parent)
        {
            Name = $"{parent.Name}/Port/{position}";
            Parent = parent;
            Size = new Size(10, 10);
            BackColor = Color.Black;
            Cursor = Cursors.Cross;

            Location = position == "Left"
                       ? new Point(3, parent.Height / 2 - 5)
                       : new Point(parent.Width - 13, parent.Height / 2 - 5);

            parent.Controls.Add(this);
            BringToFront();

            MouseDown += (_, e) =>
            {
                if (e.Button == MouseButtons.Left &&
                    FindForm() is Form1 f &&
                    f.MainCanvas is { } canvas)
                {
                    canvas.StartRubberBand(this);
                }
            };
        }

        // helper returns center of this port in canvas coords
        public Point CenterOnCanvas()
        {
            var center = new Point(Left + Width / 2, Top + Height / 2);
            var screen = Parent.PointToScreen(center);
            return ((Form1)FindForm()).MainCanvas.PointToClient(screen);
        }
    }
}
