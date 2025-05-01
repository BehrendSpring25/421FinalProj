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
                if (e.Data.GetDataPresent(typeof(TaskCard)))
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
                if (e.Data.GetData(typeof(TaskCard)) is TaskCard taskCard)
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
            flowTasks.Controls.Add(new PaletteItem("Email", "EmailTask"));
            flowTasks.Controls.Add(new PaletteItem("SMS", "SmsTask"));
        }
        //void Canvas_DragEnter(object? s, DragEventArgs e)
        //{
        //    if (e.Data!.GetDataPresent(DataFormats.Text))
        //        e.Effect = DragDropEffects.Copy;
        //}

        //void Canvas_DragDrop(object? s, DragEventArgs e)
        //{
        //    string? type = e.Data!.GetData(DataFormats.Text) as string;
        //    if (type == null) return;

        //    var pt = Canvas.PointToClient(new Point(e.X, e.Y));
        //    var card = new TaskCard(type) { Location = pt };
        //    Canvas.Controls.Add(card);
        //}
    }
}
