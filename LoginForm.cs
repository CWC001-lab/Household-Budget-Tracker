using System;
using System.Drawing;
using System.Windows.Forms;

public class LoginForm : Form
{
    private UserManager userManager = new UserManager();

    private TextBox usernameTextBox;
    private TextBox emailTextBox;
    private TextBox passwordTextBox;
    private Button registerButton;
    private Button loginButton;

    public LoginForm()
    {
        InitializeComponents();
        this.MaximizeBox = false;
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.Text = "Welcome to Household Manager";
        this.Size = new System.Drawing.Size(500, 600);
        this.BackColor = Color.White;

        // PictureBox for logo
        PictureBox pictureBox = new PictureBox
        {
            // Uncomment and specify the path to your logo image
            // Image = Image.FromFile("./assets/logo.jpg"),
            SizeMode = PictureBoxSizeMode.CenterImage,
            Location = new System.Drawing.Point((this.Width - 100) / 2, 50),
            Size = new System.Drawing.Size(100, 100),
            BackColor = Color.Transparent
        };

        // Username Label and TextBox
        Label usernameLabel = new Label
        {
            Text = "Username",
            TextAlign = ContentAlignment.MiddleLeft,
            Location = new System.Drawing.Point(50, pictureBox.Bottom + 30),
            ForeColor = System.Drawing.Color.Black,
            AutoSize = true,
            Font = new Font("Arial", 10, FontStyle.Bold)
        };

        usernameTextBox = new TextBox
        {
            Location = new System.Drawing.Point(50, usernameLabel.Bottom + 5),
            Size = new System.Drawing.Size(400, 30),
            Font = new Font("Arial", 12),
            ForeColor = Color.Gray
        };

        // Email Label and TextBox
        Label emailLabel = new Label
        {
            Text = "Email",
            TextAlign = ContentAlignment.MiddleLeft,
            Location = new System.Drawing.Point(50, usernameTextBox.Bottom + 20),
            ForeColor = System.Drawing.Color.Black,
            AutoSize = true,
            Font = new Font("Arial", 10, FontStyle.Bold)
        };

        emailTextBox = new TextBox
        {
            Location = new System.Drawing.Point(50, emailLabel.Bottom + 5),
            Size = new System.Drawing.Size(400, 30),
            Font = new Font("Arial", 12),
            ForeColor = Color.Gray
        };

        // Password Label and TextBox
        Label passwordLabel = new Label
        {
            Text = "Password",
            TextAlign = ContentAlignment.MiddleLeft,
            Location = new System.Drawing.Point(50, emailTextBox.Bottom + 20),
            ForeColor = System.Drawing.Color.Black,
            AutoSize = true,
            Font = new Font("Arial", 10, FontStyle.Bold)
        };

        passwordTextBox = new TextBox
        {
            Location = new System.Drawing.Point(50, passwordLabel.Bottom + 5),
            Size = new System.Drawing.Size(400, 30),
            Font = new Font("Arial", 12),
            PasswordChar = '*',
            ForeColor = Color.Gray
        };

        // Register and Login Buttons
        registerButton = new Button
        {
            Text = "Register",
            Location = new System.Drawing.Point(50, passwordTextBox.Bottom + 30),
            Size = new System.Drawing.Size(180, 40),
            Font = new Font("Arial", 12, FontStyle.Bold),
            BackColor = Color.FromArgb(56, 151, 240),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        registerButton.Click += new EventHandler(RegisterButtonClick);

        loginButton = new Button
        {
            Text = "Login",
            Location = new System.Drawing.Point(270, passwordTextBox.Bottom + 30),
            Size = new System.Drawing.Size(180, 40),
            Font = new Font("Arial", 12, FontStyle.Bold),
            BackColor = Color.FromArgb(76, 175, 80),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat
        };
        loginButton.Click += new EventHandler(LoginButtonClick);

        this.Controls.Add(pictureBox);
        this.Controls.Add(usernameLabel);
        this.Controls.Add(usernameTextBox);
        this.Controls.Add(emailLabel);
        this.Controls.Add(emailTextBox);
        this.Controls.Add(passwordLabel);
        this.Controls.Add(passwordTextBox);
        this.Controls.Add(registerButton);
        this.Controls.Add(loginButton);

        this.StartPosition = FormStartPosition.CenterScreen;
    }

    private void TextBox_Enter(object sender, EventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (textBox.ForeColor == Color.Gray)
        {
            textBox.Text = "";
            textBox.ForeColor = Color.Black;
        }
    }

    private void TextBox_Leave(object sender, EventArgs e)
    {
        TextBox textBox = (TextBox)sender;
        if (string.IsNullOrWhiteSpace(textBox.Text))
        {
            textBox.ForeColor = Color.Gray;
            textBox.Text = textBox == usernameTextBox ? "Username" : textBox == emailTextBox ? "Email" : "Password";
        }
    }

    private void RegisterButtonClick(object sender, EventArgs e)
    {
        string username = usernameTextBox.Text;
        string email = emailTextBox.Text;
        string password = passwordTextBox.Text;
        userManager.RegisterUser(username, email, password);
    }

    private void LoginButtonClick(object sender, EventArgs e)
    {
        string username = usernameTextBox.Text;
        string email = emailTextBox.Text;
        string password = passwordTextBox.Text;

        if (userManager.Login(username, email, password))
        {
            MessageBox.Show("Login successful!");
            this.Hide();
            DashboardForm dashboard = new DashboardForm(username, email, password);
            dashboard.Show();
        }
        else
        {
            MessageBox.Show("Login failed. Invalid email or password.");
        }
    }

    private void InitializeComponents()
    {
        this.FormBorderStyle = FormBorderStyle.FixedSingle;
        this.MaximizeBox = false;
        this.StartPosition = FormStartPosition.CenterScreen;

        usernameTextBox = new TextBox
        {
            Location = new System.Drawing.Point(50, 50),
            Size = new System.Drawing.Size(400, 30),
            ForeColor = Color.Gray,
            Font = new Font("Arial", 12)
        };

        emailTextBox = new TextBox
        {
            Location = new System.Drawing.Point(50, 100),
            Size = new System.Drawing.Size(400, 30),
            ForeColor = Color.Gray,
            Font = new Font("Arial", 12)
        };

        passwordTextBox = new TextBox
        {
            Location = new System.Drawing.Point(50, 150),
            Size = new System.Drawing.Size(400, 30),
            ForeColor = Color.Gray,
            Font = new Font("Arial", 12),
            PasswordChar = '*'
        };

        registerButton = new Button
        {
            Text = "Register",
            Location = new System.Drawing.Point(50, 200),
            Size = new System.Drawing.Size(100, 30),
            BackColor = Color.LightBlue,
            ForeColor = Color.Black,
            FlatStyle = FlatStyle.Flat
        };

        loginButton = new Button
        {
            Text = "Login",
            Location = new System.Drawing.Point(150, 200),
            Size = new System.Drawing.Size(100, 30),
            BackColor = Color.LightGreen,
            ForeColor = Color.Black,
            FlatStyle = FlatStyle.Flat
        };
    }
}
