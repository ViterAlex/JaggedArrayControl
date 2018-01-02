using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class ArrayControl<TControl> : UserControl
        where TControl: Control
    {
        public ArrayControl()
        {
            InitializeComponent();
        }

        protected Size DefaultElementSize = new Size(40, 20);

        public virtual FlowDirection FlowDirection
        {
            get { return flowLayoutPanel1.FlowDirection; }
            set { flowLayoutPanel1.FlowDirection = value; }
        }

        public string this[int i]
        {
            get { return flowLayoutPanel1.Controls.OfType<TControl>().FirstOrDefault(t => t.Name.Equals($"txt{i}"))?.Text; }
        }


        private void countNud_ValueChanged(object sender, EventArgs e)
        {
            UpdateObjects();
        }

        private void UpdateObjects()
        {
            var fields = flowLayoutPanel1.Controls.OfType<TextBox>().ToList();
            for (int i = fields.Count; i < countNud.Value; i++)
            {
                var ctrl = Activator.CreateInstance(typeof(TControl)) as Control;
                ctrl.Name = $"txt{i}";
                ctrl.Size = DefaultElementSize;
                flowLayoutPanel1.Controls.Add(ctrl);
            }

            for (int i = fields.Count; i > countNud.Value; i--)
            {
                flowLayoutPanel1.Controls.RemoveAt(i);
            }
        }
    }
}
