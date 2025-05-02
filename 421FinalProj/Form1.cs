namespace _421FinalProj
{
    public partial class Form1 : Form
    {
        private PictureBox? ghostControl;
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            Canvas.AllowDrop = true;

            // Handle DragEnter to validate the dragged object
            Canvas.DragEnter += (s, e) =>
            {
                if (e.Data?.GetDataPresent(typeof(TaskCard)) == true)
                {
                    e.Effect = DragDropEffects.Move;
                }
                else
                {
                    e.Effect = DragDropEffects.None;
                }
            };

            // Handle DragDrop to place the TaskCard on the canvas
            Canvas.DragDrop += (s, e) =>
            {
                if (e.Data?.GetData(typeof(TaskCard)) is TaskCard taskCard)
                {
                    // Convert the drop location to canvas coordinates
                    var dropLocation = Canvas.PointToClient(new Point(e.X, e.Y));
                    taskCard.Location = dropLocation;

                    // Add the TaskCard to the canvas if it's not already there
                    if (!Canvas.Controls.Contains(taskCard))
                    {
                        Canvas.Controls.Add(taskCard);
                    }
                }
            };
        }
        private void Form1_Load(object? sender, EventArgs e)
        {
            // put startup logic here
            //flowTasks.Controls.Add(new PaletteItem("Email", "EmailTask"));
            //flowTasks.Controls.Add(new PaletteItem("SMS", "SmsTask"));

            this.AllowDrop = true;
            this.DragOver += Form_DragOver;


            Canvas.AllowDrop = true;
            Canvas.DragEnter += BoxdragEnter;
            Canvas.DragDrop += BoxDrop;
            Canvas.DragOver += Form_DragOver;

            var emailBox = new Label
            {
                Text = "Email",
                BackColor = Color.LightBlue,
                AutoSize = true,
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };
            emailBox.MouseMove += EmailBox_MouseMove;
            flowTasks.Controls.Add(emailBox);

            var smsBox = new Label
            {
                Text = "SMS",
                BackColor = Color.LightGreen,
                AutoSize = true,
                Padding = new Padding(10),
                BorderStyle = BorderStyle.FixedSingle
            };
            smsBox.MouseMove += SmsBox_MouseMove;
            flowTasks.Controls.Add(smsBox);

            Canvas.DragEnter += BoxdragEnter!;
            Canvas.DragDrop += BoxDrop!;
        }

        private void EmailBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender is Control emailBox)
                {
                    GhostControl(emailBox);

                    emailBox.DoDragDrop(emailBox.Text, DragDropEffects.Copy | DragDropEffects.Move);

                    RemoveGhostControl();
                }
            }
        }

        private void SmsBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender is Control smsBox)
                {
                    GhostControl(smsBox);

                    smsBox.DoDragDrop(smsBox.Text, DragDropEffects.Copy | DragDropEffects.Move);

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
                Parent = this,
                Enabled = false
            };

            this.Controls.Add(ghostControl);
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
            var mousePosition = this.PointToClient(new Point(e.X, e.Y));
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
                this.Controls.Remove(ghostControl);
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
