using System;

namespace QuickMartApp
{
    public static class TransactionManager
    {
        public static SaleTransaction LastTransaction;
        public static bool HasLastTransaction = false;

        // Creating a new transcation
        public static void CreateNewTransaction()
        {
            Console.Write("Enter Invoice No: ");
            string invoiceNo = Console.ReadLine();

            // If invoice is not valid , then exit program
            if (string.IsNullOrWhiteSpace(invoiceNo))
            {
                Console.WriteLine("Invoice No cannot be empty.");
                return;
            }

            Console.Write("Enter Customer Name: ");
            string customer = Console.ReadLine();

            Console.Write("Enter Item Name: ");
            string item = Console.ReadLine();

            Console.Write("Enter Quantity: ");
            if (!int.TryParse(Console.ReadLine(), out int qty) || qty <= 0)
            {
                Console.WriteLine("Quantity must be greater than 0.");
                return;
            }

            Console.Write("Enter Purchase Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal purchase) || purchase <= 0)
            {
                Console.WriteLine("Purchase Amount must be greater than 0.");
                return;
            }

            Console.Write("Enter Selling Amount (total): ");
            if (!decimal.TryParse(Console.ReadLine(), out decimal selling) || selling < 0)
            {
                Console.WriteLine("Selling Amount must be >= 0.");
                return;
            }

            LastTransaction = new SaleTransaction()
            {
                InvoiceNo = invoiceNo,
                CustomerName = customer,
                ItemName = item,
                Quantity = qty,
                PurchaseAmount = purchase,
                SellingAmount = selling
            };

            HasLastTransaction = true;
            CalculateProfitLoss();
            Console.WriteLine("\nTransaction saved successfully.");
            PrintCalculationResult();
        }

        // Viewing a last transcation
        public static void ViewLastTransaction()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            Console.WriteLine("\n-------------- Last Transaction --------------");
            Console.WriteLine($"InvoiceNo: {LastTransaction.InvoiceNo}");
            Console.WriteLine($"Customer: {LastTransaction.CustomerName}");
            Console.WriteLine($"Item: {LastTransaction.ItemName}");
            Console.WriteLine($"Quantity: {LastTransaction.Quantity}");
            Console.WriteLine($"Purchase Amount: {LastTransaction.PurchaseAmount:F2}");
            Console.WriteLine($"Selling Amount: {LastTransaction.SellingAmount:F2}");
            Console.WriteLine($"Status: {LastTransaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {LastTransaction.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {LastTransaction.ProfitMarginPercent:F2}%");
            Console.WriteLine("--------------------------------------------");
        }

        // Calculation of profit or loss
        public static void CalculateProfitLoss()
        {
            if (!HasLastTransaction)
            {
                Console.WriteLine("No transaction available. Please create a new transaction first.");
                return;
            }

            decimal purchase = LastTransaction.PurchaseAmount;
            decimal selling = LastTransaction.SellingAmount;

            if (selling > purchase)
            {
                LastTransaction.ProfitOrLossStatus = "PROFIT";
                LastTransaction.ProfitOrLossAmount = selling - purchase;
            }
            else if (selling < purchase)
            {
                LastTransaction.ProfitOrLossStatus = "LOSS";
                LastTransaction.ProfitOrLossAmount = purchase - selling;
            }
            else
            {
                LastTransaction.ProfitOrLossStatus = "BREAK-EVEN";
                LastTransaction.ProfitOrLossAmount = 0;
            }

            LastTransaction.ProfitMarginPercent = (purchase > 0)
                ? (LastTransaction.ProfitOrLossAmount / purchase) * 100
                : 0;
        }

        // Printing of all Result
        public static void PrintCalculationResult()
        {
            Console.WriteLine($"Status: {LastTransaction.ProfitOrLossStatus}");
            Console.WriteLine($"Profit/Loss Amount: {LastTransaction.ProfitOrLossAmount:F2}");
            Console.WriteLine($"Profit Margin (%): {LastTransaction.ProfitMarginPercent:F2}%");
            Console.WriteLine("------------------------------------------------------");
        }
    }
}
