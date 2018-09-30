using System;
using System.Drawing;
using System.Diagnostics;

using System.Windows.Forms;
using System.Collections.Generic;

namespace WumpusAppXBox
{
    class GameControl
    {      
       
        Map map;
        WumpusUI ui;
        Rectangle screen;
        Inventory inv = new Inventory(10, 0);
        bool backgroundPlaying = true;
        bool[] playerMoves = new bool [4];
        Sound sound;
        public GameControl(WumpusUI ui,Rectangle screen, int mapNum, Sound sound)
        {
            
            this.sound = sound;
            this.ui = ui;
            
            this.screen = screen;
            ui.arrowBought += Ui_arrowBought;
            ui.secretBought += Ui_secretBought;
            map = new Map(mapNum);
        }    

        private void Ui_secretBought(object sender, EventArgs e)
        {
            Secret s = map.getTrivia().getSecret();
            if (map.getPlayer().getInventory().getGold() >= 1 && s != null)
            {
                map.getPlayer().getInventory().changeGold(-1);
                map.getPlayer().getInventory().addSecret(s);
            }
        }

        private void Ui_arrowBought(object sender, EventArgs e)
        {
            if(map.getPlayer().getInventory().getGold() >= 2)
            {
                map.getPlayer().getInventory().changeGold(-2);
                map.getPlayer().getInventory().changeArrows(1);
            }            
        }

        bool shooting = false;
        public void arrowShot(Point joystickPosition)
        {
            //if the player has reloaded an arrow and the player has more than 0 arrows
            if (map.getPlayer().reloaded() && map.getPlayer().getInventory().getArrows() > 0)
            {
                Console.WriteLine((joystickPosition.getX()) + "," + (joystickPosition.getY()));
                double angle = Math.Atan2((joystickPosition.getY()), (joystickPosition.getX()));
                Point vel = new Point(Math.Cos(angle) * Arrow.speed, Math.Sin(angle) * Arrow.speed);
                Point arrowPos = new Point(map.getPlayer().getPosition().getX(), map.getPlayer().getPosition().getY());
                map.addArrow(map.getPlayer().getRoomNumber(), arrowPos, vel);
                ui.newArrowProjectile(new Point(screen.Width / 2, screen.Height / 2),(float)(-angle * 180 / Math.PI +90));
                map.getPlayer().resetReload();
                map.getPlayer().getInventory().changeArrows(-1);
            }
           
        }

        

