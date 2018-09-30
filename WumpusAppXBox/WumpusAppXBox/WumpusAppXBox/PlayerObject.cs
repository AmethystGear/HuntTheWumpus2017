using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class PlayerObject : PhysicsObject
    {
        int roomheight = 0;
        int turns = 0;
        double maxSpeed = 15;
        Inventory inv = new Inventory(10,0);

        public PlayerObject(int roomNumber, Point position, Point velocity, Point acceleration, int size, int roomheight,String name) : base(roomNumber, position, velocity, acceleration, size, name)
        {
            this.roomheight = roomheight;
        }

        public bool movedByBat()
        {
            return touchingBat;
        }

        //calculates the room positions outside the room
        public Point[] calculateRoomPosArray()
        {
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

        public int getTurns()
        {
            return turns;
        }

        //tries to change the player room number - if not possible then does nothing
        public void tryRoomChange(int [] connectedRooms)
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
                //and the current room is connected (connectedRooms[i] != -1)
                if ((getPosition().distToPt(roomPosns[i]) < getPosition().distToPt(org)) && connectedRooms[i] != -1)
                {
                    //the player has changed rooms!
                    //make a new point that is the player's new position (player in new room) and return it
                    setRoomNumber(connectedRooms[i]);
                    turns++;
                    setPosition (new Point(getPosition().getX() - roomPosns[i].getX(), getPosition().getY() - roomPosns[i].getY()));
                }
            }            
        }

        bool roomChanged = false;
        bool touchingBat = false;
        double health = 10;

        public bool hasRoomChanged()
        {
           
            return roomChanged;
        }

        public void kill()
        {
            health = 0;
        }

        public void changeHealth(int delta)
        {
            health += delta;
        }

        public bool isAlive()
        {
            return health > 0;
        }

        public void askTrivia()
        {

        }

        public bool getFlip()
        {
            return prevOrientation != orientation;
        }

        bool prevOrientation = false;
        bool orientation = false;
        
        //handles the player
        public void playerController(int[] connectedRooms, Point playerMovement,CollisionLine [] collLines, WumpusObject wumpus)
        {
            prevOrientation = orientation;
            reload++;

            setVelocity(playerMovement.Scale(maxSpeed));
            
            tryRoomChange(connectedRooms);      
            string [] collisions = handleCollisions(collLines, getAllObjects());
            touchingBat = false;
            for (int i = 0; i < collisions.Length; i++)
            {
                if (collisions[i] == "bat")
                {
                    touchingBat = true;
                }
            }

            handleObj();
            
        }

        public Inventory getInventory()
        {
            return inv;
        }

        int reload = 0;
        int max = 40;
        public bool reloaded()
        {
            return reload >= max;
        }

        public void resetReload()
        {
            reload = 0;
        }

        public bool justReloaded()
        {
            return reload == max;
        }

        public bool getOrientation()
        {
            return orientation;
        }
    }
}
