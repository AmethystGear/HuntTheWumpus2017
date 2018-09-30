using System.Collections.Generic;
using System.Drawing;

namespace WumpusAppXBox
{
    /// <summary>
    /// class for all static objects
    /// static objects do not react to collisions. They may have collisions, 
    /// (physicsObjects collide with them) but they do not react to physics themselves
    /// </summary>
    class StaticObject : StaticObjectInterface
    {
        
        Point position;
        int size;
        int roomNumber;
        string name;
        public StaticObject (Point position, int size, int roomNumber, string name)
        {
            this.position = position;
            this.size = size;
            this.roomNumber = roomNumber;
            this.name = name;
        }

        public string getName()
        {
            return name;
        }

        public Point getPosition()
        {
            return position;
        }

        //gets position relative to a point.
        public Point getPositionRelativeTo(Point p, Rectangle screen)
        {
            return new Point((position.getX() - p.getX()) + screen.Width / 2.0, (p.getY() - position.getY()) + screen.Height / 2.0);
        }

        public int getRoomNumber()
        {
            return roomNumber;
        }

        public int getSize()
        {
            return size;
        }

        
        public string[] getCollisions(PhysicsObject[] possibleCollidingObjs)
        {
            //loop through all the possible colliding objects
            List<string> collidingObjs = new List<string>();
            for (int i = 0; i < possibleCollidingObjs.Length; i++)
            {
                //if the object is in the same room and is close to the static object, 
                //then add the name of the object to the list.
                if (possibleCollidingObjs[i].getRoomNumber() == roomNumber && position.distToPt(possibleCollidingObjs[i].getPosition()) < size)
                {
                    collidingObjs.Add(possibleCollidingObjs[i].getName());
                }
            }
            //return the list of strings that this object is colliding with.
            return collidingObjs.ToArray();
        }
    }
}
