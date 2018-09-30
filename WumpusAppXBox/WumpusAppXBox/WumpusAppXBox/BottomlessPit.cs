using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class BottomlessPit : StaticObject
    {      
        public BottomlessPit(Point position, int size, int roomNumber, string name) : base(position, size, roomNumber, name){}

        public void managePit(PlayerObject player)
        {
            string [] collisionObjs = getCollisions(new PhysicsObject[] { player });

            for (int i = 0; i < collisionObjs.Length;i++)
            {
                if (collisionObjs[i].Equals("player"))
                {
                    player.kill();
                }
            }
            
        }
    }
}
