using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Resources.DataModels
{
    internal class TowerGenJObject
    {
        public int Seed { get; set; }
        public int[,] TowerPlacements { get; set; }
    }
}
