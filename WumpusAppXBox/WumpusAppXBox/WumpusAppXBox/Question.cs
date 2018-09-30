using System;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.Generic;
using System.Linq;

namespace WumpusAppXBox
{
    class Question
    {


        /*
        Ok so some information to start this off.
        I don't know how you guys want to run this project but this question object will work as follows.
        Please know to make this work the text file should be placed two layers above the executable or in the same folder as the bin and obj folder should do.
        Likewise the file should be named Angela, if it is not you shall invoke the feirce powers of the north to come down on your behind.


       Oh and key is the parameter that needs to be called when the question object is initated. It is a random number that will act as which question out of 50 you want.


        Questiontext will be what the question is, ex. What is "2 + 2?"
    

        Possible answer choices 1 - 4
        Ans1 ex. "4"
        Ans2 ex. "7"
        Ans3 ex. "3"
        Ans4 ex. "Jelly Beans"

        The correct answer in reference of Ans1, Ans2, Ans3 and Ans4
        correctAns ex. "1"
        It WILL return as a string, below is the code to convert it into an int on your side.
        
        \\//
        int correctAnswer;
            if (Correct = "1")
            correctAnswer = 1;

            else if(Correct = "2")
            correctAnswer = 2;

            else if(Correct = "2")
            correctAnswer = 3;
        
            else
            correctAnswer = 4;
                   
      \\//

        The UI or Game object can create as many questions as they want and read from these objects as parameter    s which will also be included
        */



        // Here were declaring some variables
        int correctAnswer;
        

        public Question() 
        {
            correctAnswer = 0;
        }

        
        public Question(int key, string QuestionFile)
        {
            //This constuctor is a test to see if it would work, its a bit too bulky and i will slim it down to have less exceptions.
            string lineText = File.ReadLines(QuestionFile).ElementAtOrDefault(key - 1);
            string[] parsedText = lineText.Split(':');
            QuestionText = parsedText[0];
            answers = new string[4];
            answers[0] = parsedText[1];
            answers[1] = parsedText[2];
            answers[2] = parsedText[3];
            answers[3] = parsedText[4];
            Correct = parsedText[5];
            questionNumber = key;
        }

        public string QuestionText { get; }
        public string []  answers { get; }
        public string secret { get; }
        public string Correct;
        public int questionNumber { get; }
        public int GetCorrect()
        {
            return int.Parse(Correct);
        }
    }
}

