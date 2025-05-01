using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    public class TaskCard : UserControl
    {
        private Point _grab;
        private bool _dragging;

        public TaskCard(string taskType)
        {
            Size = new(320, 240);
            BorderStyle = BorderStyle.FixedSingle;
            BackColor = Color.White;

            Controls.Add(new Label
            {
                Text = $"Send {taskType}",
                Dock = DockStyle.Top,
                Height = 28,
                BackColor = Color.Silver,
                TextAlign = ContentAlignment.MiddleCenter
            });

            //MouseDown += (s, e) =>
            //{
            //    _dragging = true;
            //    _grab = e.Location;
            //};
            //MouseMove += (s, e) =>
            //{
            //    if (!_dragging || e.Button != MouseButtons.Left) return;
            //    Left += e.X - _grab.X;
            //    Top += e.Y - _grab.Y;
            //};
            //MouseUp += (_, _) => _dragging = false;
            MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    DoDragDrop(this, DragDropEffects.Move);
                }
            };
        }
    }

}
