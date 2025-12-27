using System;

namespace QuickMartApp
{
    class Program
    {
        static void Main(string[] args)
        {

            // Menu for USER 
            while (true)
            {
                Console.WriteLine("\n================== QuickMart Traders ==================");
                Console.WriteLine("1. Create New Transaction");
                Console.WriteLine("2. View Last Transaction");
                Console.WriteLine("3. Calculate Profit/Loss (Recompute & Print)");
                Console.WriteLine("4. Exit");
                Console.Write("Enter your option: ");

                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        TransactionManager.CreateNewTransaction();
                        break;

                    case "2":
                        TransactionManager.ViewLastTransaction();
                        break;

                    case "3":
                        TransactionManager.CalculateProfitLoss();
                        if (TransactionManager.HasLastTransaction)
                            TransactionManager.PrintCalculationResult();
                        break;

                    case "4":
                        Console.WriteLine("Thank you. Application closed normally.");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }
    }
}
