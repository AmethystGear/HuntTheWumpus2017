using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class Fireball : PhysicsObject
    {
        int lifespan = 300;
        int life = 0;
        public Fireball(int roomNumber, Point position, Point velocity, Point acceleration, int size, String name) : base(roomNumber, position, velocity, acceleration, size, name)
        {

        }

        

        bool alive = true;

        public bool isAlive()
        {
            return alive;
        }

        public double angle()
        {
            return getVelocity().getAngleInRad(new Point(0, 0));
        }

        string[] collidingObjs = new string[0];
        public void FireballHandler(PlayerObject player,CollisionLine[] collLines,PhysicsObject[] collidingObjects)
        {
            life++;
            //look through the colliding objects array. If the fireball is colliding with a wall or a player, then set alive to false.
            for (int i = 0; i < collidingObjs.Length; i++)
            {                
                if (collidingObjs[i].Equals("player") || collidingObjs[i].Equals("wall"))
                {
                    if (collidingObjs[i].Equals("player"))
                    {
                        player.changeHealth(-4);
                    }
                    alive = false;                    
                }
            }
            

            

            //handle collisions with all objects, get the names of the objects that the fireball is colliding with.
            collidingObjs = handleCollisions(collLines,collidingObjects);           

            if(getPosition().distToPt(new Point(0,0)) > 1000)
            {
                alive = false;
            }

            if(life > lifespan)
            {
                alive = false;
            }

            //handle the physics of the object
            handleObj();
        }
    }
}
