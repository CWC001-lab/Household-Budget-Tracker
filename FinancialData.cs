using System;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.IO;

public class UserInfo
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public List<TransactionInfo> Transactions { get; set; }
    public int Budget { get; set; }
    public string BudgetName { get; set; }
}

public class TransactionInfo
{
    public int Amount { get; set; }
    public DateTime Date { get; set; }
    public string Name { get; set; }
    public bool IsIncome { get; set; }
}