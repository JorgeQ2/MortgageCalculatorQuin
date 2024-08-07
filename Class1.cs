namespace MortgageCalculator
{
    public class Class1
    {
        public static void Main(string[] args)
        {
            double amountLoaned = 10000;
            double annualRate = 5;
            int numberOfMonths = 60;

            try
            {
                Mortgage mortgage = new Mortgage(amountLoaned, annualRate, numberOfMonths);
                mortgage.CalculateMonthlyPayment();
                mortgage.PrintAmortizationSchedule();
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    class Mortgage
    {
        private double AmountLoaned { get; set; }
        private double AnnualRate { get; set; }
        private int NumberOfMonths { get; set; }
        private double MonthlyRate { get; set; }
        public double TotalMonthlyPayment { get; private set; }

        public Mortgage(double amountLoaned, double annualRate, int numberOfMonths)
        {
            if (amountLoaned <= 0)
                throw new ArgumentException("Amount loaned must be greater than zero.");
            if (annualRate <= 0)
                throw new ArgumentException("Annual rate must be greater than zero.");
            if (numberOfMonths <= 0)
                throw new ArgumentException("Number of months must be greater than zero.");

            AmountLoaned = amountLoaned;
            AnnualRate = annualRate;
            NumberOfMonths = numberOfMonths;
            MonthlyRate = annualRate / 1200;
        }

        public void CalculateMonthlyPayment()
        {
            TotalMonthlyPayment = (AmountLoaned * MonthlyRate) / (1 - Math.Pow(1 + MonthlyRate, -NumberOfMonths));
            Console.WriteLine($"Total Monthly Payment: {TotalMonthlyPayment:C}");
        }

        public void PrintAmortizationSchedule()
        {
            double remainingBalance = AmountLoaned;

            for (int month = 1; month <= NumberOfMonths; month++)
            {
                double interestPayment = remainingBalance * MonthlyRate;
                double principalPayment = TotalMonthlyPayment - interestPayment;
                remainingBalance -= principalPayment;

                Console.WriteLine($"Month {month}:");
                Console.WriteLine($"  Interest Payment: {interestPayment:C}");
                Console.WriteLine($"  Principal Payment: {principalPayment:C}");
                Console.WriteLine($"  Remaining Balance: {remainingBalance:C}");
            }
        }
    }
}
