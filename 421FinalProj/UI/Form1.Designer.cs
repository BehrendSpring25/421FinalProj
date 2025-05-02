namespace _421FinalProj
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            panelTop = new Panel();
            btnBuild = new Button();
            btnRun = new Button();
            panelToolBox = new Panel();
            tabControl = new TabControl();
            tpTool = new TabPage();
            flowTasks = new FlowLayoutPanel();
            tpOut = new TabPage();
            splitter1 = new Splitter();
            panelTop.SuspendLayout();
            panelToolBox.SuspendLayout();
            tabControl.SuspendLayout();
            tpTool.SuspendLayout();
            SuspendLayout();
            // 
            // panelTop
            // 
            panelTop.BackColor = Color.MistyRose;
            panelTop.Controls.Add(btnBuild);
            panelTop.Controls.Add(btnRun);
            panelTop.Dock = DockStyle.Top;
            panelTop.Location = new Point(0, 0);
            panelTop.Margin = new Padding(4, 5, 4, 5);
            panelTop.Name = "panelTop";
            panelTop.Size = new Size(1143, 117);
            panelTop.TabIndex = 0;
            // 
            // btnBuild
            // 
            btnBuild.BackColor = Color.LemonChiffon;
            btnBuild.Location = new Point(160, 20);
            btnBuild.Margin = new Padding(4, 5, 4, 5);
            btnBuild.Name = "btnBuild";
            btnBuild.Size = new Size(134, 78);
            btnBuild.TabIndex = 1;
            btnBuild.Text = "Build";
            btnBuild.UseVisualStyleBackColor = false;
            btnBuild.Click += btnBuild_Click;
            // 
            // btnRun
            // 
            btnRun.BackColor = Color.Honeydew;
            btnRun.Location = new Point(17, 20);
            btnRun.Margin = new Padding(4, 5, 4, 5);
            btnRun.Name = "btnRun";
            btnRun.Size = new Size(134, 78);
            btnRun.TabIndex = 0;
            btnRun.Text = "Run";
            btnRun.UseVisualStyleBackColor = false;
            // 
            // panelToolBox
            // 
            panelToolBox.BackColor = Color.MistyRose;
            panelToolBox.Controls.Add(tabControl);
            panelToolBox.Dock = DockStyle.Left;
            panelToolBox.Location = new Point(0, 117);
            panelToolBox.Margin = new Padding(4, 5, 4, 5);
            panelToolBox.Name = "panelToolBox";
            panelToolBox.Padding = new Padding(14, 17, 14, 17);
            panelToolBox.Size = new Size(316, 633);
            panelToolBox.TabIndex = 1;
            // 
            // tabControl
            // 
            tabControl.Controls.Add(tpTool);
            tabControl.Controls.Add(tpOut);
            tabControl.Dock = DockStyle.Fill;
            tabControl.Location = new Point(14, 17);
            tabControl.Margin = new Padding(11, 13, 11, 13);
            tabControl.Name = "tabControl";
            tabControl.Padding = new Point(20, 3);
            tabControl.SelectedIndex = 0;
            tabControl.Size = new Size(288, 599);
            tabControl.TabIndex = 0;
            // 
            // tpTool
            // 
            tpTool.Controls.Add(flowTasks);
            tpTool.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            tpTool.Location = new Point(4, 34);
            tpTool.Margin = new Padding(4, 5, 4, 5);
            tpTool.Name = "tpTool";
            tpTool.Padding = new Padding(4, 5, 4, 5);
            tpTool.Size = new Size(280, 561);
            tpTool.TabIndex = 0;
            tpTool.Text = "Toolbox";
            tpTool.UseVisualStyleBackColor = true;
            // 
            // flowTasks
            // 
            flowTasks.Dock = DockStyle.Fill;
            flowTasks.Location = new Point(4, 5);
            flowTasks.Margin = new Padding(4, 5, 4, 5);
            flowTasks.Name = "flowTasks";
            flowTasks.Size = new Size(272, 551);
            flowTasks.TabIndex = 0;
            // 
            // tpOut
            // 
            tpOut.Location = new Point(4, 34);
            tpOut.Margin = new Padding(4, 5, 4, 5);
            tpOut.Name = "tpOut";
            tpOut.Padding = new Padding(4, 5, 4, 5);
            tpOut.Size = new Size(279, 562);
            tpOut.TabIndex = 1;
            tpOut.Text = "Output";
            tpOut.UseVisualStyleBackColor = true;
            // 
            // splitter1
            // 
            splitter1.Location = new Point(316, 117);
            splitter1.Margin = new Padding(4, 5, 4, 5);
            splitter1.Name = "splitter1";
            splitter1.Size = new Size(11, 633);
            splitter1.TabIndex = 2;
            splitter1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(10F, 25F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1143, 750);
            Controls.Add(splitter1);
            Controls.Add(panelToolBox);
            Controls.Add(panelTop);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Margin = new Padding(4, 5, 4, 5);
            Name = "Form1";
            Text = "B0n3r";
            panelTop.ResumeLayout(false);
            panelToolBox.ResumeLayout(false);
            tabControl.ResumeLayout(false);
            tpTool.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private Panel panelTop;
        private Button btnBuild;
        private Button btnRun;
        private Panel panelToolBox;
        private TabControl tabControl;
        private TabPage tpTool;
        private TabPage tpOut;
        private FlowLayoutPanel flowTasks;
        private Splitter splitter1;
        private Panel Canvas;
    }
}

