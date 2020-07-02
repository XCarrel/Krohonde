using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Krohonde.GreenColony
{
    public class GreenQueen : Queen
    {
        public GreenQueen(Point location, Point speed, Colony colony) : base(location, speed, colony)
        { }
        public override void Live()
        {
            base.Live();
            for (int i = 0; i < 30; i++)
            {
                if(Energy <= 20000)
                {
                    Eat(i);
                }
                else
                {
                    LayEgg(MotherNature.AntTypes.SoldierAnt, new Point((int)X + 10, (int)Y));
                }
                
            }
            DoNothing(); // The queen MUST either do something (Move, Eat, Lay an egg) or announce that she does nothing
        }
        
    }
}
