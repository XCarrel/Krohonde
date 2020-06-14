using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Krohonde.World
{
    public class FoodCluster
    {
        private List<Food> content;

        public FoodCluster()
        {
            content = new List<Food>();
        }

        public void Add(Food f)
        {
            content.Add(f);
        }

        public List<Food> Content { get => content; }
    }
}
