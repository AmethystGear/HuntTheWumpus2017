using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace WumpusAppXBox
{
    class HighScore
    {
        //intstance variables
        private StreamWriter strmWrite;
        private StreamReader strmRead;
        private int playerScore;
        private int[] scoresSorted;
        private int[] scoresRaw;
        private String[] names;
        private int line;

        //constructor
        public HighScore()
        {
            strmWrite = new StreamWriter("HighScoresModified.txt");
            strmRead = new StreamReader("HighScores.txt");
            playerScore = 0;
            scoresSorted = new int[10];
            scoresRaw = new int[10];
            names = new String[10];
            line = 0;
        }

        public String[] topScoreNames()
        {
            //returns a String[] of the names (least to greatest)

            strmRead = new StreamReader("HighScores.txt");
            for (int n = 0; n < 10; n++)
            {
                strmRead.ReadLine();
            }

            for (int i = 0; i < names.Length; i++)
            {                
                string line = strmRead.ReadLine();
                names[i] = line.Split('%')[0];            
            }

            for(int j = 0; j < 10;j++) //allign the names to the correct scores
            {
                for (int b = 0; b < 10; b++)
                {
                    //compare scores from raw to sorted, raw index = name, make the name match the sorted score
                    if(scoresRaw[j] == scoresSorted[b])
                    {
                        String x = names[j];
                        String y = names[b];
                        names[b] = x;
                        names[j] = y;
                    }
                }
            }
            return names; 
        }

        public int[] topScores()
        {
            //returns an int[] of all the top scores sorted (least to greatest)
            scoresRaw = getScoresRaw();
            scoresSorted = scoresRaw;
            Array.Sort(scoresSorted);
            return scoresSorted;
        }

        public int getPlayerScore(int gold, int turns, int arrows)
        {
            calculatePlayerScore(gold, turns, arrows);
            return playerScore;
        }

        private int[] getScoresRaw()
        {
            strmRead = new StreamReader("HighScores.txt");
            //reads the scores and puts in array
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
                string line = strmRead.ReadLine();
                string scoreAsString = line.Split('%')[0];
                scoresRaw[i] = int.Parse(scoreAsString); 
            }
            return scoresRaw;
        }

        private Boolean isPlayerScoreHighEnough()
        {
            //returns true if the new score from this run is higher than the lowest score on the leaderboard
            if(playerScore >= scoresSorted[9])
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void lineOfLowestScore()
        {
            //sets line to which line is the lowest score that will need to be replaced
            for (int n = 0; n < 10; n++)
            {
                if(scoresRaw[n] == scoresSorted[9])
                {
                    line = n;
                    break;
                }
            }
        }

        public void modifyLeaderboard(String name, String cave, int gold, int turns, int arrows)
        {
            //will add the new highscore and remove the lowest score to the new file along with all the other scores
            //copy the modified file back to the original
            calculatePlayerScore(gold, turns, arrows);
            strmRead.BaseStream.Position = 0;
            strmRead.DiscardBufferedData();
            lineOfLowestScore();
            for (int i = 0; i < line; i++)//copy up to the line that needs to be changed
            {
                String s = strmRead.ReadLine();
                strmWrite.WriteLine(s);
            }
            strmRead.ReadLine();
            strmWrite.WriteLine(playerScore + "%" + gold + "%" +  turns + "%" + arrows);//new score added
            for (int j = 0; j < 9 - line; j++)//copy all the other scores
            {
                String t = strmRead.ReadLine();
                strmWrite.WriteLine(t);
            }

            for (int n = 0; n < line; n++)//copy names and caves up to the lowest
            {
                String p = strmRead.ReadLine();
                strmWrite.WriteLine(p);
            }
            strmRead.ReadLine();
            strmWrite.WriteLine(name + "%" + cave);//change
            for (int b = 0; b < 9 - line; b++)//copy the remaining
            {
                String g = strmRead.ReadLine();
                strmWrite.WriteLine(g);
            }
            strmWrite.Close();
            strmRead.Close();
            System.IO.File.Delete("HighScores.txt");
            File.Copy("HighScoresModified.txt", "HighScores.txt");
        }

        private void calculatePlayerScore(int gold, int turns, int arrows)
        {
            playerScore = 100 - turns + gold + (10 * arrows);
        }
    }
}
