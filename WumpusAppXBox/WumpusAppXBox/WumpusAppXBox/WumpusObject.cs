using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class WumpusObject : PhysicsObject
    {
        int maxHealth = 5;
        int health = 5;
        int velocity = 5;
        int[] reloads = new int[] {100, 5, 5, 5, 5, 5, 5};
        int reloadNum = 0;
        int reload = 0;
        bool grounded = false;
        double currentShootingAngle = 0.0;
       

        public WumpusObject(int roomNumber, Point position, Point velocity, Point acceleration, int size, String name) : base(roomNumber, position, velocity, acceleration, size, name)
        {
            
        }
        public void reset()
        {
            reload = 0;
            reloadNum++;
            reloadNum %= (reloads.Length);            
        }

        public bool readyToShoot()
        {
            return reload > reloads[reloadNum];
        }

        public int getHealth()
        {
            return health;
        }

        public void ground()
        {
            grounded = true;
        }
        
        public double getCurrentShootingAngle()
        {
            return currentShootingAngle;
        }

        public string getWumpusAlerts()
        {
            return wumpusAlert;
        }

        string wumpusAlert = "";
        public void wumpusController(bool playerInRoom, PlayerObject player, PhysicsObject[] possibleCollidingObjs,CollisionLine[] collLines)
        {
            wumpusAlert = "";
            if (playerInRoom)
            {

                reload++;
                //follow the player
                Point playerPos = player.getPosition();

                
                currentShootingAngle = player.getPosition().getAngleInRad(getPosition()) + ((Math.Abs(reloadNum-(reloads.Length/2.0)) - reloads.Length/4.0)/(reloads.Length/2.0)) * 2;

                Point direction = new Point(playerPos.getX() - getPosition().getX(), playerPos.getY() - getPosition().getY());
                double directionAngle = Math.Atan2(direction.getY(), direction.getX());
                Point vel = new Point(velocity * Math.Cos(directionAngle), velocity * Math.Sin(directionAngle));

                setVelocity(vel);

                string[] collisions = handleCollisions(collLines, possibleCollidingObjs);
                int numDamages = 0;
                for (int i = 0; i < collisions.Length; i++)
                {
                    if (collisions[i].Contains("arrow"))
                    {
                        Console.WriteLine(collisions[i]);
                        health--;
                        numDamages++;
                    }                   
                }

                if(health <  maxHealth/2 && !grounded)
                {
                    Random gen = new Random();
                    int newRoomNumber = gen.Next(1, 31);
                    health = maxHealth;
                    while(newRoomNumber == getRoomNumber())
                    {
                        newRoomNumber = gen.Next(1, 31);
                    }
                    setRoomNumber(newRoomNumber);
                    wumpusAlert = "The wumpus ran away! :( \r\n";
                }
                    

                handleObj();
            }

        }
    }
}

