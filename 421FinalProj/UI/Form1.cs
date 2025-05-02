using _421FinalProj.UI;
using _421FinalProj;

namespace _421FinalProj
{
    public partial class Form1 : Form
    {
        public UICanvas MainCanvas { get; }

        public Form1()
        {
            InitializeComponent();
            MainCanvas = UICanvas.getCanvas();
            Controls.Add(MainCanvas);
            this.Load += Form1_Load;
            MainCanvas.AllowDrop = true;

        }

        private void Form1_Load(object? sender, EventArgs e)
        {

            var emailBox = new TaskCard("Email", Color.LightBlue);
            emailBox.setForm(this);
            emailBox.setCanvas(MainCanvas);

            flowTasks.Controls.Add(emailBox.taskBox);

            var smsBox = new TaskCard("SMS", Color.LightGreen);
            smsBox.setForm(this);
            smsBox.setCanvas(MainCanvas);

            flowTasks.Controls.Add(smsBox.taskBox);

        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            CanvasManager c = CanvasManager.getInstance();
            c.setState(new Compile());
            c.getState().Build();
        }
    }
}
