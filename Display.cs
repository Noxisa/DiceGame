using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    internal class Display
    {
        public static void ShowProbabilityTable(List<DiceSet> diceSets)
        {
            Console.WriteLine("Probability Table");
            Console.WriteLine("User Dice | Computer Dice | User Win % | Computer Win %");

            for (int i = 0; i < diceSets.Count; i++)
            {
                for (int j = 0; j < diceSets.Count; j++)
                {
                    if (i == j) continue;
                    double userWinProbability = CalculateWinProbability(diceSets[i], diceSets[j]);
                    double computerWinProbability = 100 - userWinProbability;
                    Console.WriteLine($"{GetDiceDescription(),10} | {GetDiceDescription(),13} | {userWinProbability,10:F2}% | {computerWinProbability,12:F2}%");
                }
            }
        }

        private static double CalculateWinProbability(DiceSet userDice, DiceSet computerDice)
        {
            
            int userWins = 0;
            int totalGames = userDice.Sides.Count * computerDice.Sides.Count;

            foreach (var userSide in userDice.Sides)
            {
                foreach (var computerSide in computerDice.Sides)
                {
                    if (userSide > computerSide)
                    {
                        userWins++;
                    }
                }
            }

            return (double)userWins / totalGames * 100;
        }

        private static string GetDiceDescription()
        {
            return "Dice";
        }
    }
       }