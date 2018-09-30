using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class Room
    {
        int roomNumber = 0;
        List <CollisionLine> openRoomEdges = new List <CollisionLine>();
        List <CollisionLine> closedRoomEdges = new List<CollisionLine>();
        List<CollisionLine> roomEdges = new List<CollisionLine>();
        List<StaticObject> doors = new List<StaticObject>();

        int[] paths;
        int roomHeight;

        public List<StaticObject> getDoors()
        {
            return doors;
        }
        

        public List<StaticObject> createDoors()
        {
            List<StaticObject> doors = new List<StaticObject>();
            for (int i = 0; i < paths.Length; i++)
            {
                if(paths[i] !=  -1)
                {
                    Point doorPos = Point.midPoint(roomCorners(roomHeight)[i], roomCorners(roomHeight)[(i+1)%6]);
                    StaticObject door = new StaticObject(doorPos, 100, roomNumber, "door");
                    doors.Add(door);
                }
            }
            return doors;
           
        }

        public Point[] roomCorners(int roomHeight)
        {
            Point[] corners = new Point[6];
            corners[0] = new Point(-Math.Tan(Math.PI / 6) * roomHeight * 0.5, -roomHeight * 0.5);
            corners[1] = new Point(Math.Tan(Math.PI / 6) * roomHeight * 0.5, -roomHeight * 0.5);
            corners[2] = new Point((roomHeight* 0.5)/Math.Cos(Math.PI / 6), 0);
            corners[3] = new Point(Math.Tan(Math.PI / 6) * roomHeight * 0.5, roomHeight * 0.5);
            corners[4] = new Point(-Math.Tan(Math.PI / 6) * roomHeight * 0.5, roomHeight * 0.5);
            corners[5] = new Point(-(roomHeight * 0.5) / Math.Cos(Math.PI / 6), 0);            
            
            return corners;
        }

        public Room (int [] paths, int roomHeight, int roomNumber)
        {
            this.paths = paths;
            
            this.roomHeight = roomHeight;
            this.roomNumber = roomNumber;
            Point[] corners = roomCorners(roomHeight);
            for (int i = 0; i < 6; i++)
            {
                if (paths[i] == -1)
                {
                    
                    CollisionLine newCollLine = new CollisionLine(corners[i], corners[(i + 1) % 6], roomNumber,"wall");
                    openRoomEdges.Add(newCollLine);
                }
                else
                {
                   
                    Point p1 = new Point(corners[i].getX() * 2.0 / 3.0 + corners[(i + 1) % 6].getX() * 1.0 / 3.0, corners[i].getY() * 2.0 / 3.0 + corners[(i + 1) % 6].getY() * 1.0 / 3.0);
                    Point p2 = new Point(corners[i].getX() * 1.0 / 3.0 + corners[(i + 1) % 6].getX() * 2.0 / 3.0, corners[i].getY() * 1.0 / 3.0 + corners[(i + 1) % 6].getY() * 2.0 / 3.0);
                    CollisionLine newCollLine = new CollisionLine(corners[i], p1, roomNumber, "wall");
                    CollisionLine newCollLine2 = new CollisionLine(p2, corners[(i + 1) % 6], roomNumber, "wall");
                    openRoomEdges.Add(newCollLine);
                    openRoomEdges.Add(newCollLine2);
                }

                CollisionLine newCollLineClosed = new CollisionLine(corners[i], corners[(i + 1) % 6], roomNumber, "wall");
                closedRoomEdges.Add(newCollLineClosed);

                
            }
            doors = createDoors();
        }

        public void closeRoom()
        {
            roomEdges = closedRoomEdges;
            doors = new List<StaticObject>();
        }

        public void openRoom()
        {
            roomEdges = openRoomEdges;
            doors = createDoors();
        }

        public CollisionLine[] getRoomEdges()
        {
            return roomEdges.ToArray();
        }
    }
}
