using System;

namespace SPS
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string nickname;
            int age = 0;


            Console.WriteLine("\t\tHello Player!");

            while (true) // Authorization
            {
                Console.Write("Enter your nickname:");
                nickname = Console.ReadLine();
                if (string.IsNullOrEmpty(nickname))
                {
                    Console.WriteLine("Wrong input! Try again? (y - yes/other сharacter(s) - no)");
                    if (Console.ReadLine() == "y")
                        continue;
                    else break;
                }
            
                Console.Write("Enter your age:");
                if (!int.TryParse(Console.ReadLine(), out age))
                {
                    Console.WriteLine("Wrong input! Try again? (y - yes/other сharacter(s) - no)");
                    if (Console.ReadLine() == "y")
                        continue;
                    else break;
                }
                break;
            }


            Console.WriteLine(nickname);
            Console.WriteLine(age.ToString());

            Console.WriteLine("--------------------------------------------------------------");
            Console.WriteLine("Goodbye!");
            Console.ReadKey();
        }
    }
}
