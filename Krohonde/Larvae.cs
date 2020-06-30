using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Krohonde
{
    public class Larvae
    {
        private static int lastid = 0; // id if the last larvae that was instanciated
        private readonly int id;

        private readonly string name;
        private readonly MotherNature.AntTypes type;
        private readonly string certificate;
        private readonly int rot;

        private double maturity;     // 100 means it's birth time !!!

        private Point Location;

        public Larvae(MotherNature.AntTypes t, Point location, Queen mother)
        {
            id = ++lastid;
            type = t;
            name = mother.Colony.GetType().Name + this.GetType().Name + id;
            Location = location;
            certificate = mother.Colony.World().GetEggCertificate(name);
            rot = MotherNature.alea.Next(0, 360) ;
        }

        public Larvae(MotherNature.AntTypes t, Point location, Queen mother, int mat) : this(t,location,mother)
        {
            maturity = mat;
        }

        public void Grow(double deltatime)
        {
            maturity += deltatime;
            // TODO growth must cost food to colony
        }
        public double Maturity { get => maturity; }

        public MotherNature.AntTypes Type { get => type; }
        public System.Drawing.Point SDLocation { get => new System.Drawing.Point((int)Location.X, (int)Location.Y); }

        public string Name { get => name; }
        public string Certificate { get => certificate; }
        public int Rot { get => rot; }

    }
}
