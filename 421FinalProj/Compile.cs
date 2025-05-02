using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class Compile : StateIF
    {
        private CanvasManager c = CanvasManager.getInstance();
        public void addTask(Task task)
        {
            c.setState(new Modify());
            c.getState().addTask(task);
        }

        public void execute()
        {
            c.setState(new Execute());
            c.getState().execute();
        }

        public void removeTask(Task task)
        {
            c.setState(new Modify());
            c.getState().removeTask(task);
        }

        public void Build()
        {
            UICanvas ui = UICanvas.getCanvas();
            
            foreach (Panel p in ui.Controls)
            {
                if (p.Controls.Count == 6)
                {
                    EmailBuilderIF builder = new EmailBuilderIF();
                    builder.To(p.Controls[1].Text);
                    builder.Subject(p.Controls[3].Text);
                    builder.Body(p.Controls[5].Text);

                    c.addTask(builder.Build());
                }

                else
                {
                    SMSBuilderIF builder = new SMSBuilderIF();

                    builder.To(p.Controls[1].Text);
                    builder.Body(p.Controls[3].Text);

                    c.addTask(builder.Build());

                }
            }

        }
    }
}
