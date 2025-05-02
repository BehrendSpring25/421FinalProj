using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    public class TaskCard : Label
    {
        public Label taskBox;
        private PictureBox? ghostControl;
        public Panel Canvas = new Panel();
        public Form Form = new Form();

        public TaskCard(string taskType, Color color)
        {

            taskBox = new Label
            {
                Text = taskType,
                BackColor = color,
                AutoSize = true,
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };
            taskBox.MouseMove += TaskBox_MouseMove;
        }

        public void setCanvas(Panel canvas)
        {
            Canvas = canvas;
            Canvas.AllowDrop = true;
            Canvas.DragEnter += BoxdragEnter;
            Canvas.DragDrop += BoxDrop;
            Canvas.DragOver += Form_DragOver;
        }

        public void setForm(Form form)
        {
            Form = form;
            Form.AllowDrop = true;
            Form.DragOver += Form_DragOver;
        }

        private void TaskBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender is Control box && (box.Text == "Email" || box.Text == "SMS"))
                {
                    GhostControl(box);

                    box.DoDragDrop(box.Text, DragDropEffects.Copy | DragDropEffects.Move);

                    RemoveGhostControl();
                }

            }
        }

        private void GhostControl(Control control)
        {
            Bitmap b = new Bitmap(control.Width, control.Height);
            control.DrawToBitmap(b, new Rectangle(0, 0, control.Width, control.Height));

            ghostControl = new PictureBox
            {
                Image = b,
                Size = control.Size,
                BackColor = Color.Transparent,
                Location = control.Location,
                Parent = Form,
                Enabled = false
            };

            Form.Controls.Add(ghostControl);
            ghostControl.BringToFront();
        }

        private void BoxdragEnter(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(DataFormats.StringFormat))
            if (e.Data?.GetDataPresent(DataFormats.StringFormat) == true)
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void Form_DragOver(object sender, DragEventArgs e)
        {
            var mousePosition = Form.PointToClient(new Point(e.X, e.Y));
            if (Canvas.ClientRectangle.Contains(Canvas.PointToClient(mousePosition)))
            {
                return;
            }

            if (ghostControl != null)
            {
                ghostControl.Location = new Point(mousePosition.X - ghostControl.Width / 2, mousePosition.Y - ghostControl.Height / 2);
            }
        }

        private void RemoveGhostControl()
        {
            if (ghostControl != null)
            {
                Form.Controls.Remove(ghostControl);
                ghostControl.Dispose();
                ghostControl = null;
            }
        }

        private void BoxDrop(object sender, DragEventArgs e)
        {
            //if (e.Data.GetDataPresent(DataFormats.StringFormat))
            //{
            //    string droppedData = (string)e.Data.GetData(DataFormats.StringFormat);
            //}

            if (sender == Canvas && e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string droppedData = (string)e.Data.GetData(DataFormats.StringFormat);
                var dropLocation = Canvas.PointToClient(new Point(e.X, e.Y));

                var panel = new Panel
                {
                    Location = dropLocation,
                    AutoSize = true,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White
                };

                if (droppedData == "Email")
                {
                    panel = emailDrop(panel);
                }
                else if (droppedData == "SMS")
                {
                    panel = smsDrop(panel);
                }

                Canvas.Controls.Add(panel);
            }
        }

        private Panel emailDrop(Panel p)
        {
            p.BackColor = Color.LightBlue;

            p.Controls.Add(CreateLabel("To:", new Point(5, 5)));
            p.Controls.Add(CreateTextBox(new Point(50, 5)));

            p.Controls.Add(CreateLabel("Subject:", new Point(5, 35)));
            p.Controls.Add(CreateTextBox(new Point(50, 35)));

            p.Controls.Add(CreateLabel("Body:", new Point(5, 65)));
            p.Controls.Add(CreateTextBox(new Point(50, 65), true, new Size(200, 100)));

            return p;
        }

        private Panel smsDrop(Panel p)
        {
            p.BackColor = Color.LightGreen;

            p.Controls.Add(CreateLabel("To:", new Point(5, 5)));
            p.Controls.Add(CreateTextBox(new Point(50, 5)));

            p.Controls.Add(CreateLabel("Message:", new Point(5, 35)));
            p.Controls.Add(CreateTextBox(new Point(50, 35), true, new Size(200, 100)));

            Panel LeftPort = new Panel
            {
                Size = new Size(10, 10),
                BackColor = Color.Blue,
                Location = new Point(0, p.Height / 2 - 5),
                Cursor = Cursors.Hand
            };
            LeftPort.BringToFront();
            p.Controls.Add(LeftPort);

            Panel RightPort = new Panel
            {
                Size = new Size(10, 10),
                BackColor = Color.Red,
                Location = new Point(p.Width - 10, p.Height / 2 - 5),
                Cursor = Cursors.Hand
            };
            RightPort.BringToFront();
            p.Controls.Add(RightPort);

            return p;
        }

        private Label CreateLabel(string txt, Point location)
        {
            return new Label
            {
                Text = txt,
                Location = location,
                AutoSize = true,
            };
        }

        private TextBox CreateTextBox(Point location, bool multiline = false, Size? size = null)
        {
            var textBox = new TextBox
            {
                Location = location,
                Multiline = multiline
            };

            if (size.HasValue)
            {
                textBox.Size = size.Value;
            }
            else
            {
                textBox.Width = 200;
            }

            return textBox;
        }
    }

}
