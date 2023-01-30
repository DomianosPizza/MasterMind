using System;
using System.Collections.Generic;
using MasterMindLibrary;


namespace MasterMind
{
    class Program
    {
        static List<Peg> pegList = new List<Peg>()
        {
            new Peg(ConsoleColor.White, ConsoleColor.Red),
            new Peg(ConsoleColor.White, ConsoleColor.Blue),
            new Peg(ConsoleColor.Black, ConsoleColor.Green),
            new Peg(ConsoleColor.Black, ConsoleColor.Yellow),
            new Peg(ConsoleColor.Black, ConsoleColor.Cyan),
            new Peg(ConsoleColor.White, ConsoleColor.Magenta),
            new Peg(ConsoleColor.White, ConsoleColor.DarkGray),
            new Peg(ConsoleColor.White, ConsoleColor.DarkRed)
        };

        static void Main(string[] args)
        {
            List<Attempt> allAttempts = new List<Attempt>();
            bool gameWon = false;

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to Mastermind!");
            Console.ResetColor();

            //ask for difficulty
            Console.WriteLine("Choose a Difficulty");
            int difficulty = MMLib.GetConsoleMenu(new List<string> { "Easy: 4 Colors", "Medium: 6 Colors", "Hard: 8 Colors" });

            //ask for maxTurns of turns to guess it
            int maxTurns = MMLib.GetConsoleInt("How Many Attempts(1 - 50)", 1, 50);

            //store the maxPegs based on difficulty
            int maxPegs = (difficulty * 2) + 2;

            //Generate an answer
            List<int> answer = MMLib.GenerateAnswer(maxPegs);
            //show cheat? 
            MMLib.Cheat(answer, pegList);


            do
            {

                //  get user attempt
                Attempt attempt = GetAttemptFromUser(maxPegs, allAttempts, maxTurns);

                //  Check the attempt for a correct guess
                CheckAttempt(attempt, answer);
                //  add the attempt to the attempt list
                allAttempts.Add(attempt);
                //  determine if the game has been won or not
                if (attempt.CorrectAnswerCount == maxPegs)
                {
                    gameWon = true;
                }
                //  reduce the maxTurns
                maxTurns--;



            } while (!gameWon && maxTurns != 0);//loop while !gameWon && maxTurns != 0

            //If won, display Game Won!
            if (gameWon)
            {
                Console.WriteLine("You won");
                MMLib.ShowAnswer(answer, pegList, "0");
            }
            //If lost, show game loss
            if(!gameWon && maxTurns == 0)
            {
                Console.WriteLine("you lost");
                //show the correct answer
                MMLib.ShowAnswer(answer, pegList, "0");
            }


        }

        static Attempt GetAttemptFromUser(int maxPegs, List<Attempt> allAttempts, int maxTurns)
        {
            //Create a new Attempt
            Attempt attempt = new Attempt();

            //Get color options based on maxPegs
            MMLib.GetColorOptions(maxPegs, pegList);

            //Loop of # of pegs they need to choose
            for(int i = 0; i < maxPegs; i++)
            {

                //      clear console
                Console.Clear();
                //      Display # of attempts left
                MMLib.ShowAttempts(allAttempts, pegList, "0");
                //      Show all previous attempts
                MMLib.ShowAttempts(allAttempts, pegList, "0");
                //      Show pegs they have chosen already in this attempt
                MMLib.ShowChosenPegs(attempt, pegList);

                //      Ask them to pick a peg color from a menu of options
                int pickPeg = MMLib.GetConsoleMenu(MMLib.GetColorOptions(maxPegs, pegList))-1;
                //      Add the chosen peg to the Attempt.AttemptList
                attempt.AttemptList.Add(pickPeg);
                
            }



            //Return the attempt when done
            return attempt;
        }


        static void CheckAttempt(Attempt attempt, List<int> answer)
        {
            //Check the attempt.AttemptList to see if they got a match to the answer
            //If a peg is correct, increment the attempt.CorrectAnswerCount
            for (int i = 0; i < answer.Count; i++)
            {
                if (attempt.AttemptList[i] == answer[i]) {
                    attempt.CorrectAnswerCount++;
                }
            }
        }
    }
}
