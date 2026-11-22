using System;
using System.Drawing;
using System.Windows.Forms;

namespace Second_Project_Test__Attempt_2_
{
    public partial class SIGNUP : Form
    {
        private Label label1, label2, label3, label4, label5, label6;
        private Panel panel1;
        private GroupBox groupBox1;
        private Button button4, button5;
        private TextBox textBox3, textBox4;

        public SIGNUP()
        {
            InitializeComponent();
            CreateSignUpUI();
        }

        private void CreateSignUpUI()
        {
            this.Text = "Sign Up";
            this.Size = new Size(400, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            panel1 = new Panel
            {
                BackColor = SystemColors.Window,
                Location = new Point(0, 0),
                Size = new Size(800, 51)
            };

            label4 = new Label
            {
                AutoSize = true,
                Location = new Point(100, 17),
                Text = "Akademia Finansów i Biznesu Vistula"
            };
            panel1.Controls.Add(label4);

            int groupBoxWidth = 263;
            int groupBoxHeight = 422;
            int startX = (this.ClientSize.Width - groupBoxWidth) / 2;
            int startY = (this.ClientSize.Height - groupBoxHeight) / 2;

            groupBox1 = new GroupBox
            {
                Size = new Size(groupBoxWidth, groupBoxHeight),
                Location = new Point(startX, startY),
                Anchor = AnchorStyles.None
            };

            label6 = new Label
            {
                Text = "Sign Up",
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold),
                Location = new Point(90, 15),
                AutoSize = true
            };

            label5 = new Label
            {
                Text = "Please create your account",
                Location = new Point(60, 45),
                AutoSize = true
            };

            label1 = new Label
            {
                Text = "Create Password",
                Location = new Point(22, 149),
                AutoSize = true,
            };

            label3 = new Label
            {
                Text = "Create Username",
                Location = new Point(22, 79),
                AutoSize = true,
            };

            label2 = new Label
            {
                Text = "© 2025 OOPtimizers Inc. All rights reserved.",
                Location = new Point(18, 395),
                AutoSize = true,
                Font = new Font("Nirmala UI", 7.8f),
                ForeColor = Color.FromArgb(164, 165, 169)
            };

            textBox4 = new TextBox
            {
                Location = new Point(22, 100),
                Size = new Size(224, 26),
                Font = new Font("Microsoft Sans Serif", 12),
                BorderStyle = BorderStyle.FixedSingle
            };

            textBox3 = new TextBox
            {
                Location = new Point(22, 170),
                Size = new Size(224, 26),
                Font = new Font("Microsoft Sans Serif", 12),
                PasswordChar = '●',
                UseSystemPasswordChar = true,
                BorderStyle = BorderStyle.FixedSingle
            };

            button5 = new Button
            {
                Text = "ADD ACCOUNT",
                Location = new Point(22, 256),
                Size = new Size(224, 44),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                ForeColor = Color.FromArgb(164, 165, 169),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            };
            button5.Click += button5_Click;

            button4 = new Button
            {
                Text = "BACK",
                Location = new Point(22, 314),
                Size = new Size(224, 44),
                BackColor = Color.MidnightBlue,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                ForeColor = Color.FromArgb(164, 165, 169),
                Font = new Font("Microsoft Sans Serif", 10, FontStyle.Bold)
            };
            button4.Click += button4_Click;

            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(textBox4);
            groupBox1.Controls.Add(textBox3);
            groupBox1.Controls.Add(button5);
            groupBox1.Controls.Add(button4);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(label3);

            this.Controls.Add(panel1);
            this.Controls.Add(groupBox1);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            string username = textBox4.Text.Trim();
            string password = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password.Contains("?"))
            {
                MessageBox.Show("You can't use a question mark (?) in your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var existingUser = manageUsers.findUser(username);
            if (existingUser != null)
            {
                MessageBox.Show("User already exists.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                var newUser = new usersList(username, password);
                if (manageUsers.saveUser(newUser))
                {
                    MessageBox.Show("New user created successfully.");
                    this.Close();
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
