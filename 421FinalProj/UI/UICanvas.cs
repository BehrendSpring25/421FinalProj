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

        // add at top of UICanvas
        private const int SNAP_MARGIN = 20;


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
            Point screenPt = PointToScreen(canvasPt);

            foreach (Control card in Controls)
            {
                foreach (Control child in card.Controls)
                {
                    if (child is PortPanel port)
                    {
                        // convert to card space
                        Point ptInCard = card.PointToClient(screenPt);

                        // inflate the port‑bounds to create an easy hit box
                        Rectangle hit = Rectangle.Inflate(port.Bounds,
                                                           SNAP_MARGIN,
                                                           SNAP_MARGIN);

                        if (hit.Contains(ptInCard))
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
            pen.EndCap = LineCap.Flat;     // shaft ends flat; we draw our own heads
            pen.StartCap = LineCap.Flat;

            foreach (var (from, to) in _links)
                DrawArrowLine(e.Graphics, pen,
                              from.CenterOnCanvas(), to.CenterOnCanvas(),   // endpoints
                              step: 40,   // ⇐ arrow every 40 px
                              head: 8,    // ⇐ arrow length (px)
                              half: 4);   // ⇐ half‑width of base (px)

            // live rubber band (single dashed shaft + last arrow only)
            if (_rubberStart != null && _rubberEnd.HasValue)
            {
                pen.DashStyle = DashStyle.Dash;
                DrawArrowLine(e.Graphics, pen,
                              _rubberStart.CenterOnCanvas(),
                              _rubberEnd.Value,
                              step: 9999);          // huge step → only final head
            }

        }
        // draw a solid arrow every <step> pixels along the vector  (helper)
        private static void DrawArrowLine(Graphics g, Pen pen,
                                          PointF from, PointF to,
                                          float step = 40,   // distance between arrows
                                          float head = 6,   // arrow length
                                          float half = 3)   // arrow half‑width
        {
            // 1. draw the main shaft once
            g.DrawLine(pen, from, to);

            // 2. unit direction vector
            var vx = to.X - from.X;
            var vy = to.Y - from.Y;
            var len = MathF.Sqrt(vx * vx + vy * vy);
            if (len < 0.01f) return;

            var ux = vx / len;
            var uy = vy / len;

            // perpendicular unit (for the two arrow sides)
            var px = -uy;
            var py = ux;

            // 3. walk along the line and paint chevrons
            for (float d = step; d < len - head; d += step)
            {
                // anchor point for this arrow
                float ax = from.X + ux * d;
                float ay = from.Y + uy * d;

                // tip of arrow (a bit further along)
                float tx = ax + ux * head;
                float ty = ay + uy * head;

                // two base corners
                float bx = ax + px * half;
                float by = ay + py * half;

                float cx = ax - px * half;
                float cy = ay - py * half;

                g.FillPolygon(pen.Brush!, new[]
                {
            new PointF(tx, ty), new PointF(bx, by), new PointF(cx, cy)
        });
            }

            // 4. final arrow head at the end (optional – comment if you
            //    want only mid‑chevrons)
            {
                float ax = to.X - ux * head;
                float ay = to.Y - uy * head;
                float bx = ax + px * half;
                float by = ay + py * half;
                float cx = ax - px * half;
                float cy = ay - py * half;
                g.FillPolygon(pen.Brush!, new[]
                {
            to, new PointF(bx, by), new PointF(cx, cy)
        });
            }
        }

    }
}
