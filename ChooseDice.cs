using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using $safeprojectname$;

namespace $safeprojectname$
{
    internal class ChooseDice
    {
        private int computerDiceIndex = -1;
        private int userDiceIndex = -1;
        private static readonly System.Random fairRandom = new System.Random();
        private List<DiceSet> diceSets;

        public ChooseDice(bool userGoesFirst, List<DiceSet> diceSets) 
        {
            this.diceSets = diceSets; 

            if (userGoesFirst)
            {
                Console.WriteLine("Choose your dice:");
                DisplayDiceOptions();
                userDiceIndex = GetUserDiceChoice();

                computerDiceIndex = GetRemainingDiceIndex(userDiceIndex);
                Console.WriteLine($"I choose the remaining dice set: {GetDiceDescription(computerDiceIndex)}.");
            }
            else
            {
                computerDiceIndex = fairRandom.Next(0, diceSets.Count); 
                Console.WriteLine($"I choose the remaining dice set: {GetDiceDescription(computerDiceIndex)}.");

                Console.WriteLine("Choose your dice:");
                DisplayDiceOptions();
                userDiceIndex = GetUserDiceChoice(exclude: computerDiceIndex);
            }
            Console.WriteLine($"You choose: {GetDiceDescription(userDiceIndex)}.");
        }

        private void DisplayDiceOptions()
        {
            for (int i = 0; i < diceSets.Count; i++)
            {
                Console.WriteLine($"{i + 1}: {GetDiceDescription(i)}");
            }
        }

        private int GetUserDiceChoice(int exclude = -1)
        {
            int choice;
            do
            {
                Console.Write("Your selection: ");
                if (int.TryParse(Console.ReadLine(), out choice) &&
                    choice >= 0 && choice < diceSets.Count && choice != exclude)
                {
                    return choice;
                }
                Console.WriteLine("Invalid choice. Please try again.");
            } while (true);
        }

        private int GetRemainingDiceIndex(int chosenIndex)
        {
            for (int i = 0; i < diceSets.Count; i++)
            {
                if (i != chosenIndex)
                {
                    return i;
                }
            }
            return -1;
        }

        private string GetDiceDescription(int index)
        {
            return string.Join(", ", diceSets[index].Sides);
        }
    }

    internal class DiceSet
    {
        public List<int> Sides { get; set; } = new List<int>();
    }
}