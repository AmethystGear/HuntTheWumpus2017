using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class Point
    {
        // instance variables
        private double x;
        private double y;
       

        // constructors
        public Point(double inX, double inY)
        {
            this.x = inX;
            this.y = inY;
            
        }

        // mutators
        public void setPoint(double inX, double inY, int roomNumber)
        {
            x = inX;
            y = inY;
        }
        public void translate(double deltaX, double deltaY)
        {
            x += deltaX;
            y += deltaY;
        }
        
      
        // accessors
        public String toString()
        {
            String str = "Point: (" + x + ", " + y + ")";
            return str;
        }
        public double getX()
        {
            return x;
        }
        public double getY()
        {
            return y;
        }
      
        public Point translate(Point inP)
        {
            translate(inP.getX(), inP.getY());
            // translates position of player within the given room
            return this;
        }

        public double distToPt(Point inP)
        {
            return Math.Sqrt((inP.getX() - x) * (inP.getX() - x) + (inP.getY() - y) * (inP.getY() - y));
        }

        public double magnitude()
        {
            return distToPt(new Point(0, 0));
        }

        public double getAngleInRad(Point p2)
        {
            return Math.Atan2(getY() - p2.getY(), getX() - p2.getX());
        }

        public static Point midPoint(Point p1, Point p2)
        {
            return new Point((p1.getX() + p2.getX())/2.0, (p1.getY() + p2.getY()) / 2.0);
        }

        //gets if point is same or not
        public bool Equals(Point p)
        {
             return ((int)p.getX()) == ((int)x) && ((int)p.getY()) == ((int)y);
        }

        public Point Scale(double scaleFactor)
        {
            return new Point(x * scaleFactor, y * scaleFactor);
        }
    }
}
