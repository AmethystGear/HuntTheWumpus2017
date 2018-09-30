using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WumpusAppXBox
{
    class Trivia
    {
        string questionFile = "Angela.txt";
        bool[] questionsAsked;
        bool[] secretsGiven;
        Random rnd = new Random();
        int numQuestions = 0;
        int numQuestionsAsked = 0;
        int numSecretsGiven = 0;

        //constructor
        public Trivia()
        {
            string [] filetext = File.ReadAllLines(questionFile);
            numQuestions = filetext.Length;
            questionsAsked = new bool[numQuestions];
            secretsGiven = new bool[numQuestions];
            for (int i = 0; i < numQuestions; i++)
            {
                questionsAsked[i] = false;
                secretsGiven[i] = false;
            }
        }

        public Question getQuestion()
        { 
            return new Question(getQuestionKey(), questionFile);
        }

        public Secret getSecret()
        {
            int key = getSecretKey();
            if(key != -1)
            {
                return new Secret(key, questionFile);
            }
            return null;            
        }

        int getSecretKey()
        {
            int possibleSecret = rnd.Next(1, numQuestions);

            if (numSecretsGiven >= numQuestions)
            {
                return -1;
            }

            if (secretsGiven[possibleSecret] == false)
            {
                numSecretsGiven++;
                secretsGiven[possibleSecret] = true;

                return possibleSecret;

            }
            else
            {
                return getSecretKey();
            }
        }
        int numChecks = 0;
        int getQuestionKey()
        {
            
            int PossibleQuestion = rnd.Next(1,numQuestions);

            if (numQuestionsAsked + 1 >= numQuestions)
            {
                numQuestionsAsked = 0;
                for (int i = 0; i < numQuestions; i++)
                {
                    questionsAsked[i] = false;
                }
            }

            if (questionsAsked[PossibleQuestion] == false)
            {
                numChecks = 0;
                numQuestionsAsked++;
                questionsAsked[PossibleQuestion] = true;
               
                return PossibleQuestion;
                           
            }
            else
            {
                numChecks++;
                return getQuestionKey();
            }

        }
    }
}
