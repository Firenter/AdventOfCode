using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Classes
{
    class Santa
    {
        int north;
        int east;

        public Santa()
        {
            North = 0;
            East = 0;
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
        
        public void DeliverPresent(House house)
        {
            house.Presents += 1;
        }

    }
}
