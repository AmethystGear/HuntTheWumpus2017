using System;
using System.Collections.Generic;


namespace WumpusAppXBox
{
    class Map
    {
        Trivia trivia = new Trivia();
        Cave c;
        BatObject[] bats = new BatObject[2];
        BottomlessPit[] bottomlessPits = new BottomlessPit[2];
        PlayerObject player;
        WumpusObject wumpus;
        Room[] rooms = new Room[30];

        int roomheight = 1600;

        List<Rock> rocks = new List<Rock>();
        List<Arrow> arrows = new List<Arrow>();
        List<Fireball> fireballs = new List<Fireball>();


        List<Chest> chests = new List<Chest>();
        int prevAlertRoomNum = 0;
        int newAlertRoomNum = 0;
        Random gen = new Random(DateTime.Now.Minute);

        public Map(int mapNum)
        {
            PhysicsObject.reset();
            c = new Cave(mapNum);
            printBadConnections();
            generateMap();
            newAlertRoomNum = player.getRoomNumber();                   
        }

        public void printBadConnections()
        {
            for (int i = 0; i < rooms.Length; i++)
            {
                int [] connectedRooms = c.getConnectedRooms(i + 1);
                for (int i1 = 0; i1 < connectedRooms.Length; i1++)
                {
                    if(connectedRooms[i1] != -1)
                    {
                        int[] connectedConnectedRooms = c.getConnectedRooms(connectedRooms[i1]);
                        bool hasOrigional = false;
                        for (int i3 = 0; i3 < connectedConnectedRooms.Length; i3++)
                        {
                            if(connectedConnectedRooms[i3] == i + 1)
                            {
                                hasOrigional = true;
                            }
                        }
                        if(!hasOrigional)
                        {
                            Console.WriteLine("room # " + connectedRooms[i1] + " does not have a connection to room # " + (i+1));
                        }
                    }
                }
            }
        }

        public void generateMap()
        {
            //generate player start pos
            player = new PlayerObject(gen.Next(1, 31), new Point(0, 0), new Point(0, 0), new Point(0, 0), 100, roomheight, "player");

            //generate pos for bottomlessPits
            for (int i = 0; i < bottomlessPits.Length; i++)
            {
                int roomVal = gen.Next(1, 31);
                while (roomVal == player.getRoomNumber())
                {
                    roomVal = gen.Next(1, 31);
                }
                Point position = new Point(gen.Next(-300, 300), gen.Next(-300, 300));

                bottomlessPits[i] = new BottomlessPit(new Point(gen.Next(-300, 300), gen.Next(-300, 300)), 200, roomVal, "bottomless pit");
            }

            //
            for (int i = 0; i < rooms.Length; i++)
            {
                int numRocks = gen.Next(1, 5);
                List<Rock> rocksInRoom = new List<Rock>();
                for (int i1 = 0; i1 < numRocks; i1++)
                {
                    Point rockPos = new Point(gen.Next(-400, 400), gen.Next(-400, 400));
                    bool colliding = false;
                    while (colliding)
                    {
                        rockPos = new Point(gen.Next(-400, 400), gen.Next(-400, 400));
                       
                        for (int i2 = 0; i2 < bottomlessPits.Length; i2++)
                        {
                            if (rockPos.distToPt(bottomlessPits[i].getPosition()) < 200)
                            {
                                colliding = true;
                            }
                        }
                    }
                    
                    Rock newRock = new Rock(i + 1, rockPos, 50, "rock");
                    rocks.Add(newRock);
                    rocksInRoom.Add(newRock);
                }
                rooms[i] = new Room(c.getConnectedRooms(i + 1), roomheight, i + 1);
            }




            //generate start pos for bats
            for (int i = 0; i < bats.Length; i++)
            {
                int roomVal = gen.Next(1, 31);
                while (roomVal == player.getRoomNumber())
                {
                    roomVal = gen.Next(1, 31);
                }
                bats[i] = new BatObject(roomVal, new Point(0, 0), new Point(0, 0), new Point(0, 0), 100, "bat");
            }          

            //generate Wumpus pos
          
            wumpus = new WumpusObject(8, new Point(0, 0), new Point(0, 0), new Point(0, 0), 150, "wumpus");

            int room;
            chests = new List<Chest>();
            trivia = new Trivia();
            for (int i = 0; i < 15; i++)
            {
                room = gen.Next(1, 31);
                int numChests = chests.Count;
                Console.WriteLine(i);
                for (int i1 = 0; i1 < numChests; i1++)
                {
                    if(room == chests[i1].getRoomNumber() || room == player.getRoomNumber())
                    {
                        i1 = 0;
                        room = gen.Next(1, 31);                        
                    }
                }

                Console.WriteLine("past the room selection");
                Chest chest = null;
                bool colliding = true;
                while (colliding)
                {
                    colliding = false;
                    
                    chest = new Chest(new Point(gen.Next(-500, 500), gen.Next(-500, 500)), 100, room, gen.Next(6, 11), gen.Next(0, 3), trivia.getQuestion(), "chest", player.getInventory());
                    
                    string[] collisions = chest.getCollisions(rocks.ToArray());
                    Console.WriteLine(chest.getPosition().toString());
                    for (int i1 = 0; i1 < collisions.Length; i1++)
                    {
                        if(collisions[i1].Equals("rock"))
                        {
                            colliding = true;
                            Console.WriteLine("colliding with rock");
                        }
                    }

                    for (int i1 = 0; i1 < bottomlessPits.Length; i1++)
                    {
                        if (bottomlessPits[i1].getPosition().distToPt(chest.getPosition()) < 200)
                        {
                            colliding = true;
                            Console.WriteLine("colliding with pit");
                        }
                    }
                }
                chests.Add(chest);
                Console.WriteLine("past position selection");
            }


            
        }

