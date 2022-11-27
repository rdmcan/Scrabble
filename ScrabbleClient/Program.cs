using System;
using System.Collections.Generic;
using ScrabbleLibrary;
using System.Linq;



namespace ScrabbleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            IBag myBag = new Bag();

            Console.WriteLine("Test Client for: Scrabble (TM) Library, © 2022  " + myBag.About);
            Console.WriteLine("");

            Console.WriteLine("Bag initialized with the following " + myBag.TilesRemaining + " tiles...");
            Console.WriteLine(myBag);

            Console.Write("\nEnter the number of players (1-8): ");
            int numPlayers = Int32.Parse(Console.ReadLine());

            Console.WriteLine("\nRacks for " + numPlayers + " players were populated.\n");
            List<IRack> players = new List<IRack>();

            for (int i = 0; i < numPlayers; i++)
            {
                players.Add(myBag.NewRack());
            }

            Console.WriteLine("Bag now contains the following " + myBag.TilesRemaining + " tiles...");
            Console.WriteLine(myBag);

            for (int i = 0; i < numPlayers; i++)
            {
                Console.WriteLine("\n----------------------------------------------------------------------------------------------------");
                Console.WriteLine("  {0,50}", "Player " + (i + 1));
                Console.WriteLine("----------------------------------------------------------------------------------------------------");

                Console.WriteLine("Your rack contains [" + players[i] + "].");
                ConsoleKey response;
                string candidateStr = "";

                do
                {
                    Console.Write("Test a word for its points value? [y/n]: ");
                    response = Console.ReadKey(false).Key;
                    
                    if (response != ConsoleKey.Y && response != ConsoleKey.N)
                    {
                        Console.WriteLine("\nPress the correct key\n");
                        response = ConsoleKey.Y;
                    }
                    else if (response == ConsoleKey.N)
                        continue;
                    else
                    {
                        //Console.Write("\nEnter a word using the letters [" + players[i] + "]: ");
                        //candidateStr = Console.ReadLine();

                        //string rackLetters = players[i].ToString();
                        bool flag = true;

                        do
                        {
                            int counter = 0;
                            Console.Write("\nEnter a word using the letters [" + players[i] + "]: ");
                            candidateStr = Console.ReadLine();
                            foreach (var item in candidateStr)
                            {
                                if (players[i].ToString().Contains(item))
                                {
                                    counter++;
                                    if (candidateStr.Length == counter)
                                    {
                                        flag = false;
                                        break;
                                    }
                                }                                
                            }
                        } while (flag);


                        int pointsCandidate = players[i].GetPoints(candidateStr.ToLower());
                        Console.WriteLine("The word [" + candidateStr + "] is worth " + players[i].TotalPoints + " points.");

                        // valid candidate string to play
                        if (pointsCandidate > 0)
                        {
                            ConsoleKey responsePlay;
                            Console.Write("Do you want to play the word [" + candidateStr + "]? [y/n]: ");
                            responsePlay = Console.ReadKey(false).Key;

                            if (responsePlay == ConsoleKey.Y)
                                break;
                            Console.WriteLine();
                        }
                    }
                } while (response == ConsoleKey.Y);

                // Play the candidate candidateStr
                if (response != ConsoleKey.N)
                {
                    players[i].PlayWord(candidateStr.ToLower());
                    Console.WriteLine("\n\t\t------------------------------");
                    Console.WriteLine("\t\tWord Played:\t" + candidateStr);
                    Console.WriteLine("\t\tTotal Points:\t" + players[i].TotalPoints);
                    Console.WriteLine("\t\t------------------------------");
                    Console.WriteLine("Your rack contains [{0}]", players[i].ToString());
                    Console.WriteLine("Bag now contains the following " + myBag.TilesRemaining + " tiles...");
                    Console.WriteLine(myBag);
                }

            }

        }
    }
}
