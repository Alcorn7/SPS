using System;
using System.Data.SqlTypes;
using System.Net.Http.Headers;

namespace SPS
{
    internal class Program
    {
        static Random rnd = new Random();
        static string nickname = "";
        static int age = 0;
        static int countMatch = 0;
        static int countWin = 0;
        static int countPlayRound = 0;
        static int countWinRound = 0;


        static void Main(string[] args)
        {
            Console.WriteLine("\t\tHello Player!");
            authorization(ref nickname,ref age);
            
            while (true) //Gameloop
            {
                Console.Write("Are we ready to go into battle?\n (y - yes/other сharacter(s) - no):");
                if (Console.ReadLine() == "y")
                {
                    statistics(nickname,age, countMatch, countWin);
                    Console.WriteLine("To win the game, you need to win at least two rounds out of three.");
                }     
                else exit("Okay", nickname);

                int chooseWeapon;
                while (true)
                {
                    Console.WriteLine("Which weapon will you choose?");
                    Console.WriteLine($"1. {(Weapon)1}");
                    Console.WriteLine($"2. {(Weapon)2}");
                    Console.WriteLine($"3. {(Weapon)3}");
                    if (!int.TryParse(Console.ReadLine(), out chooseWeapon) || chooseWeapon > 3 || chooseWeapon < 1)
                    {
                        Console.Write("Wrong input! Try again?\n (y - yes/other сharacter(s) - no): ");
                        if (Console.ReadLine() == "y") continue;
                        else  exit("Okay", nickname); 
                    }

                    Console.WriteLine($"--> {(BattleStatus)battle(chooseWeapon, rnd.Next(1, 4))}"); // return Draw/Win/Lose
                    
                    if (countWinRound == 2 || countPlayRound == 3)
                        break;
                }

            }




            exit("", nickname);
        }



        static void authorization(ref string  nick, ref int  age)
        {
            while (true) // Authorization
            {
                Console.Write("Enter your nickname:");
                nick = Console.ReadLine();
                if (string.IsNullOrEmpty(nick))
                {
                    Console.Write("Wrong input! Try again?\n (y - yes/other сharacter(s) - no): ");
                    if (Console.ReadLine() == "y") continue;
                    else  exit("Okay");
                }

                Console.Write("Enter your age(only 12+ can play):");
                if (!int.TryParse(Console.ReadLine(), out age))
                {
                    Console.Write("Wrong input! Try again?\n (y - yes/other сharacter(s) - no): ");
                    if (Console.ReadLine() == "y")  continue;
                    else  exit("Okay", nick);
                }

                if(age < 12)
                    exit("Sorry but you're not old enough to play.", nick);
                break;
            }
        }

        static void statistics(string nick, int age, int countMatch, int countWin)
        {
            Console.WriteLine($"Nickname: {nick}");
            Console.WriteLine($"Age: {age}");
            Console.WriteLine($"Matches played: {countMatch}");
            Console.WriteLine($"Wins: {countWin}");
        }

        static int battle(int player, int ai)
        {
            countPlayRound++;
            Console.WriteLine("Battle!");
            if (player == ai) // Draw
                return 0;
            if ((player - ai == -1) || (player - ai == 2)) // Win
            {
                countWinRound++;
                return 1;
            }  
            else return 2; //Lose

            /*
             
             1 - Rock      2 - Scissors    3 - Paper

            1 - 2 = -1 W   2 - 1 = 1  L   3 - 1 = 2 W
            1 - 3 = -2 L   2 - 3 = -1 W   3  - 2 = 1 L
  
            */
        }

        static void exit(string message, string nickname = "Player") 
        {
            Console.WriteLine(message);
            Console.WriteLine($"Goodbye {nickname}!");
            Console.ReadKey();
            Environment.Exit(0);
        }










    }
}
