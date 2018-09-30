using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class Chest : StaticObject
    {
        bool alive = true;
        int goldInChest;
        int arrowsInChest;
        Question question;
        Inventory playerInventory;
        public Chest(Point position,  int size, int roomNumber, int goldInChest, int arrowsInChest,Question question, string name, Inventory playerInventory) : base(position, size, roomNumber, name)
        {
            this.playerInventory = playerInventory;
            this.goldInChest = goldInChest;
            this.arrowsInChest = arrowsInChest;

            //assign the questionNumber
            this.question = question;
        }

        public Question getQuestion()
        {
            return question;
        }

        //is the chest 'alive' / has the player answered the question the Chest has?
        public bool isAlive()
        {
            return alive;
        }

        bool collidingWithPlayer = false;
        bool justCollidedWithPlayer = false;

        public bool askTrivia()
        {
            return justCollidedWithPlayer;
        }

        public bool waitingForTriviaResult()
        {
            return collidingWithPlayer;
        }

        string chestAlerts = "";
        public string getChestAlerts()
        {
            if(triviaResults == null || !triviaResults[0])
            {
                return "";
            }
            else if (triviaResults[0] && triviaResults[1])
            {
                playerInventory.changeGold(goldInChest);
                playerInventory.changeArrows(arrowsInChest);
                return "You picked the correct answer! :) \r\n You got " + goldInChest + " gold coins and " + arrowsInChest + " arrow(s). \r\n";
            }
            else
            {
                return "You picked the wrong answer. :( \r\n";
            }           
        }

        public void manageChest(PlayerObject player)
        {
            
            string[] collisionObjs = getCollisions(new PhysicsObject[] { player });

            bool collidingWithPlayerTemp = collidingWithPlayer;
            collidingWithPlayer = false;
            justCollidedWithPlayer = false;
            for (int i = 0; i < collisionObjs.Length; i++)
            {
                if (collisionObjs[i].Equals("player"))
                {
                    justCollidedWithPlayer = !collidingWithPlayerTemp;
                    collidingWithPlayer = true;
                }
            }

            if (triviaResults != null && triviaResults[0])
            {
                if (triviaResults[1])
                {                   
                    
                }
                alive = false;
            }
           

        }

        bool[] triviaResults;
        public void setTriviaResults(bool[] triviaResults)
        {
            this.triviaResults = triviaResults;
        }
    }
}
