using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class CollisionLine
    {
        Point p1 = new Point(0, 0);
        Point p2 = new Point(0, 0);
        int roomNumber = 0;
        string name;
        public CollisionLine(Point p1, Point p2, int roomNumber,string name)
        {
            this.p1 = p1;
            this.p2 = p2;
            this.roomNumber = roomNumber;
            this.name = name;
        }

        public string getName()
        {
            return name;
        }

        public double getRoomNumber()
        {
            return roomNumber;
        }

        public double getAngleInRad ()
        {
            return Math.Atan2(p1.getY() - p2.getY(), p1.getX() - p2.getX());
        }

        public Point getP1()
        {
            return p1;
        }

        public Point getP2()
        {
            return p2;
        }
    }
}
