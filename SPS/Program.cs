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
        static int countWinOrLoss = 0;


        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\tHello Player!");
            authorization(ref nickname,ref age);
            
            while (true) //Gameloop
            {
                
                statistics(nickname, age, countMatch, countWin);
                Console.Write("Are we ready to go into battle?\n (y - yes/other сharacter(s) - no):");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("**To win the game, you need to win at least\n two rounds out of three.**");
                }     
                else exit("Okay", nickname);
                Console.Clear();
                int chooseWeapon;
                int chooseAi, nameAi;
                nameAi = rnd.Next(1, 4);
                Console.WriteLine("===============================================================");
                while (true) // battle
                {
                    Console.WriteLine("Which weapon will you choose?");
                    Console.WriteLine($"1. {(Weapon)1}\t\t2. {(Weapon)2}\t\t3. {(Weapon)3}");

                    if (!int.TryParse(Console.ReadLine(), out chooseWeapon) || chooseWeapon > 3 || chooseWeapon < 1)
                    {
                        Console.Write("Wrong input! Try again?\n (y - yes/other сharacter(s) - no): ");
                        if (Console.ReadLine() == "y") continue;
                        else  exit("Okay", nickname); 
                    }
                    chooseAi = rnd.Next(1, 4);
                    
                    Console.WriteLine($"--> {(BattleStatus)battle(chooseWeapon, chooseAi, nameAi)}"); // return Draw/Win/Lose
                    Console.WriteLine("--------------------------------------------------------------");
                    if (countPlayRound == 2 && countWinOrLoss % 2 == 0 && countWinOrLoss != 0) 
                    {
                        if (countWinOrLoss == 2) // two win 
                        {
                            Console.WriteLine($"{nickname}, it`s  win match");
                            countWin++;
                        }
                        else // two loss (-2)
                            Console.WriteLine($"{nickname}, it`s  loss match");
                        countPlayRound = 0;
                        countMatch++;
                        break;
                    } // two win or two loss 

                    if (countPlayRound == 3) 
                    {
                        switch (countWinOrLoss)
                        {
                            case 2 or 1: // win draw win(2)/ draw win win(2) / win loss win(1) / loss win win(1) / draw win draw(1) / draw draw win(1) ...
                                Console.WriteLine($"{nickname}, it`s  win match");
                                countWin++;
                                break;
                            case -2 or -1:
                                Console.WriteLine($"{nickname}, it`s  loss match");
                                break;
                            case 0: // draw draw draw / win lose draw / lose win draw / win draw lose / lose draw win
                                Console.WriteLine($"{nickname}, it`s  draw match");
                                break;
                        }

                        countPlayRound = 0;
                        countMatch++;
                        break;
                    }              
                }

            }




            exit("", nickname);
        }



        static void authorization(ref string  nick, ref int  age)
        {
            Console.WriteLine("===============================================================");
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
            Console.WriteLine("===============================================================");
            Console.WriteLine($"Nickname: {nick} \tAge: {age} \tMatches played: {countMatch}\tWins: {countWin}");
            Console.WriteLine("===============================================================");
        }

        static int battle(int player, int ai, int nameAi)
        {
            countPlayRound++;
            Console.WriteLine($" {(Weapon)player} ({nickname}) vs {(Weapon)ai} ({(NameAi)nameAi})");
            
            if (player == ai) // Draw
            { 
                return 0; 
            }
            if ((player - ai == -1) || (player - ai == 2)) // Win
            {
                countWinOrLoss++;
                return 1;
            }
            else //Loss
            {
                countWinOrLoss--;
                return 2; 
            } 

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
