using _421FinalProj;

public enum PortSide { Left, Right }

public class PortPanel : Panel
{
    public PortSide Side { get; }           // expose side for tests

    public PortPanel(string side, Panel parent)
    {
        Side = side == "Left" ? PortSide.Left : PortSide.Right;
        Name = $"{parent.Name}/Port/{side}";
        Parent = parent;
        Size = new Size(10, 10);
        BackColor = Color.Black;
        Cursor = Cursors.Cross;

        Location = Side == PortSide.Left
            ? new Point(3, parent.Height / 2 - 5)
            : new Point(parent.Width - 13, parent.Height / 2 - 5);

        parent.Controls.Add(this);
        BringToFront();

        // ---- Rule #1: only Right ports can start a drag ----
        if (Side == PortSide.Right)
        {
            MouseDown += (_, e) =>
            {
                if (e.Button == MouseButtons.Left &&
                    FindForm() is Form1 f && f.MainCanvas != null)
                {
                    f.MainCanvas.StartRubberBand(this);
                }
            };
        }
    }

    public Point CenterOnCanvas()
    {
        var center = new Point(Left + Width / 2, Top + Height / 2);
        var screen = Parent.PointToScreen(center);
        return ((Form1)FindForm()).MainCanvas.PointToClient(screen);
    }
}