        public void onTimer(XBoxGamepad controller)
        {
            //if the player is alive
            if (map.getPlayer().isAlive())
            {
                //if the wumpus is alive
                if (map.getWumpus().getHealth() > 0)
                {
                    //shoot the arrow if the right trigger is pressed down
                    if(controller.RightTrigger > 0.5)
                    {
                        arrowShot(new Point(controller.RightStick.Position.X, controller.RightStick.Position.Y));
                    }


                    if (map.getPlayer().getRoomNumber() == map.getWumpus().getRoomNumber() && backgroundPlaying)
                    {
                        sound.stopSound1();
                        sound.playSound2();
                        backgroundPlaying = false;
                    }
                    else if(!backgroundPlaying && map.getPlayer().getRoomNumber() != map.getWumpus().getRoomNumber())
                    {
                        sound.stopSound2();
                        sound.playSound1();
                        backgroundPlaying = true;
                    }
                    
                    //close the room that the wumpus is in (so the player can't escape)
                    for (int i = 0; i < map.getRooms().Length; i++)
                    {
                        if (map.wumpusInRoom(i + 1))
                        {
                            map.getRooms()[i].closeRoom();
                            
                        }
                        else
                        {
                            map.getRooms()[i].openRoom();
                        }
                    }
                    


                    for (int i = 0; i < map.getBats().Length; i++)
                    {
                        if(!map.getBats()[i].isAlive())
                        {
                            map.getBats()[i].setRoomNumber(0);
                        }                        
                    }
                                       
                    for (int i = 0; i < map.getBats().Length; i++)
                    {
                        int[] connectedRooms = map.getConnectedRooms(map.getPlayer().getRoomNumber());
                        CollisionLine[] collLines = map.getRooms()[map.getPlayer().getRoomNumber() - 1].getRoomEdges();
                        List<PhysicsObject> collObjs = new List<PhysicsObject>();
                        collObjs.Add(map.getPlayer());
                        collObjs.AddRange(map.getArrows());
                        collObjs.Add(map.getWumpus());
                        map.getBats()[i].batController(map.batsInRoom()[i], map.getPlayer(), collLines,collObjs.ToArray());
                    }

                    for (int i = 0; i < map.getBottomlessPits().Length;i++)
                    {
                        map.getBottomlessPits()[i].managePit(map.getPlayer());
                    }

                    handlePlayer(new Point(controller.LeftStick.Position.X, controller.LeftStick.Position.Y));
                    
                    handleWumpus();

                    handleArrows();

                    handleFireballs();

                    
                    for (int i = 0; i < map.getChests().Length; i++)
                    {
                        map.getChests()[i].manageChest(map.getPlayer());
                        if(map.getChests()[i].askTrivia())
                        {
                            
                            ui.openTrivia(map.getChests()[i].getQuestion().questionNumber, map.getChests()[i].getQuestion().QuestionText, map.getChests()[i].getQuestion().answers, map.getPlayer().getInventory().getSecretsAsString());
                            int answer = ui.getRecentAnswer();
                            

                            playerMoves = new bool[] { false, false, false, false };
                            
                            if (answer == map.getChests()[i].getQuestion().GetCorrect())
                            {
                                map.getChests()[i].setTriviaResults(new bool[] { true, true });                                                           
                            }
                            else if(answer != -1)
                            {
                                map.getChests()[i].setTriviaResults(new bool[] { true, false });                                
                            }
                            else
                            {
                                map.getChests()[i].setTriviaResults(new bool[] { false, false });
                            }                   
                            
                            if (answer != -1)
                            {
                                ui.displayAlerts(map.getChests()[i].getChestAlerts());
                                map.removeChest(i);
                            }


                        }
                    }                    
                    
                    //draw the objects with their new positions in the UI
                    drawObjects();
                }
                else 
                {
                    if (!backgroundPlaying)
                    {
                        sound.stopSound2();
                        sound.playSound1();
                        backgroundPlaying = true;
                    }


                    //if the player orientation is facing right, 
                    //flip the player so that it faces left (reset player orientation)
                    if (map.getPlayer().getOrientation())
                    {
                        ui.drawPlayer(new Point(0, 0), true);
                    }

                    //kill all projectiles in the ui, map, and transparent pictureboxes
                    killProjectiles();
                    ui.winScreen();                 
                }
            }
            else
            {
                if (!backgroundPlaying)
                {
                    sound.stopSound2();
                    sound.playSound1();
                    backgroundPlaying = true;
                }


                //if the player orientation is facing right, 
                //flip the player so that it faces left (reset player orientation)
                if (map.getPlayer().getOrientation())
                {
                    ui.drawPlayer(new Point(0, 0), true);
                }

                //kill all projectiles in the ui, map, and transparent pictureboxes
                killProjectiles();
                ui.loseScreen();
            }
        }

        public void killProjectiles()
        {
            int numArrows = map.getArrows().Count;

            for (int i = 0; i < numArrows; i++)
            {
                map.removeArrow(0);
                TransparentPictureBox.removePictureBox(ui.getArrow(0));
                ui.killArrow(0);
            }

            int numFireballs = map.getFireballs().Count;

            for (int i = 0; i < numFireballs; i++)
            {
                map.removeFireball(0);
                TransparentPictureBox.removePictureBox(ui.getArrow(0));
                ui.killFireball(0);
            }
        }

