namespace _421FinalProj
{
    public partial class Form1 : Form
    {
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

            Canvas.DragEnter += BoxdragEnter;
            Canvas.DragDrop += BoxDrop;
        }

        private void EmailBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender is Control emailBox)
                {
                    // Fix for CS1503: Use a string key instead of typeof(TaskCard)
                    emailBox.DoDragDrop(emailBox.Text, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
        }

        private void SmsBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender is Control smsBox)
                {
                    smsBox.DoDragDrop(smsBox.Text, DragDropEffects.Copy | DragDropEffects.Move);
                }
            }
        }

        private void BoxdragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                e.Effect = DragDropEffects.Copy;
            }
            else
            {
                e.Effect = DragDropEffects.None;
            }

        }

        private void BoxDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.StringFormat))
            {
                string droppedData = (string)e.Data.GetData(DataFormats.StringFormat);
            }
        }
    }
}
