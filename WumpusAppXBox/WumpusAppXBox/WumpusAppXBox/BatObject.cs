using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class BatObject : PhysicsObject
    {
        
        int health = 3;
        int velocity = 4;


        public BatObject(int roomNumber, Point position, Point velocity, Point acceleration, int size, String name) : base(roomNumber, position, velocity, acceleration, size, name) { }

        public bool isAlive()
        {
            return health > 0;
        }

        public void batController(bool playerInRoom, PlayerObject player, CollisionLine[] collLines, PhysicsObject[] possibleCollidingObjects)
        {

            if (playerInRoom)
            {
                //follow the player
                Point playerPos = player.getPosition();

                Point direction = new Point(playerPos.getX() - getPosition().getX(), playerPos.getY() - getPosition().getY());
                double directionAngle = Math.Atan2(direction.getY(), direction.getX());
                Point vel = new Point(velocity * Math.Cos(directionAngle), velocity * Math.Sin(directionAngle));

                setVelocity(vel);

                string[] collisions = handleCollisions(collLines, possibleCollidingObjects);
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

                Console.WriteLine(numDamages);

                handleObj();
            }
        }
    }
}
