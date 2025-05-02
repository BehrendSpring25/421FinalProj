namespace _421FinalProj
{
    public partial class Form1 : Form
    {
        
        public Form1()
        {
            InitializeComponent();
            this.Load += Form1_Load;
            Canvas.AllowDrop = true;

            //// Handle DragEnter to validate the dragged object
            //Canvas.DragEnter += (s, e) =>
            //{
            //    if (e.Data?.GetDataPresent(typeof(TaskCard)) == true)
            //    {
            //        e.Effect = DragDropEffects.Move;
            //    }
            //    else
            //    {
            //        e.Effect = DragDropEffects.None;
            //    }
            //};

            //// Handle DragDrop to place the TaskCard on the canvas
            //Canvas.DragDrop += (s, e) =>
            //{
            //    if (e.Data?.GetData(typeof(TaskCard)) is TaskCard taskCard)
            //    {
            //        // Convert the drop location to canvas coordinates
            //        var dropLocation = Canvas.PointToClient(new Point(e.X, e.Y));
            //        taskCard.Location = dropLocation;

            //        // Add the TaskCard to the canvas if it's not already there
            //        if (!Canvas.Controls.Contains(taskCard))
            //        {
            //            Canvas.Controls.Add(taskCard);
            //        }
            //    }
            //};
        }
        private void Form1_Load(object? sender, EventArgs e)
        {

            var emailBox = new TaskCard("Email", Color.LightBlue);
            emailBox.setForm(this);
            emailBox.setCanvas(Canvas);
            
            flowTasks.Controls.Add(emailBox.taskBox);

            var smsBox = new TaskCard("SMS", Color.LightGreen);
            smsBox.setForm(this);
            smsBox.setCanvas(Canvas);
            
            flowTasks.Controls.Add(smsBox.taskBox);

            //var smsBox = new Label
            //{
            //    Text = "SMS",
            //    BackColor = Color.LightGreen,
            //    AutoSize = true,
            //    Padding = new Padding(10),
            //    BorderStyle = BorderStyle.FixedSingle
            //};
            //smsBox.MouseMove += SmsBox_MouseMove;
            //flowTasks.Controls.Add(smsBox);

            //Canvas.DragEnter += BoxdragEnter!;
            //Canvas.DragDrop += BoxDrop!;
        }


    }
}
