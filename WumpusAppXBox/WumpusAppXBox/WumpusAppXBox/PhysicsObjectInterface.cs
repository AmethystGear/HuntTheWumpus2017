using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    interface PhysicsObjectInterface
    {
        Point getPosition();

        Point getVelocity();

        Point getAcceleration();

        int getRoomNumber();

        int getSize();

        String getName();

        void setRoomNumber(int roomNum);

        void setPosition(Point p);

        void setVelocity(Point p);

        void setAcceleration(Point p);

        string[] handleCollisions(CollisionLine [] collLines,PhysicsObject[] possibleCollidingObjs);

        

        void handleObj();     
    }
}
