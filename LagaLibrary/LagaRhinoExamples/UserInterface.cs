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

namespace LagaRhinoExamples
{
    /// <summary>
    /// user interface
    /// </summary>
    public class UserInterface : Form
    {
        public UserInterface(RhinoDoc doc, RunMode mode)
        {
            Title = "LagaRhino examples";
            ClientSize = new Size(300, 500);
            Content = new Label { Text = "hi" };

            StackLayout sl = new StackLayout();
            sl.Padding = 10;
            sl.Orientation = Orientation.Vertical;
            sl.BackgroundColor = Colors.White;
            sl.Spacing = 5;
            sl.Items.Add(CreateButton("Find center of circle"));
            sl.Items.Add(CreateButton("Find the bigger circle"));

            this.Content = sl;

            /*
            ToolBar = new ToolBar
            {
                Items =
                {
                    new Command(),
                    new SeparatorToolItem(),
                    new ButtonToolItem { Text = "Click Me, ToolItem"},
 
                }
            };

            
            Menu = new MenuBar()
            {
                Items =
                {
                    new ButtonMenuItem
                    {
                        Text = "Examples",
                        Items =
                        { 
				            // you can add commands or menu items
				            new Command(),
				            // another menu item, not based off a Command
				            new ButtonMenuItem { Text = "Click Me 1, MenuItem" },
                            new ButtonMenuItem { Text = "Click Me 2, MenuItem" }
                        }
                    }
                }
            };*/

        }

        private Button CreateButton(string title)
        {
            var button = new Button();
            button.Size = new Size(200, 25);
            button.BackgroundColor = Colors.LightGrey;
            button.Click += Button_Click;
            button.Text = title;
            
            return button;
        }

        private void Button_Click(object sender, EventArgs e)
        {
            MessageBox.Show("here is call the first example of laga");
        }
    }
}
