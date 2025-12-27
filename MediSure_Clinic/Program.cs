using System;
using MediSure_Clinic;

namespace MediSure_Clinic
{
    class Program
    {
        static void Main(string[] args)
        {
            // Menu option for user
            while (true)
            {
                Console.WriteLine("\n===== MediSure Clinic Billing Menu =====");
                Console.WriteLine("1. Create Bill");
               Console.WriteLine("2. View Last Bill");
                Console.WriteLine("3. Clear Last Bill");
                Console.WriteLine("4. Exit");
                Console.Write("Enter Your Choice: ");

                string input = Console.ReadLine()!;

                switch (input)
                {
                    case "1":
                        BillingService.CreateBill();
                        break;
                    case "2":
                        BillingService.ViewLastBill();
                        break;
                    case "3":
                        BillingService.ClearLastBill();
                        break;
                    case "4":
                        Console.WriteLine("\nThank you for using MediSure Clinic Billing System!");
                        return;
                    default:
                        Console.WriteLine("\nInvalid Choice. Please enter 1-4.");
                        break;
                }
            }
        }
    }
}
