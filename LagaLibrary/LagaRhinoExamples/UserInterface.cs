using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eto.Forms;
using Eto.Drawing;
using System.Net.Security;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;

namespace LagaRhinoExamples
{
    /// <summary>
    /// user interface
    /// </summary>
    public class UserInterface : Form
    {
        private readonly RhinoDoc rhinoDocument;
        public UserInterface(RhinoDoc doc, RunMode mode)
        {
            rhinoDocument = doc;
            Title = "LagaRhino examples";
            ClientSize = new Size(300, 500);
            BackgroundColor = Colors.White;
            
            Button cirBtn = new Button();
            cirBtn.Size = new Size(200, 25);
            cirBtn.BackgroundColor = Colors.LightGrey;
            cirBtn.Click += cirBtn_Click;
            cirBtn.Text = "Find center of circle";

            Button tspBtn = new Button();
            tspBtn.Size = new Size(200, 25);
            tspBtn.BackgroundColor = Colors.LightGrey;
            tspBtn.Click += tspBtn_Click;
            tspBtn.Text = "Travelling sales problem";

            StackLayout sl = new StackLayout();
            sl.AlignLabels = true;
            sl.Padding = 10;
            sl.Orientation = Orientation.Vertical;
            sl.Spacing = 5;
            sl.Items.Add(cirBtn);
            sl.Items.Add(tspBtn);
            this.Content = sl;
        }

        private void tspBtn_Click(object sender, EventArgs e)
        {
            TravellingSales tsp = new TravellingSales(rhinoDocument);
        }

        private void cirBtn_Click(object sender, EventArgs e)
        {
            CircleCenter circle = new CircleCenter(rhinoDocument);
        }
    }
}
