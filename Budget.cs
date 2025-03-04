using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;
using static DashboardForm;

public class BudgetForm : Form
{
    private TextBox budgetNameTextBox;
    private TextBox budgetAmountTextBox;
    private Button saveBudgetButton;
    private string email;
    private readonly DataChangedEventHandler dataChangedCallback;

    public BudgetForm(string email, DataChangedEventHandler callback)
    {
        this.email = email;
        this.dataChangedCallback = callback;
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        this.Text = "Budget";
        this.Size = new System.Drawing.Size(400, 250);
        this.StartPosition = FormStartPosition.CenterScreen;
        this.BackColor = Color.White;

        // Title Label
        Label titleLabel = new Label
        {
            Text = "Set Your Budget",
            Font = new Font("Arial", 18, FontStyle.Bold),
            ForeColor = Color.FromArgb(56, 151, 240),
            Location = new System.Drawing.Point(20, 20),
            AutoSize = true
        };

        // Budget Name Label and TextBox
        Label budgetNameLabel = new Label
        {
            Text = "Budget Name:",
            Font = new Font("Arial", 12, FontStyle.Regular),
            Location = new System.Drawing.Point(20, 70),
            AutoSize = true
        };

        budgetNameTextBox = new TextBox
        {
            Location = new System.Drawing.Point(150, 70),
            Size = new System.Drawing.Size(220, 30),
            Font = new Font("Arial", 12)
        };

        // Budget Amount Label and TextBox
        Label budgetAmountLabel = new Label
        {
            Text = "Amount:",
            Font = new Font("Arial", 12, FontStyle.Regular),
            Location = new System.Drawing.Point(20, 120),
            AutoSize = true
        };

        budgetAmountTextBox = new TextBox
        {
            Location = new System.Drawing.Point(150, 120),
            Size = new System.Drawing.Size(220, 30),
            Font = new Font("Arial", 12)
        };

        // Save Budget Button
        saveBudgetButton = new Button
        {
            Text = "Save Budget",
            Font = new Font("Arial", 12, FontStyle.Bold),
            BackColor = Color.FromArgb(76, 175, 80),
            ForeColor = Color.White,
            Size = new System.Drawing.Size(200, 40),
            Location = new System.Drawing.Point((this.Width - 200) / 2, 180),
            FlatStyle = FlatStyle.Flat
        };
        saveBudgetButton.Click += (sender, e) =>
        {
            SaveBudget(budgetNameTextBox.Text, budgetAmountTextBox.Text, email);
        };

        // Add Controls to Form
        this.Controls.Add(titleLabel);
        this.Controls.Add(budgetNameLabel);
        this.Controls.Add(budgetNameTextBox);
        this.Controls.Add(budgetAmountLabel);
        this.Controls.Add(budgetAmountTextBox);
        this.Controls.Add(saveBudgetButton);
    }

    private void SaveBudget(string budgetName, string budgetAmount, string email)
    {
        if (string.IsNullOrWhiteSpace(budgetName) || string.IsNullOrWhiteSpace(budgetAmount) || !int.TryParse(budgetAmount, out int parsedBudget) || parsedBudget <= 0)
        {
            MessageBox.Show("Invalid input. Please enter a valid budget name and a positive number for the amount.");
            return;
        }

        string filePath = "./database.json";

        try
        {
            string jsonData = File.ReadAllText(filePath);
            List<UserInfo> users = JsonConvert.DeserializeObject<List<UserInfo>>(jsonData) ?? new List<UserInfo>();

            var user = users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                user.Budget = parsedBudget;
                user.BudgetName = budgetName;
            }
            else
            {
                MessageBox.Show("User not found.");
            }

            string updatedJsonData = JsonConvert.SerializeObject(users, Formatting.Indented);
            File.WriteAllText(filePath, updatedJsonData);

            dataChangedCallback?.Invoke(this, EventArgs.Empty);
            MessageBox.Show("Budget saved!");
            this.Close();
        }
        catch (Exception ex)
        {
            MessageBox.Show("An error occurred: " + ex.Message);
        }
    }
}
