namespace MortgageCalculator
{
    public class CalculatorEquation
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

    public class Mortgage
    {
        private decimal loanAmount;
        private decimal interestRate;
        private int loanTerm;

        private double AmountLoaned { get; set; }
        private double AnnualRate { get; set; }
        private int NumberOfMonths { get; set; }
        private double MonthlyRate { get; set; }
        private double TotalMonthlyPayment { get; set; }

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

        

        public double CalculateMonthlyPayment()
        {
            TotalMonthlyPayment = (AmountLoaned * MonthlyRate) / (1 - Math.Pow(1 + MonthlyRate, -NumberOfMonths));
            
            return TotalMonthlyPayment;
        }

        public List<Payment> PrintAmortizationSchedule()
        {
            
            CalculateMonthlyPayment();
            double remainingBalance = AmountLoaned;
            var result = new List<Payment>();
            for (int month = 1; month <= NumberOfMonths; month++)
            {
                double interestPayment = remainingBalance * MonthlyRate;
                double principalPayment = TotalMonthlyPayment - interestPayment;
                remainingBalance -= principalPayment;
                result.Add(new Payment(month, interestPayment, principalPayment, remainingBalance));
                
            }
            return result;
        }
    }
    public class  Payment
    {
        public Payment(int month, double interestPayment, double principalPayment, double remainingBalance)
        {
            Month = month;
            InterestPayment = interestPayment;
            PrincipalPayment = principalPayment;
            RemainingBalance = remainingBalance;
        }

        public int Month { get; }
        public double InterestPayment { get; }
        public double PrincipalPayment { get; }
        public double RemainingBalance { get; }
    }
}
