using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Classes
{
    class House
    {
        int north;
        int east;

        int presents;

        public House(int north, int east)
        {
            this.north = north;
            this.east = east;

            this.presents = 0;
        }

        public int North
        {
            get
            {
                return north;
            }

            set
            {
                north = value;
            }
        }

        public int East
        {
            get
            {
                return east;
            }

            set
            {
                east = value;
            }
        }
        
        public int Presents
        {
            get
            {
                return presents;
            }

            set
            {
                presents = value;
            }
        }
    }
}
