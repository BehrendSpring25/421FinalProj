using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

namespace _421FinalProj.UI
{
    public class TaskCard : Label
    {
        public Label taskBox;
        private PictureBox? ghostControl;
        public Panel Canvas = new Panel();
        public Form Form = new Form();
        public string TaskType = "";

        public TaskCard(string taskType, Color color)
        {
            TaskType = taskType;
            taskBox = new Label
            {
                Text = taskType,
                BackColor = color,
                AutoSize = true,
                Padding = new Padding(10),
                Margin = new Padding(10),
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
                if (sender is Control box && box.Text == TaskType)
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
            // convert *screen* → *form* once
            Point formPt = Form.PointToClient(new Point(e.X, e.Y));

            if (Canvas.ClientRectangle.Contains(Canvas.PointToClient(new Point(e.X, e.Y))))
            {
                // you can add canvas‑specific logic here
            }

            // always move the ghost in *form* space
            if (ghostControl != null)
                ghostControl.Location =
                    new Point(formPt.X - ghostControl.Width / 2,
                              formPt.Y - ghostControl.Height / 2);
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
                if (droppedData != TaskType)
                {
                    return;
                }

                var panel = new Panel
                {
                    Name = $"Dropped/{droppedData}",
                    Location = dropLocation,
                    AutoSize = true,
                    AutoSizeMode = AutoSizeMode.GrowAndShrink,
                    BorderStyle = BorderStyle.FixedSingle,
                    BackColor = Color.White,
                };

                if (droppedData == "Email")
                {
                    panel = emailDrop(panel);
                }
                else if (droppedData == "SMS")
                {
                    panel = smsDrop(panel);
                }
                else if (droppedData == "Start")
                {
                    panel = startDrop(panel);
                }
                else if (droppedData == "End")
                {
                    panel = endDrop(panel);
                }

                panel.PerformLayout();
                panel.Size = panel.PreferredSize;

                panel.Location = new Point(
                    dropLocation.X - panel.Width / 2,
                    dropLocation.Y - panel.Height / 2);

                Canvas.Controls.Add(panel);
            }
        }

        private Panel emailDrop(Panel p)
        {
            p.BackColor = Color.LightBlue;

            p.Controls.Add(CreateLabel(p, "To:", new Point(5, 5)));
            p.Controls.Add(CreateTextBox(p, new Point(50, 5)));

            p.Controls.Add(CreateLabel(p, "Subject:", new Point(5, 35)));
            p.Controls.Add(CreateTextBox(p, new Point(50, 35)));

            p.Controls.Add(CreateLabel(p, "Body:", new Point(5, 65)));
            p.Controls.Add(CreateTextBox(p, new Point(50, 65), true, new Size(200, 100)));

            p.PerformLayout();
            p.Size = p.PreferredSize;
            PortPanel portPanelL = new PortPanel("Left", p);
            PortPanel portPanelR = new PortPanel("Right", p);

            return p;
        }

        private Panel smsDrop(Panel p)
        {
            p.BackColor = Color.LightGreen;

            // — row 1 — “To”
            p.Controls.Add(CreateLabel(p, "To:", new Point(5, 5)));
            p.Controls.Add(CreateTextBox(p, new Point(50, 5)));

            // — row 2 — “Message”
            p.Controls.Add(CreateLabel(p, "Message:", new Point(5, 35)));
            p.Controls.Add(CreateTextBox(p, new Point(50, 35), true, new Size(200, 100)));

            // — row 3 — “Carrier”
            p.Controls.Add(CreateLabel(p, "Carrier:", new Point(5, 145)));
            p.Controls.Add(CreateCarrierDropDown(p, new Point(50, 142)));

            // ports + sizing
            p.PerformLayout();
            p.Size = p.PreferredSize;
            _ = new PortPanel("Left", p);
            _ = new PortPanel("Right", p);

            return p;
        }


        private Panel startDrop(Panel p)
        {
            p.BackColor = Color.Green;

            p.Width = 30;
            p.Height = 30;
            PortPanel portPanelR = new PortPanel("Right", p);

            return p;
        }

        private Panel endDrop(Panel p)
        {
            p.BackColor = Color.Red;

            p.Width = 30;
            p.Height = 30;
            PortPanel portPanelL = new PortPanel("Left", p);

            return p;
        }


        private Label CreateLabel(Panel p, string txt, Point location)
        {
            return new Label
            {
                Parent = p,
                Text = txt,
                Location = location,
                AutoSize = true,
                Margin = new Padding(5),
            };
        }

        private TextBox CreateTextBox(Panel p, Point location, bool multiline = false, Size? size = null)
        {

            var textBox = new TextBox
            {
                Parent = p,
                Location = location,
                Multiline = multiline,
                Margin = new Padding(10),
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

        private ComboBox CreateCarrierDropDown(Panel parent, Point location)
        {
            var cb = new ComboBox
            {
                Parent = parent,
                Location = location,
                Width = 150,
                DropDownStyle = ComboBoxStyle.DropDownList
            };

            cb.Items.AddRange(new object[]
            {
                "AT&T",
                "Verizon",
                "T‑Mobile",
                "Sprint (legacy)",
                "US Cellular",
                "Google Fi"
            });

            cb.SelectedIndex = 0;        // default

            return cb;
        }


    }

}
