using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj.UI
{
    public class PaletteItem : UserControl
    {
        public string TaskType { get; }
        public PaletteItem(string text, string type)
        {
            TaskType = type;
            Size = new(180, 40);
            BackColor = Color.Gainsboro;
            Cursor = Cursors.Hand;
            Controls.Add(new Label
            {
                Text = text,
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleCenter
            });

            MouseDown += (_, e) =>
            {
                if (e.Button == MouseButtons.Left)
                    DoDragDrop(TaskType, DragDropEffects.Copy);
            };
        }
    }

}
