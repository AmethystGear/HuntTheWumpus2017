using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WumpusAppXBox
{
    class PhysicsObject : PhysicsObjectInterface
    {
        Point position;
        Point velocity;
        Point acceleration;
        int roomNumber;
        int collsionForceFactor = 5;
        /*size/name is not settable except in constructor 
        because an object's size/name should stay the same*/
        string name = "";
        int size = 0;
        static List <PhysicsObject> allPhysicsObjects = new List<PhysicsObject>();
      

        public PhysicsObject(int roomNumber, Point position, Point velocity, Point acceleration, int size, String name)
        {
            allPhysicsObjects.Add(this);
            this.position = position;
            this.velocity = velocity;
            this.acceleration = acceleration;
            this.roomNumber = roomNumber;
            this.size = size;
            this.name = name;
            
        }


        public static PhysicsObject[] getAllObjects()
        {
            return allPhysicsObjects.ToArray();
        }

        public static void reset()
        {
            allPhysicsObjects = new List<PhysicsObject>();
        }

        public static void deleteObject(PhysicsObject p)
        {
            allPhysicsObjects.Remove(p);
        }

        public String getName()
        {
            return name;
        }

        public int getSize()
        {
            return size;
        }

        //the gets for the object
        public Point getPosition()
        {
            return position;
        }

        public Point getVelocity()
        {
            return velocity;
        }

        public Point getAcceleration()
        {
            return acceleration;
        }

        public int getRoomNumber()
        {
            return roomNumber;
        }


        //the sets for the object
        public void setPosition(Point p)
        {
            position = p;
        }

        public void setVelocity(Point p)
        {
            velocity = p;
        }

        public void setAcceleration(Point p)
        {
            acceleration = p;
        }

        public void setRoomNumber(int roomNum)
        {
            
            roomNumber = roomNum;            
        }

        public Point getPositionRelativeTo (Point p, Rectangle screen)
        {
            return new Point((getPosition().getX() - p.getX()) + screen.Width / 2.0, (p.getY() - getPosition().getY()) + screen.Height / 2.0); 
        }

        //collision detection (with static objects) and reaction
        public string [] handleCollisions(CollisionLine[] collLines,PhysicsObject[] possibleCollidingObjs)
        {
            List<string> objectsColliding = new List<string>();


            Point vel = new Point(0,0);
            bool collOccurred = false;
            for (int i = 0; i < collLines.Length; i++)
            {
                double d1 = collLines[i].getP1().distToPt(getPosition());
                double d2 = collLines[i].getP2().distToPt(getPosition());
                double c = collLines[i].getP1().distToPt(collLines[i].getP2());
               

                double distToCollLine = Math.Sqrt(d1*d1 - Math.Pow(((d1*d1 - d2*d2) / (2.0*c) + c/2.0),2));
                double collBorder = Math.Sqrt(Math.Abs(d1 * d1 - d2 * d2));
                
                if (collBorder < c && distToCollLine < size/2)
                {
                    objectsColliding.Add(collLines[i].getName());
                    Point velTranslateVector = new Point(-Math.Cos(collLines[i].getAngleInRad() + Math.PI / 2) * collsionForceFactor, -Math.Sin(collLines[i].getAngleInRad() + Math.PI/2) * collsionForceFactor);
                    vel.translate(velTranslateVector);
                    collOccurred = true;
                } 
            }

            
            for (int i = 0; i < possibleCollidingObjs.Length; i++)
            {
                double dist = position.distToPt(possibleCollidingObjs[i].getPosition());
                if (dist < (size + possibleCollidingObjs[i].getSize()) * 0.5 && possibleCollidingObjs[i].getRoomNumber() == getRoomNumber() && possibleCollidingObjs[i] != this)
                {
                    objectsColliding.Add(possibleCollidingObjs[i].getName());
                    double angle = position.getAngleInRad(possibleCollidingObjs[i].getPosition());
                    Point velTranslateVector = new Point(Math.Cos(angle) * collsionForceFactor, Math.Sin(angle) * collsionForceFactor);
                    vel.translate(velTranslateVector);
                    collOccurred = true;
                }
            }
            

            if (collOccurred)
            {
                setVelocity(vel);
            }

            return objectsColliding.ToArray();
        }

        




        //handles object acceleration, velocity, and position
        public void handleObj()
        {
            Point accel = getAcceleration();
            Point vel = getVelocity();
            Point pos = getPosition();

            vel.translate(accel);
            pos.translate(vel);

            setVelocity(vel);
            setPosition(pos);
            
        }

        
        
    }
}
