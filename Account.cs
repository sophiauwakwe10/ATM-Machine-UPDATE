using System;
using System.Collections.Generic;

class Account
{
    public string AccountNumber { get; }
    public string Pin { get; }
    public decimal Balance { get; set; }
    public bool IsBlocked { get; set; }

    public Account(string accountNumber, string pin, decimal balance)
    {
        AccountNumber = accountNumber;
        Pin = pin;
        Balance = balance;
        IsBlocked = false;
    }
}

