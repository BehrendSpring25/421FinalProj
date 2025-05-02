// Canvas.cs
using _421FinalProj.UI;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace _421FinalProj
{
    public class UICanvas : Panel
    {
        public record Connection(PortPanel From, PortPanel To);

        private readonly List<Connection> _links = new();
        private Point? _rubberEnd;           // live drag end‑point
        private PortPanel? _rubberStart;


        public event EventHandler? ConnectionChanged;
        private void RaiseConnectionChanged()
            => ConnectionChanged?.Invoke(this, EventArgs.Empty);

        public UICanvas()
        {
            DoubleBuffered = true;
            BackColor = Color.Bisque;
            Dock = DockStyle.Fill;
            Location = new Point(229, 70);
            Name = "Canvas";
            Size = new Size(571, 380);
            TabIndex = 3;
        }

        public void StartRubberBand(PortPanel fromPort)
        {
            _rubberStart = fromPort;
            _rubberEnd = fromPort.CenterOnCanvas();
            Capture = true;
            MouseMove += PortConnect_MouseMove;
            MouseUp += PortConnect_MouseUp;
        }

        private void PortConnect_MouseMove(object? s, MouseEventArgs e)
        {
            if (_rubberStart != null)
            {
                _rubberEnd = e.Location;
                Invalidate();                 // triggers repaint
            }
        }

        private void PortConnect_MouseUp(object? s, MouseEventArgs e)
        {
            if (_rubberStart == null) return;

            var target = GetPortAt(e.Location);
            if (target != null && target != _rubberStart)
            {
                _links.Add(new Connection(_rubberStart, target));
                RaiseConnectionChanged();
            }

            _rubberStart = null;
            _rubberEnd = null;
            Capture = false;
            MouseMove -= PortConnect_MouseMove;
            MouseUp -= PortConnect_MouseUp;
            Invalidate();
        }

        private PortPanel? GetPortAt(Point canvasPt)
        {
            // convert canvas‑client → screen once
            Point screenPt = PointToScreen(canvasPt);

            foreach (Control card in Controls)
            {
                // task cards are Panel (or any Control)
                foreach (Control child in card.Controls)
                {
                    if (child is PortPanel port)
                    {
                        // back-screen → card‑client
                        Point ptInCard = card.PointToClient(screenPt);

                        if (port.Bounds.Contains(ptInCard))
                            return port;
                    }
                }
            }
            return null;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            //Overlay?.DoPaint(e);
            e.Graphics.SmoothingMode =
                System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            using var pen = new Pen(Color.DimGray, 2);

            // draw permanent links
            foreach (var (from, to) in _links)
                e.Graphics.DrawLine(pen,
                                    from.CenterOnCanvas(),
                                    to.CenterOnCanvas());

            // draw rubber band if any
            if (_rubberStart != null && _rubberEnd.HasValue)
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                e.Graphics.DrawLine(pen,
                                    _rubberStart.CenterOnCanvas(),
                                    _rubberEnd.Value);
            }
        }
    }
}
