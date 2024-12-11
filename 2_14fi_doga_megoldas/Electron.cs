using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace _2_14fi_doga_megoldas
{
    class Electron:UserControl
    {
        //prop
        public int speed { get; set; }
        public Electron() {
            InitializeComponent();
        }
        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Electron
            // 
            this.BackColor = Color.Lime;
            this.Name = "Electron";
            this.Size = new Size(10, 10);
            this.ResumeLayout(false);

        }
    }
}
