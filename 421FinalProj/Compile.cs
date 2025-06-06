﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _421FinalProj
{
    internal class Compile : StateIF
    {
        private CanvasManager c = CanvasManager.getInstance();
        private Form1? _ui => Application.OpenForms.OfType<Form1>().FirstOrDefault();
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
            if (_ui == null) return;           // safety guard

            _ui.ShowOutputTab();
            _ui.Log("=== Build started ===");

            UICanvas ui = UICanvas.getCanvas();
            var links = ui.GetConnections();

            // 1. locate Start node
            Panel? startPanel = ui.Controls
                .OfType<Panel>()
                .FirstOrDefault(p => p.Name.Contains("/Start", StringComparison.OrdinalIgnoreCase));

            if (startPanel == null)
            {
                _ui.Log("ERROR: No Start node found – aborting compile.");
                MessageBox.Show("No Start node found – cannot compile");
                return;
            }
            _ui.Log($"Found Start node   =>  {startPanel.Name}");

            HashSet<Panel> visited = new();
            Panel? current = startPanel;

            while (current != null && visited.Add(current))
            {
                // 2. Right port
                PortPanel? rightPort = current.Controls
                    .OfType<PortPanel>()
                    .FirstOrDefault(pp => pp.Side == PortSide.Right);

                if (rightPort == null)
                {
                    _ui.Log($"ERROR: {current.Name} has no Right port.");
                    break;
                }

                // 3. outgoing link
                var nextLink = links.FirstOrDefault(l => l.From == rightPort);
                if (nextLink == null)
                {
                    _ui.Log($"ERROR: No connection leaving {current.Name}.");
                    break;
                }

                Panel targetPanel = (Panel)nextLink.To.Parent!;
                _ui.Log($"{current.Name}  -->  {targetPanel.Name}");

                // 4. Stop at End
                if (targetPanel.Name.Contains("/End", StringComparison.OrdinalIgnoreCase))
                {
                    _ui.Log("Reached End node – build traversal complete.");
                    break;
                }

                // 5. build task
                if (targetPanel.Name.Contains("/Email", StringComparison.OrdinalIgnoreCase))
                {
                    var eb = new EmailBuilderIF();
                    eb.To(targetPanel.Controls[3].Text);
                    eb.Subject(targetPanel.Controls[5].Text);
                    eb.Body(targetPanel.Controls[7].Text);

                    c.addTask(eb.Build());
                    _ui.Log($"   Added Email task → {targetPanel.Controls[1].Text}");
                }
                else if (targetPanel.Name.Contains("/SMS", StringComparison.OrdinalIgnoreCase))
                {
                    // Controls index map for SMS node
                    // 0: Label "To"
                    // 1: TextBox (To number)
                    // 2: Label "Message"
                    // 3: TextBox (Body)
                    // 4: Label "Carrier"
                    // 5: ComboBox (Carrier selector)

                    string toNumber = targetPanel.Controls[3].Text;
                    string body = targetPanel.Controls[5].Text;
                    string carrierUI = ((ComboBox)targetPanel.Controls[7]).SelectedItem!.ToString();

                    // ----- carrier → gateway lookup -----
                    string gateway = carrierUI switch
                    {
                        "AT&T" => "txt.att.net",
                        "Verizon" => "vtext.com",
                        "T‑Mobile" => "tmomail.net",
                        "Sprint (legacy)" => "messaging.sprintpcs.com",
                        "US Cellular" => "email.uscc.net",
                        "Google Fi" => "msg.fi.google.com",
                        _ => ""                        // fallback (shouldn’t happen)
                    };

                    var sb = new SMSBuilderIF();
                    sb.To(toNumber);
                    sb.Body(body);
                    sb.AddCarrierGateway(gateway);          // builder stores or combines

                    c.addTask(sb.Build());
                    _ui.Log($"   Added SMS task → {toNumber} via {carrierUI} ({gateway})");
                }

                else
                {
                    _ui.Log($"WARNING: Unrecognized node type {targetPanel.Name} – skipped.");
                }

                // 6. advance
                current = targetPanel;
            }

            _ui.Log("=== Build finished ===");
        }


    }
}
