using System;
using System.Media;
using System.IO;

namespace WumpusAppXBox
{
    class Sound
    {
        private String fullLine;
        private String[] Files;
        private StreamReader strmRead;
        private SoundPlayer sound1;
        private SoundPlayer sound2;


        //constructor
        public Sound()
        {
            strmRead = new StreamReader("SoundConfiguration.txt");//make sure to add the files to the configuration file and debug bin
            fullLine = strmRead.ReadLine();
            Files = fullLine.Split(' ');

            sound1 = new SoundPlayer(Files[0]);
            sound2 = new SoundPlayer(Files[1]);
            Console.WriteLine(Files[1]);
        }

        //BACKGROUND
        public void playSound1()
        {
            sound1.PlayLooping();
        }

        public void stopSound1()
        {
            sound1.Stop();
        }
        //WUMPUS FIGHT
        public void playSound2()
        {
            sound2.PlayLooping();
        }

        public void stopSound2()
        {
            sound2.Stop();
        }        
    }
}
