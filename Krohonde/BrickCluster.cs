using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krohonde
{
    public class BrickCluster
    {
        private List<Brick> content;

        public BrickCluster()
        {
            content = new List<Brick>();
        }

        public void Add(Brick f)
        {
            content.Add(f);
        }

        public List<Brick> Content { get => content; }

    }
}
