using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    //the interface for the static objects
    interface StaticObjectInterface
    {
        //these functions are required
        Point getPosition();

        Point getPositionRelativeTo(Point p, Rectangle screen);

        int getRoomNumber();

        int getSize();

        string getName();

        string[] getCollisions(PhysicsObject[] possibleCollidingObjs);
    }
}
