using System;
using System.Data.SqlTypes;
using System.Net.Http.Headers;

namespace SPS
{
    internal class Program
    {
        static Random rnd = new Random();
        static string nickname = "";
        static string age = "";
        static int intAge = 0;
        static int countMatch = 0;
        static int countWin = 0;
        static int countPlayRound = 0;
        static int countWinOrLoss = 0;

        static string[,] ageType = { { "Cadet", "Padawan", "Capybara", "Grandpa", "Dracula", "EldenLord", "NoData" }, 
                                    {   "12",     "18",       "45",      "70",      "400",      "1000",   "10000" } };

        static string[] winPhrases = { "Someone is lucky!", "Incredibly!!!", "Divinely!!!11!!11" };
        static string[] lossPhrases = { "You'll be lucky next time.", "You can! Try again!!!", "Go look for the lucky clover." };

        static void Main(string[] args)
        {
            Console.WriteLine("\t\t\tHello Player!");
            authorization(ref nickname,ref age, ref intAge);
            
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
                Console.WriteLine($"MATCH-{countMatch + 1}");
                Console.WriteLine("===============================================================");
                int chooseWeapon;
                int chooseAi, nameAi;
                nameAi = rnd.Next(1, 4);
                
                while (true) // battle
                {

                    Console.WriteLine($"ROUND-{countPlayRound + 1}");
                    Console.WriteLine($"1. {(Weapon)1}\t\t2. {(Weapon)2}\t\t3. {(Weapon)3}");
                    Console.Write("Which weapon will you choose?");

                    if (!int.TryParse(Console.ReadLine(), out chooseWeapon) || chooseWeapon > 3 || chooseWeapon < 1)
                    {
                        Console.Write("Wrong input! Try again?\n (y - yes/other сharacter(s) - no): ");
                        if (Console.ReadLine() == "y") continue;
                        else  exit("Okay", nickname); 
                    }
                    chooseAi = rnd.Next(1, 4);
                    
                    Console.WriteLine($"--> {(BattleStatus)battle(chooseWeapon, chooseAi, nameAi)}"); // return Draw/Win/Lose
                    Console.WriteLine("--------------------------------------------------------------");

                    if (countWinOrLoss == 2) // two win 
                        {
                        phrases(BattleStatus.Win);
                        countWin++;
                        countPlayRound = 0;
                        countWinOrLoss = 0;
                        countMatch++;
                        break;
                    }
                    if (countWinOrLoss == -2) // two loss (-2)
                    {
                        phrases(BattleStatus.Loss);
                        countPlayRound = 0;
                        countWinOrLoss = 0;
                        countMatch++;
                        break;
                    }

                    if (countPlayRound == 3) 
                    {
                        switch (countWinOrLoss)
                        {
                            case 2 or 1: // win draw win(2)/ draw win win(2) / win loss win(1) / loss win win(1) / draw win draw(1) / draw draw win(1) ...
                                phrases(BattleStatus.Win);
                                countWin++;
                                break;
                            case -2 or -1:
                                phrases(BattleStatus.Loss);
                                break;
                            case 0: // draw draw draw / win lose draw / lose win draw / win draw lose / lose draw win
                                phrases(BattleStatus.Draw);
                                break;
                        }
                        countWinOrLoss = 0;
                        countPlayRound = 0;
                        countMatch++;
                        break;
                    }              
                }
            }
        }



        static void authorization(ref string  nick, ref string age, ref int intAge)
        {
            Console.WriteLine("===============================================================");
            while (true) // Authorization
            {
                Console.Write("Enter your nickname(no more 10 сharacters):");
                nick = Console.ReadLine();
                if (string.IsNullOrEmpty(nick))
                {
                    Console.Write("Wrong input! Try again?\n (y - yes/other сharacter(s) - no): ");
                    if (Console.ReadLine() == "y") continue;
                    else  exit("Okay");
                }
                if (nick.Length > 10)
                {
                    Console.Write("Wrong input! Try again?\n (y - yes/other сharacter(s) - no): ");
                    if (Console.ReadLine() == "y") continue;
                    else exit("Okay");
                }

                Console.Write("Enter your age(only 12+ can play):");
                if (!int.TryParse(Console.ReadLine(), out intAge))
                {
                    Console.Write("Wrong input! Try again?\n (y - yes/other сharacter(s) - no): ");
                    if (Console.ReadLine() == "y")  continue;
                    else  exit("Okay", nick);
                }

                if(intAge < 12)
                    exit("Sorry but you're not old enough to play.", nick);

                for(int i = 1; true; i++)
                {
                    if (int.Parse(ageType[1,i]) > intAge)
                    {
                        age = ageType[0,i];
                        break;
                    }
                    if(10000 <= intAge)
                    {
                        age = ageType[0, 6];
                        break;
                    }
                }
                break;
            }
        }

        static void statistics(string nick, string age, int countMatch, int countWin)
        {
            Console.WriteLine("===============================================================");
            Console.WriteLine($"Nickname: \t{nick}");
            Console.WriteLine($"Age: \t\t{intAge}({age})");
            Console.WriteLine($"Matches played: {countMatch}");
            Console.WriteLine($"Wins: \t\t{countWin}");
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
        static void phrases(BattleStatus winLoss)
        {
            int r = rnd.Next(0, 3);
            if (winLoss == BattleStatus.Win)
            {
                Console.WriteLine($"{nickname}, it`s  win match");
                Console.WriteLine($"{winPhrases[r]}");
            }
            if (winLoss == BattleStatus.Loss)
            {
                Console.WriteLine($"{nickname}, it`s  loss match");
                Console.WriteLine($"{lossPhrases[r]}");
            }
            if (winLoss == BattleStatus.Draw)
            {
                Console.WriteLine($"{nickname}, it`s  draw match");
                Console.WriteLine($"{lossPhrases[r]}");
            }
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
