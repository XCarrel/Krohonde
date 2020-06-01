using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Krohonde.Creatures;
using Krohonde.World;
using Creatures;

namespace FormsApp
{
    public partial class Form1 : Form
    {
        private WorkerAnt myAnt;
        private MotherNature myWorld;

        public Form1()
        {
            InitializeComponent();
            myWorld = new MotherNature();
            myAnt = new WorkerAnt(new Point(10, 10), new Point(10, 10), myWorld);
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            myAnt.Live();
            ant0.SetBounds(myAnt.X, myAnt.Y,40,40);
        }
    }
}