        public void handleArrows()
        {
            //remove all thhe arrows that aren't alive
            for (int i = 0; i < map.getArrows().Count; i++)
            {
                if (!map.getArrows()[i].isAlive())
                {
                    map.removeArrow(i);
                    TransparentPictureBox.removePictureBox(ui.getArrow(i));
                    ui.killArrow(i);                    
                    i--;
                }                
            }

            //handle each arrow
            for (int i = 0; i < map.getArrows().Count; i++)
            {
                //get the CollisionLines in the arrow's room
                CollisionLine[] collLines = map.getRooms()[map.getArrows()[i].getRoomNumber() - 1].getRoomEdges();

                //get the list of objects that can collide with the arrows
                List<PhysicsObject> collidingObjs = new List<PhysicsObject>();
                collidingObjs.AddRange(map.getBats());
                collidingObjs.Add(map.getWumpus());
                collidingObjs.AddRange(map.getFireballs());
                collidingObjs.AddRange(map.getRocks());
                //handle the arrow
                map.getArrows()[i].arrowHandler(collLines, map.getWumpus(),collidingObjs.ToArray(),map.getConnectedRooms(map.getArrows()[i].getRoomNumber()));                
            }

        }

        public void handleFireballs()
        {
            //remove all thhe fireballs that aren't alive
            for (int i = 0; i < map.getFireballs().Count; i++)
            {
                if (!map.getFireballs()[i].isAlive())
                {
                    map.removeFireball(i);
                    TransparentPictureBox.removePictureBox(ui.getFireball(i));
                    ui.killFireball(i);                    
                    i--;
                }
            }

            //handle each arrow
            for (int i = 0; i < map.getFireballs().Count; i++)
            {
                //get the CollisionLines in the arrow's room
                
                CollisionLine[] collLines = map.getRooms()[map.getFireballs()[i].getRoomNumber() - 1].getRoomEdges();
                
                //get the list of objects that can collide with the arrows
                List<PhysicsObject> collidingObjs = new List<PhysicsObject>();
                collidingObjs.Add(map.getPlayer());
                

                //handle the arrow

                map.getFireballs()[i].FireballHandler(map.getPlayer(),collLines, collidingObjs.ToArray());
            }

        }

        public void handlePlayer(Point joystickPosition)
        {            
            int[] connectedRooms = map.getConnectedRooms(map.getPlayer().getRoomNumber());
            CollisionLine[] collLines = map.getRooms()[map.getPlayer().getRoomNumber()-1].getRoomEdges();
            map.getPlayer().playerController(connectedRooms, joystickPosition, collLines,map.getWumpus());
        }

        public void handleWumpus()
        {
            int[] connectedRooms = map.getConnectedRooms(map.getWumpus().getRoomNumber());
            CollisionLine[] collLines = map.getRooms()[map.getWumpus().getRoomNumber()-1].getRoomEdges();
            List <PhysicsObject> collObjs= new List<PhysicsObject>();
            collObjs.Add(map.getPlayer());
            collObjs.AddRange(map.getArrows());
            collObjs.AddRange(map.getBats());
            collObjs.AddRange(map.getRocks());
            map.getWumpus().wumpusController(map.wumpusInRoom(), map.getPlayer(), collObjs.ToArray(), collLines);

            if (map.getWumpus().readyToShoot())
            {
                double angle = map.getWumpus().getCurrentShootingAngle();
                Point position = new Point(map.getWumpus().getPosition().getX(), map.getWumpus().getPosition().getY());
                map.addFireball(map.getWumpus().getRoomNumber(), position, new Point(Math.Cos(angle) * 10, Math.Sin(angle)*10));
                ui.newFireball(map.getWumpus().getPositionRelativeTo(map.getPlayer().getPosition(), screen), (float) (-angle * 180/Math.PI + 90));                
                map.getWumpus().reset();
            }
            ui.displayAlerts(map.getWumpus().getWumpusAlerts());
        }

        //get the position of the room relative to the player
        private Point relRoomPos()
        {
            Point roomPos = new Point (-map.getPlayer().getPosition().getX() + (screen.Width / 2.0), map.getPlayer().getPosition().getY() + (screen.Height / 2.0));
       
            return roomPos;
        }

        //controls player based on user input -- runs on playerMoved event (from UI) and on playerCollision event (from Map)
        public void arrowBought ()
        {
            if(inv.getGold() > 1)
            {
                inv.changeGold(-1);
                inv.changeArrows(1);
            }
        }

