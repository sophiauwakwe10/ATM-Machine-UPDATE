using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATM_Machine_UPDATE
{
    class ATM
    {
        private Dictionary<string, Account> accounts;

        public delegate void TransactionDelegate(Account account, decimal amount);

        public ATM()
        {
            accounts = new Dictionary<string, Account>();
            accounts.Add("123456", new Account("123456", "1234", 1000));
            accounts.Add("789012", new Account("789012", "5678", 500));
        }

        public void Run()
        {
            Console.WriteLine("Welcome to the ATM!");

            while (true)
            {
                Console.WriteLine("Enter your account number:");
                string accountNumber = Console.ReadLine();

                if (accounts.ContainsKey(accountNumber))
                {
                    Account account = accounts[accountNumber];

                    if (account.IsBlocked)
                    {
                        Console.WriteLine("Your account is blocked.");
                        break;
                    }

                    Console.WriteLine("Enter your PIN:");
                    string pin = Console.ReadLine();

                    if (pin == account.Pin)
                    {
                        Console.WriteLine("Login successful!");
                        ShowOptions(account);
                    }
                    else
                    {
                        Console.WriteLine("Incorrect PIN.");
                        HandleIncorrectPin(account);
                    }
                }
                else
                {
                    Console.WriteLine("Account not found.");
                }
            }
        }

        private void ShowOptions(Account account)
        {
            Console.WriteLine("1. Check balance");
            Console.WriteLine("2. Make deposit");
            Console.WriteLine("3. Make withdrawal");
            Console.WriteLine("4. Transfer money");
            Console.WriteLine("5. Logout");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.WriteLine($"Your balance: ${account.Balance}");
                    break;
                case 2:
                    PerformTransaction(account, Deposit);
                    break;
                case 3:
                    PerformTransaction(account, Withdraw);
                    break;
                case 4:
                    PerformTransfer(account);
                    break;
                case 5:
                    Console.WriteLine("Logging out...");
                    break;
                default:
                    Console.WriteLine("Invalid option.");
                    break;
            }

            if (choice != 5)
            {
                ShowOptions(account);
            }
        }

        private void PerformTransaction(Account account, TransactionDelegate transaction)
        {
            Console.WriteLine("Enter amount:");
            decimal amount = decimal.Parse(Console.ReadLine());
            transaction(account, amount);
        }

        private void Deposit(Account account, decimal amount)
        {
            account.Balance += amount;
            Console.WriteLine("Deposit successful.");
        }

        private void Withdraw(Account account, decimal amount)
        {
            if (amount <= account.Balance)
            {
                account.Balance -= amount;
                Console.WriteLine("Withdrawal successful.");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }

        private void PerformTransfer(Account sourceAccount)
        {
            Console.WriteLine("Enter recipient account number:");
            string recipientAccountNumber = Console.ReadLine();
            if (accounts.ContainsKey(recipientAccountNumber))
            {
                Account recipientAccount = accounts[recipientAccountNumber];
                PerformTransaction(sourceAccount, (account, amount) => Transfer(account, recipientAccount, amount));
            }
            else
            {
                Console.WriteLine("Recipient account not found.");
            }
        }

        private void Transfer(Account sourceAccount, Account recipientAccount, decimal amount)
        {
            if (amount <= sourceAccount.Balance)
            {
                sourceAccount.Balance -= amount;
                recipientAccount.Balance += amount;
                Console.WriteLine("Transfer successful.");
            }
            else
            {
                Console.WriteLine("Insufficient funds.");
            }
        }

        private void HandleIncorrectPin(Account account)
        {
            int attempts = 1;
            while (attempts <= 3)
            {
                Console.WriteLine($"Attempts remaining: {4 - attempts}");
                Console.WriteLine("Enter your PIN:");
                string pin = Console.ReadLine();
                if (pin == account.Pin)
                {
                    Console.WriteLine("Login successful!");
                    ShowOptions(account);
                    return;
                }
                else
                {
                    attempts++;
                }
            }
            Console.WriteLine("Too many incorrect attempts. Your account is blocked.");
            account.IsBlocked = true;
        }
    }
}

    
