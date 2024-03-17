using System;
using System.Data.SqlTypes;

namespace SPS
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string nickname = "";
            int age = 0;
            bool ready = true;

            Console.WriteLine("\t\tHello Player!");
            authorization(ref nickname,ref age);
            
            if (age > 12)
            {
                Console.Write("Are we ready to go into battle? (y - yes/other сharacter(s) - no):");
                if (Console.ReadLine() == "y")
                {
                    Console.WriteLine("To win the game, you need to win at least two rounds out of three.");
                }     
                else exit("Okay", nickname);
            }
            else
            {
                exit("Sorry but you're not old enough to play.", nickname);
            }        
            
            while (ready)
            {
                int chooseWeapon;
                

                while (true)
                {
                    Console.WriteLine("Which weapon will you choose?");
                    Console.WriteLine($"1. {(Weapon)1}");
                    Console.WriteLine($"2. {(Weapon)2}");
                    Console.WriteLine($"3. {(Weapon)3}");
                    if (!int.TryParse(Console.ReadLine(), out chooseWeapon) || chooseWeapon > 3 || chooseWeapon < 1)
                    {
                        Console.Write("Wrong input! Try again? (y - yes/other сharacter(s) - no): ");
                        if (Console.ReadLine() == "y")
                            continue;
                        else 
                        {
                            ready = false;
                            break;
                        }
                        
                    }
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
                    Console.Write("Wrong input! Try again? (y - yes/other сharacter(s) - no): ");
                    if (Console.ReadLine() == "y") continue;
                    else  exit("test");
                }

                Console.Write("Enter your age:");
                if (!int.TryParse(Console.ReadLine(), out age))
                {
                    Console.Write("Wrong input! Try again? (y - yes/other сharacter(s) - no): ");
                    if (Console.ReadLine() == "y")  continue;
                    else  exit("test", nick);
                }
                break;
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
