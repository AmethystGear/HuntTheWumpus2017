using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class Rock : PhysicsObject
    {
        
        public Rock(int roomNumber, Point position, int size, String name) : base(roomNumber, position, new Point(0, 0), new Point(0, 0), size, name)
        { }

        

        public static int maxNumberRocksPerRoom()
        {
            return 4;
        }

    }
}
