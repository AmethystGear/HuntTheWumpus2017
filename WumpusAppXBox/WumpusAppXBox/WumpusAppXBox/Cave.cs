using System;
using System.IO;
using System.Linq;

namespace WumpusAppXBox
{
    class Cave
    {
        string[] maps = new string[] { "Cave1file.txt", "Cave2file.txt", "Cave3file.txt", "Cave4file.txt", "Cave5file.txt" };
        string currentMap;
        int mapNum;

        public Cave(int mapNum)
        {
            this.mapNum = mapNum;
            currentMap = maps[mapNum-1];
        }

        public int[] getConnectedRooms(int room)
        {
            //gives all connected rooms. Rooms that aren't connected yield -1
            int[] connectedRoomsRaw = getConnectedRoomsRaw(room - 1);
            int[] adjacentRooms = getAdjacentRooms(room);
            int[] connectedRooms = new int[connectedRoomsRaw.Length];
            for (int i = 0; i < connectedRoomsRaw.Length; i++)
            {
                if (connectedRoomsRaw[i] != -1)
                {
                    connectedRooms[i] = adjacentRooms[i];
                }
                else
                {
                    connectedRooms[i] = -1;
                }
            }

            return connectedRooms;
        }

        private int [] getConnectedRoomsRaw(int room)
        {
            //gets the raw format of the connected rooms (i.e. 0,-1,0,-1...)
            string roomLine = File.ReadLines(currentMap).ElementAtOrDefault(room);
            
            string[] roomLineParsed = roomLine.Split(' ');
            int[] roomConnectedRaw = new int[roomLineParsed.Length];
            for (int i = 0; i < roomConnectedRaw.Length; i++)
            {
                roomConnectedRaw[i] = int.Parse(roomLineParsed[i]);
            }
            return roomConnectedRaw;
        }

        public int [] getAdjacentRooms (int room)
        {
            //gets the adjacent rooms
            int[] adjRooms = new int[6];
            adjRooms[3] = (room + 6) % 30;

            if(room % 6 != 0)
            {
                adjRooms[4] = (room + ((room + 1) % 2) * 6 + 1) % 30;
                adjRooms[5] = (room + (room % 2) * 24 + 1) % 30;
            }
            else
            {
                adjRooms[4] = (room + 1) % 30;
                adjRooms[5] = (room + 25) % 30;
            }
            
            adjRooms[0] = (room +24) % 30;

            if ((room - 1) % 6 != 0)
            {
                adjRooms[1] = (room + ((room + 1) % 2) * 6 + 23) % 30;
                adjRooms[2] = (room + (room % 2) * 24 + 5) % 30;
            }
            else
            {
                adjRooms[1] = (room + 29) % 30;
                adjRooms[2] = (room + 5) % 30;
            }

            for (int i = 0; i < adjRooms.Length; i++)
            {
                if (adjRooms[i] == 0)
                {
                    adjRooms[i] = 30;
                }
            }

            return adjRooms;
        }

    }
}