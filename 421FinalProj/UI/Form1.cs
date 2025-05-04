using _421FinalProj.UI;
using _421FinalProj;
using System.Diagnostics;

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


            var startBox = new TaskCard("Start", Color.Green);
            startBox.setForm(this);
            startBox.setCanvas(MainCanvas);

            var endBox = new TaskCard("End", Color.Red);
            endBox.setForm(this);
            endBox.setCanvas(MainCanvas);

            flowTasks.Controls.Add(emailBox.taskBox);
            flowTasks.Controls.Add(smsBox.taskBox);
            flowTasks.Controls.Add(startBox.taskBox);
            flowTasks.Controls.Add(endBox.taskBox);

        }

        private void btnBuild_Click(object sender, EventArgs e)
        {
            CanvasManager c = CanvasManager.getInstance();
            c.setState(new Compile());
            c.getState().Build();
            Debug.WriteLine("");
        }

        public void ShowOutputTab()
        => tabControl.SelectedTab = tpOut;

        public void Log(string message)
        {
            if (rtbLog.InvokeRequired)
            {
                rtbLog.Invoke(new Action<string>(Log), message);
                return;
            }
            rtbLog.AppendText($"{DateTime.Now:HH:mm:ss}  {message}{Environment.NewLine}");
            rtbLog.ScrollToCaret();
        }

        private void btnRun_Click(object sender, EventArgs e)
        {
            CanvasManager c = CanvasManager.getInstance();
            c.setState(new Execute());
            c.getState().execute();
        }
    }
}
