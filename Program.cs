using System.Xml.Linq;

namespace Bank_System_Task
{
    public  class Account
    {
        public Account(string name="Unknown", double balance=9000)
        {
            Name = name;
            Balance = balance;
        }

        public string Name { get; set; }
        public double Balance { get; set; }
        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount > 0)
            {
                Balance -= (amount);
                return true;
            }
            return false;
        }
        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return $"Name : {Name} , Balance : {Balance}";
        }
    }
    public static class AccountUtil
    { 
        public static void Display(List<Account> accounts)
        {
            Console.WriteLine("Accounts");
            foreach (Account account in accounts)
            {
                Console.WriteLine(account);
            }
        }
        public static void Withdraw(List<Account> accounts,double amount)
        {

            foreach (var item in accounts)
            {
                if (item.Withdraw(amount))
                {
                    Console.WriteLine($"Successfull Withdraw for {item.Name}");
                }
                else
                {
                    Console.WriteLine($"Failed Withdrawfor {item.Name}");
                }
            }
        }
        public static void Deposit(List<Account> accounts, double amount)
        {

            foreach (var item in accounts)
            {
                if (item.Deposit(amount))
                {
                    Console.WriteLine($"Successfull Deposit for account {item.Name}");
                }
                else
                {
                    Console.WriteLine($"Failed Deposit for account {item.Name}");
                }
            }
        }
    }
    public class SavingAccount:Account
    {
       
        public double interestRate { get; set; }
        public SavingAccount(string name="Unknown", double balance=9000,double interestrate=0.2) : base(name, balance)
        {
            this.interestRate= interestrate;
        }

       
        public override bool Withdraw(double amount)
        {
            if (Balance - amount > 0)
            {
                Balance-=(amount+interestRate);
                return true;
            }
            return false;
        }
        public override bool Deposit(double amount)
        {
            if (amount>0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }
    }
    public class CheckingAccount : Account
    {
        public double Fee { get; set; }
        public CheckingAccount(string name= "Unknown", double balance=5000,double fee = 1.5) : base(name, balance)
        {
            this.Fee = fee;
        }   
        public override bool Withdraw(double amount)
        {
            if (Balance - amount > 0)
            {
                Balance -= (amount + Fee);
                return true;
            }
            return false;
        }
        public override bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }
    }
    public class TrustAccount : SavingAccount
    {
        private int counter=0;
        public TrustAccount(string name="Unknown", double balance = 5000, double interestrate = 1.5) : base(name, balance, interestrate)
        {
          
        }
        public override bool Withdraw(double amount)
        {
        
            counter++;
            if (Balance - amount > 0&&amount<=Balance*.2&&counter<=3)
            {
                Balance -= (amount + interestRate);
   
                return true;
            }
            return false;
        }
        public override bool Deposit(double amount)
        {
            if (amount > 0&&amount>=5000)
            {
                Balance += amount+50;
                return true;
            }
            else if (amount>0&&amount<5000)
            {
                Balance += amount + 50;
                return true;
            }
            return false;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {

            var accounts = new List<Account>();
            accounts.Add(new Account());
            accounts.Add(new Account("Larry"));
            accounts.Add(new Account("Moe", 2000));
            accounts.Add(new Account("Curly", 5000));

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 5000);


            // //Savings
            List<Account> Account = new List<Account>();
            Account.Add(new SavingAccount());
            Account.Add(new SavingAccount("Superman"));
            Account.Add(new SavingAccount("Batman", 2000));
            Account.Add(new SavingAccount("Wonderwoman", 5000, 5.0));

            AccountUtil.Display(Account);
            AccountUtil.Deposit(Account, 1000);
            AccountUtil.Withdraw(Account, 2000);
            AccountUtil.Display(Account);
            //Checking
            var checAccounts = new List<Account>();
            checAccounts.Add(new CheckingAccount());
            checAccounts.Add(new CheckingAccount("Larry2"));
            checAccounts.Add(new CheckingAccount("Moe2", 2000));
            checAccounts.Add(new CheckingAccount("Curly2", 5000));

            AccountUtil.Display(checAccounts);
            AccountUtil.Deposit(checAccounts, 1000);
            AccountUtil.Withdraw(checAccounts, 2000);
            AccountUtil.Withdraw(checAccounts, 2000);

            // Trust
            var trustAccounts = new List<Account>();
            trustAccounts.Add(new TrustAccount());
            trustAccounts.Add(new TrustAccount("Superman2"));
            trustAccounts.Add(new TrustAccount("Batman2", 2000));
            trustAccounts.Add(new TrustAccount("Wonderwoman2", 5000, 5.0));
            AccountUtil.Display(trustAccounts);
            AccountUtil.Deposit(trustAccounts, 1000);
            AccountUtil.Deposit(trustAccounts, 6000);
            AccountUtil.Withdraw(trustAccounts, 2000);
            AccountUtil.Withdraw(trustAccounts, 3000);
            AccountUtil.Withdraw(trustAccounts, 500);
            AccountUtil.Display(trustAccounts);


            Console.ReadKey();
        }
    }
}
