using System;
using MediSure_Clinic;

namespace MediSure_Clinic
{
    public static class BillingService
    {
        private static PatientBill LastBill;
        private static bool HasLastBill = false;

        // Creating Bill by getting input from user
        public static void CreateBill()
        {
            Console.Write("\nEnter Bill Id: ");
            string billId = Console.ReadLine()!;
            if (string.IsNullOrWhiteSpace(billId))
            {
                Console.WriteLine("Bill Id cannot be empty.");
                return;
            }

            Console.Write("Enter Patient Name: ");
            string patientName = Console.ReadLine()!;

            // check whether user have insureance or not
            bool hasInsurance = false;
            while (true)
            {
                Console.Write("Is the patient insured? (Y/N): ");
                string ins = Console.ReadLine()!;

                if (ins == "Y" || ins == "y")
                {
                    hasInsurance = true;
                    break;
                }
                if (ins == "N" || ins == "n")
                {
                    hasInsurance = false;
                    break;
                }

                Console.WriteLine("Invalid input. Enter Y or N.");
            }

            Console.Write("Enter Consultation Fee (>0): ");
            decimal consultationFee = decimal.Parse(Console.ReadLine()!);
            if (consultationFee <= 0)
            {
                Console.WriteLine("Consultation Fee must be > 0.");
                return;
            }

            Console.Write("Enter Lab Charges (>=0): ");
            decimal labCharges = decimal.Parse(Console.ReadLine()!);
            if (labCharges < 0)
            {
                Console.WriteLine("Value cannot be negative.");
                return;
            }

            Console.Write("Enter Medicine Charges (>=0): ");
            decimal medicineCharges = decimal.Parse(Console.ReadLine()!);
            if (medicineCharges < 0)
            {
                Console.WriteLine("Value cannot be negative.");
                return;
            }

            PatientBill bill = new PatientBill()
            {
                BillId = billId,
                PatientName = patientName,
                HasInsurance = hasInsurance,
                ConsultationFee = consultationFee,
                LabCharges = labCharges,
                MedicineCharges = medicineCharges
            };

            // Calculation of bills and discounts
            bill.GrossAmount = bill.ConsultationFee + bill.LabCharges + bill.MedicineCharges;
            bill.DiscountAmount = hasInsurance ? bill.GrossAmount * 0.10m : 0m;
            bill.FinalPayable = bill.GrossAmount - bill.DiscountAmount;

            LastBill = bill;
            HasLastBill = true;

            Console.WriteLine("\nBill created successfully.");
            Console.WriteLine($"Gross Amount: {bill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {bill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {bill.FinalPayable:F2}");
            Console.WriteLine("------------------------------------------------------------");
        }

        public static void ViewLastBill()
        {
            if (!HasLastBill)
            {
                Console.WriteLine("\nNo bill available. Please create a new bill first.");
                return;
            }

            // Output of last bill
            Console.WriteLine("\n----------- Last Bill -----------");
            Console.WriteLine($"BillId: {LastBill.BillId}");
            Console.WriteLine($"Patient: {LastBill.PatientName}");
            Console.WriteLine($"Insured: {(LastBill.HasInsurance ? "Yes" : "No")}");
            Console.WriteLine($"Consultation Fee: {LastBill.ConsultationFee:F2}");
            Console.WriteLine($"Lab Charges: {LastBill.LabCharges:F2}");
            Console.WriteLine($"Medicine Charges: {LastBill.MedicineCharges:F2}");
            Console.WriteLine($"Gross Amount: {LastBill.GrossAmount:F2}");
            Console.WriteLine($"Discount Amount: {LastBill.DiscountAmount:F2}");
            Console.WriteLine($"Final Payable: {LastBill.FinalPayable:F2}");
            Console.WriteLine("--------------------------------");
        }

        // Clear last bill
        public static void ClearLastBill()
        {
            LastBill = new PatientBill();
            HasLastBill = false;
            Console.WriteLine("\nLast bill cleared.");
        }
    }
}