        private void drawObjects()
        {
            
            
            ui.updateGold(map.getPlayer().getInventory().getGold());
            ui.updateArrows(map.getPlayer().getInventory().getArrows());
            

            
            for (int i = 0; i < map.getRooms()[map.getPlayer().getRoomNumber()-1].getDoors().Count; i++)
            {
                ui.drawDoor(map.getRooms()[map.getPlayer().getRoomNumber()-1].getDoors()[i].getPositionRelativeTo(map.getPlayer().getPosition(), screen),i);                
            }
            
            for (int i = map.getRooms()[map.getPlayer().getRoomNumber()-1].getDoors().Count; i < 6; i++)
            {
                ui.drawDoor(new Point(-10000000, -1000000), i);
            }

            ui.drawRoom(relRoomPos());
            

            bool chestInRoom = false;
            for (int i = 0; i < map.getChests().Length; i++)
            {
                
                if (map.getChests()[i].getRoomNumber() == map.getPlayer().getRoomNumber())
                {
                    
                    ui.drawChest(map.getChests()[i].getPositionRelativeTo(map.getPlayer().getPosition(), screen));
                    chestInRoom = true;
                }
            }

            if(chestInRoom == false)
            {
                
                ui.drawChest(new Point(-1000000, -1000000));
            }

            int numRocks = 0;
            for (int i = 0; i < map.getRocks().Length; i++)
            {
                if (map.getRocks()[i].getRoomNumber() == map.getPlayer().getRoomNumber())
                {
                    ui.drawRock(numRocks, map.getRocks()[i].getPositionRelativeTo(map.getPlayer().getPosition(), screen));
                    numRocks++;
                }               
            }

            while (numRocks < Rock.maxNumberRocksPerRoom())
            {
                ui.drawRock(numRocks, new Point(-100000,-100000));
                numRocks++;
            }



            for (int i = 0; i < map.getBottomlessPits().Length;i++)
            {
                if(map.getBottomlessPits()[i].getRoomNumber() == map.getPlayer().getRoomNumber())
                {
                    ui.drawPit(map.getBottomlessPits()[i].getPositionRelativeTo(map.getPlayer().getPosition(), screen), i);
                }
                else
                {
                    ui.drawPit(new Point(-100000, -100000),i);
                }
            }

            for(int i = 0; i < map.getArrows().Count;i++)
            {              
                if(map.getArrows()[i].getRoomNumber() == map.getPlayer().getRoomNumber())
                {
                    ui.drawArrow(map.getArrows()[i].getPositionRelativeTo(map.getPlayer().getPosition(), screen), i);
                }
                else
                {
                    ui.drawArrow(new Point(-1000000, -100000), i);
                }
                
            }

            for (int i = 0; i < map.getFireballs().Count; i++)
            {
                if (map.getFireballs()[i].getRoomNumber() == map.getPlayer().getRoomNumber())
                {
                    
                    ui.drawFireball(map.getFireballs()[i].getPositionRelativeTo(map.getPlayer().getPosition(), screen),0, i);
                }
                else
                {
                    
                    ui.drawFireball(new Point(-1000000, -100000),0, i);
                }

            }

            
            for (int i = 0; i < map.getBats().Length; i++)
            {
                if(map.batsInRoom()[i] && map.getBats()[i].isAlive())
                {
                    ui.drawBat(map.getBats()[i].getPositionRelativeTo(map.getPlayer().getPosition(),screen), i);
                }
                else
                {
                    ui.drawBat(new Point(-1000000, -1000000), i);
                }
            }
            
            if (map.getWumpus().getRoomNumber() == map.getPlayer().getRoomNumber())
            {
               ui.drawWumpus(map.getWumpus().getPositionRelativeTo(map.getPlayer().getPosition(),screen));
            }
            else
            {
               ui.drawWumpus(new Point(-100000, -100000));
            }
          
            ui.drawPlayer(map.getPlayer().getPositionRelativeTo(map.getPlayer().getPosition(),screen), map.getPlayer().getFlip());

            ui.refresh();

            ui.displayAlerts(map.getPositionAlerts());
        }

        public int getGold()
        {
            return map.getPlayer().getInventory().getGold();
        }

        public int getArrows()
        {
            return map.getPlayer().getInventory().getArrows();
        }

        public int getTurns()
        {
            return map.getPlayer().getTurns();
        }
    }
}
