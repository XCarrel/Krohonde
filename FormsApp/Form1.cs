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
        private MotherNature myWorld;

        public Form1()
        {
            InitializeComponent();
            myWorld = new MotherNature();
            myWorld.AddAnt(new WorkerAnt(new Point(10, 10), new Point(10, 10), myWorld));
            myWorld.AddAnt(new FarmerAnt(new Point(10, 100), new Point(10, 5), myWorld));
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            myWorld.Live();
            ant0.SetBounds(myWorld.Ants[0].X, myWorld.Ants[0].Y, 16, 16);
            ant1.SetBounds(myWorld.Ants[1].X, myWorld.Ants[1].Y, 16, 16);
        }
    }
}
