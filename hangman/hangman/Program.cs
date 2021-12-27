using System;
using System.Collections.Generic;
using System.IO;

namespace hangman
{
    class Program
    {
        static string correctWord ;
        static char[] letters;
        static Player player;
        static void Main(string[] args)
        {
            StartGame();
            PlayGame();
            EndGame();
        }

        private static void StartGame()
        {
            var words = File.ReadAllLines(@"C:\Users\mbelhadj\source\repos\hangman\words.txt");

            Random random = new Random();
            correctWord = words[random.Next(0,words.Length)];

            letters = new char[correctWord.Length];
            for(int i = 0; i<correctWord.Length; i++)
            {
                letters[i] = "-"[0];
            }
            AskForUserName();
        }

        static void AskForUserName()
        {
            Console.WriteLine("Enter your name:");
            string input = Console.ReadLine();
            if (input.Length > 1)
            {
                //valid name
                player = new Player(input);
            }
            else
            {
                //invalid named
                AskForUserName();
            }
        }


        private static void PlayGame()
        {
            do
            {
                Console.Clear();
                DisplayMaskedWord();
                char guessedLetter = AskingForLetter();
                CheckedLetter(guessedLetter);
            } while (correctWord != new string(letters));
            Console.Clear();
        }

        private static void CheckedLetter(char guessedLetter)
        {
            for (int i = 0; i < correctWord.Length; i++)
            {
                if (guessedLetter == correctWord[i])
                {
                    letters[i] = guessedLetter;
                    player.Score++;
                }
            }
        }

        static void DisplayMaskedWord()
        {
            foreach (char c in letters)
            {
                Console.Write(c);
            }
            Console.WriteLine();
                
        }

        static char AskingForLetter()
        {
            string input;
            do
            {
                Console.WriteLine("Enter a letter");
                input = Console.ReadLine();
                
            } while (input.Length!=1);
            if(!player.GuessedLetters.Contains(input[0]))
                player.GuessedLetters.Add(input[0]);
            return input[0];
        }

        private static void EndGame()
        {
            Console.WriteLine("Game over...");
            Console.WriteLine($"Thanks for playing {player.Name}");
            Console.WriteLine($"Guesses: {player.GuessedLetters.Count} Score: {player.Score}");
        }

    }
}
