using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace $safeprojectname$
{
    internal class DiseGame
    {
        private readonly HMACGenerator hmacGenerator;
        private readonly System.Random fairRandom;

        private readonly List<List<int>> diceOptions = new()
        {
            new List<int> { 2, 2, 4, 9, 9 },
            new List<int> { 7, 5, 3, 7, 5, 3 },
            new List<int> { 1, 1, 8, 6, 8, 6 }
        };

        public DiseGame()
        {
            hmacGenerator = new HMACGenerator();
            fairRandom = new System.Random();
        }

        public void Play()
        {
            Console.WriteLine("Let's determine who makes the first move.");
            int computerChoice = fairRandom.Next(0, 2);
            string hmac = hmacGenerator.ComputeHMAC(computerChoice);
            Console.WriteLine($"I selected a random value in the range 0..1 (HMAC={hmac}).");
            Console.WriteLine("Try to guess my selection:");
            Console.WriteLine("0 = 0\n1 = 1\nX = exit\n? = help");

            string? userInput = Console.ReadLine();
            if (userInput == "0" || userInput == "1")
            {
                int userChoice = int.Parse(userInput);
                Console.WriteLine($"Your selection: {userChoice}");
                Console.WriteLine($"My selection: {computerChoice} (KEY={hmacGenerator.RevealKey()}).");

                if (userChoice == computerChoice)
                {
                    Console.WriteLine("You guessed it! You go first.");
                    RunGame(playerFirst: true);
                }
                else
                {
                    Console.WriteLine("I go first.");
                    RunGame(playerFirst: false);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Exiting game.");
            }
        }

        private void RunGame(bool playerFirst)
        {
            int playerScore = 0;
            int computerScore = 0;

            for (int round = 1; round <= 3; round++) // Gra na 3 rundy
            {
                Console.WriteLine($"\n--- Round {round} ---");
                if (playerFirst)
                {
                    Console.WriteLine("Your turn:");
                    int playerRoll = UserTurn();
                    Console.WriteLine("Computer's turn:");
                    int computerRoll = ComputerTurn();

                    if (playerRoll > computerRoll) playerScore++;
                    else if (computerRoll > playerRoll) computerScore++;
                }
                else
                {
                    Console.WriteLine("Computer's turn:");
                    int computerRoll = ComputerTurn();
                    Console.WriteLine("Your turn:");
                    int playerRoll = UserTurn();

                    if (playerRoll > computerRoll) playerScore++;
                    else if (computerRoll > playerRoll) computerScore++;
                }

                Console.WriteLine($"Scores after Round {round}: You: {playerScore}, Computer: {computerScore}");
            }

            
            Console.WriteLine("\n--- Game Over ---");
            if (playerScore > computerScore) Console.WriteLine("Congratulations, you win!");
            else if (computerScore > playerScore) Console.WriteLine("Computer wins. Better luck next time!");
            else Console.WriteLine("It's a draw!");
        }

        private int UserTurn()
        {
            Console.WriteLine("Choose your dice:");
            for (int i = 0; i < diceOptions.Count; i++)
            {
                Console.WriteLine($"{i} - [{string.Join(",", diceOptions[i])}]");
            }

            string? userInput = Console.ReadLine();
            if (int.TryParse(userInput, out int userChoice) && userChoice >= 0 && userChoice < diceOptions.Count)
            {
                var userDice = new DiceSet(diceOptions[userChoice]);
                Console.WriteLine($"You chose the dice: [{string.Join(",", userDice.Values)}]");
                int roll = RollDice(userDice);
                Console.WriteLine($"Your roll: {roll}");
                return roll;
            }
            else
            {
                Console.WriteLine("Invalid choice. Rolling a default dice...");
                var defaultDice = new DiceSet(diceOptions[0]);
                int roll = RollDice(defaultDice);
                Console.WriteLine($"Your roll: {roll}");
                return roll;
            }
        }

        private int ComputerTurn()
        {
            int choice = fairRandom.Next(0, diceOptions.Count);
            var computerDice = new DiceSet(diceOptions[choice]);
            Console.WriteLine($"I choose the dice: [{string.Join(",", computerDice.Values)}]");
            int roll = RollDice(computerDice);
            Console.WriteLine($"My roll: {roll}");
            return roll;
        }

        private int RollDice(DiceSet diceSet)
        {
            int randomIndex = fairRandom.Next(0, diceSet.Values.Count);
            return diceSet.Values[randomIndex];
        }

        public static void Main(string[] args)
        {
            DiseGame game = new();
            game.Play();
        }
    }

    internal class DiceSet
    {
        public List<int> Values { get; }

        public DiceSet(List<int> values)
        {
            Values = values;
        }
    }

    internal class HMACGenerator
    {
        private readonly byte[] key;

        public HMACGenerator()
        {
            key = RandomNumberGenerator.GetBytes(32);
        }

        public string ComputeHMAC(int value)
        {
            using (var hmac = new HMACSHA256(key))
            {
                byte[] valueBytes = System.Text.Encoding.UTF8.GetBytes(value.ToString());
                byte[] hashBytes = hmac.ComputeHash(valueBytes);
                return BitConverter.ToString(hashBytes).Replace("-", "");
            }
        }

        public string RevealKey()
        {
            return BitConverter.ToString(key).Replace("-", "");
        }
    }
}
