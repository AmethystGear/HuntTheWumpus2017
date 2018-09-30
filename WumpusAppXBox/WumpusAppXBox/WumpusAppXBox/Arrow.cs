using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class Arrow : PhysicsObject
    {
        public static int speed = 15;
        public Arrow(int roomNumber, Point position, Point velocity, int size, String name) : base(roomNumber, position, velocity, new Point(0,0), size, name)
        {}

        bool alive = true;

        public bool isAlive()
        {
            return alive;
        }

        public double angle ()
        {
            return getVelocity().getAngleInRad(new Point(0, 0));
        }

        string[] collidingObjs = new string[0];
        public void arrowHandler(CollisionLine[] collLines, WumpusObject wumpus, PhysicsObject[] possibleColldingObjs, int[] connectedRooms)
        {
            collidingObjs = handleCollisions(collLines, possibleColldingObjs);

            for (int i = 0; i < collidingObjs.Length; i++)
            {
                if (collidingObjs[i].Equals("wumpus") || collidingObjs[i].Equals("bat") || collidingObjs[i].Equals("wall") || collidingObjs[i].Equals("fireball") || collidingObjs[i].Equals("rock"))
                {
                    alive = false;
                }
                Console.WriteLine(collidingObjs[i]);
            }

            if(wumpus.getRoomNumber() == getRoomNumber() && numberTimesRoomChange > 0)
            {
                wumpus.ground();
            }

            if(numberTimesRoomChange == 2)
            {
                alive = false;
            }

            tryRoomChange(connectedRooms);

            handleObj();
        }

        //calculates the room positions outside the room
        public Point[] calculateRoomPosArray()
        {
            int roomheight = 1600;
            //Get all the positions of the rooms adjacent to the center room
            Point[] p = new Point[6];
            //the south room
            p[0] = new Point(0, -roomheight);
            //the southwest room
            p[1] = new Point(roomheight * Math.Sqrt(3.0) * 0.5, -roomheight * 0.5);
            //the northwest room
            p[2] = new Point(roomheight * Math.Sqrt(3.0) * 0.5, roomheight * 0.5);
            //the north room
            p[3] = new Point(0, roomheight);
            //the northeast room
            p[4] = new Point(-roomheight * Math.Sqrt(3.0) * 0.5, roomheight * 0.5);
            //the southeast room
            p[5] = new Point(-roomheight * Math.Sqrt(3.0) * 0.5, -roomheight * 0.5);
            return p;
        }

        int numberTimesRoomChange = 0;
        public void tryRoomChange(int[] connectedRooms)
        {

            //get all the connected rooms from cave

            //get the positions of the adjacent rooms
            Point[] roomPosns = calculateRoomPosArray();
            //get the position of the current room
            Point org = new Point(0, 0);
            //loop through all the adjacent rooms
            for (int i = 0; i < connectedRooms.Length; i++)
            {

                //if the player is closer to one of the adjacent rooms then the current room, 
                //and the current room is connected (connectedRooms[i] != 0)
                if ((getPosition().distToPt(roomPosns[i]) < getPosition().distToPt(org)) && connectedRooms[i] != 0)
                {
                    //the player has changed rooms!
                    //make a new point that is the player's new position (player in new room) and return it
                    setRoomNumber(connectedRooms[i]);
                    setPosition(new Point(getPosition().getX() - roomPosns[i].getX(), getPosition().getY() - roomPosns[i].getY()));
                    numberTimesRoomChange++;
                }
            }
        }

    }
}