        public Trivia getTrivia()
        {
            return trivia;
        }

        //gets all chests in the map
        public Chest [] getChests()
        {            
            return chests.ToArray();
        }

        //replaces a single chest -- occurs when a player answers a chest's trivia question
        public void removeChest(int index)
        {
            //remove the chest from the list
            chests.Remove(chests[index]);            
        }

        public void removeArrow(int index)
        {
            PhysicsObject.deleteObject(arrows[index]);
            arrows.Remove(arrows[index]);
        }

        public void addArrow(int roomNum, Point pos, Point vel)
        {
            arrows.Add(new Arrow(roomNum, pos, vel, 100, "arrow " + arrows.Count));
            
        }

        public void removeFireball(int index)
        {
            PhysicsObject.deleteObject(fireballs[index]);
            fireballs.Remove(fireballs[index]);
        }

        public void addFireball(int roomNum, Point pos, Point vel)
        {
            fireballs.Add(new Fireball(roomNum, pos, vel,new Point(0,0), 30, "fireball"));
        }

        public List<Fireball> getFireballs()
        {
            return fireballs;
        }

        public List<Arrow> getArrows()
        {
            return arrows;
        }

        public Room[] getRooms()
        {
            return rooms;
        }

        public PlayerObject getPlayer()
        {
            return player;
        }

        public WumpusObject getWumpus()
        {
            return wumpus;
        }

        public BatObject[] getBats()
        {
            return bats;
        }

        public Rock [] getRocks()
        {
            return rocks.ToArray();
        }

        public BottomlessPit[] getBottomlessPits()
        {
            return bottomlessPits;
        }

        public bool wumpusInRoom()
        {
            return wumpus.getRoomNumber() == player.getRoomNumber();
        }

        public bool wumpusInRoom(int room)
        {
            return wumpus.getRoomNumber() == room;
        }

        public bool pitInRoom(int room)
        {
            for (int i = 0; i < bottomlessPits.Length; i++)
            {
                if (bottomlessPits[i].getRoomNumber() == room)
                {
                    return true;
                }
            }
            return false;
        }

        public bool pitInRoom()
        {
            return pitInRoom(player.getRoomNumber());
        }

        public bool[] batsInRoom()
        {            
            return batsInRoom(player.getRoomNumber());
        }

        public bool[] batsInRoom(int room)
        {
            bool[] batsInRoom = new bool[bats.Length];
            for (int i = 0; i < bats.Length; i++)
            {
                batsInRoom[i] = bats[i].getRoomNumber() == room;
            }
            return batsInRoom;
        }

        public int[] getConnectedRooms(int room)
        {
            return c.getConnectedRooms(room);
        }

        public bool hasObject(int room)
        {
            return wumpusInRoom(room) || batsInRoom(room)[0] || batsInRoom(room)[1] || pitInRoom(room);
        }

        public bool roomChanged()
        {
            return hasRoomChanged;
        }

        public int getPrevRoomNum()
        {
            return prevAlertRoomNum;
        }

        

        string oldAlert = "";

        bool hasRoomChanged = false;
        public string getPositionAlerts()
        {
            
            string wumpusNearby = "I smell a wumpus \r\n";
            string batNearby = "I sense a bat is close \r\n";
            string bottomlessPitNearby = "I feel a draft \r\n";
            
            
            string movedByBat = "I was just moved by a bat! \r\n";
            string fullAlert = "";

            

            if(player.movedByBat())
            {
                Random gen = new Random();
                int roomVal = gen.Next(1, 31);
                while (hasObject(roomVal))
                {
                    roomVal = gen.Next(1, 31);
                }
                player.setRoomNumber(roomVal);
                player.setPosition(new Point(0, 0));
                
                fullAlert += movedByBat;
            }

            newAlertRoomNum = player.getRoomNumber();
            string movedToRoom = "Entered room " + player.getRoomNumber() + "\r\n";
            hasRoomChanged = prevAlertRoomNum != newAlertRoomNum;
            if (hasRoomChanged)
            {
                fullAlert += movedToRoom;

                for (int i = 0; i < c.getAdjacentRooms(player.getRoomNumber()).Length; i++)
                {
                    if (wumpus.getRoomNumber() == c.getAdjacentRooms(player.getRoomNumber())[i])
                    {
                        fullAlert += wumpusNearby;
                    }
                    if ((bats[0].getRoomNumber() == c.getAdjacentRooms(player.getRoomNumber())[i] || bats[1].getRoomNumber() == c.getAdjacentRooms(player.getRoomNumber())[i]) && !fullAlert.Contains(batNearby))
                    {
                        fullAlert += batNearby;
                    }
                    if ((bottomlessPits[0].getRoomNumber() == c.getAdjacentRooms(player.getRoomNumber())[i] || bottomlessPits[1].getRoomNumber() == c.getAdjacentRooms(player.getRoomNumber())[i]) && !fullAlert.Contains(bottomlessPitNearby))
                    {
                        fullAlert += bottomlessPitNearby;
                    }
                }
            }



            prevAlertRoomNum = player.getRoomNumber();

            if(oldAlert != fullAlert)
            {
                oldAlert = fullAlert;
                return fullAlert;
            }
            else
            {
                return "";
            }
            
        }
    }
}

