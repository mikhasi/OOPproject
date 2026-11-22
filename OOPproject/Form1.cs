using System;
using System.Drawing;
using System.Windows.Forms;

namespace Second_Project_Test__Attempt_2_
{
    public partial class Form1 : Form
    {
        private Label label1, label2, label3, label4, label5;
        private Panel panel1;
        private GroupBox groupBox1;
        private Button button1, button2, button3;
        private TextBox textBox1, textBox2;

        public Form1()
        {
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            this.Text = "Login";
            this.Size = new Size(400, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;

            panel1 = new Panel
            {
                BackColor = SystemColors.Window,
                Location = new Point(0, 0),
                Size = new Size(1798, 51)
            };

            label4 = new Label
            {
                Text = "Akademia Finansów i Biznesu Vistula",
                Location = new Point(100, 17),
                AutoSize = true
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

            label1 = new Label
            {
                Text = "Welcome",
                Font = new Font("Microsoft Sans Serif", 12F, FontStyle.Bold),
                Location = new Point(85, 20),
                AutoSize = true
            };

            label2 = new Label
            {
                Text = "Username",
                Location = new Point(22, 60),
                AutoSize = true
            };

            textBox1 = new TextBox
            {
                Location = new Point(22, 80),
                Size = new Size(224, 26)
            };

            label3 = new Label
            {
                Text = "Password",
                Location = new Point(22, 150),
                AutoSize = true
            };

            textBox2 = new TextBox
            {
                Location = new Point(22, 170),
                Size = new Size(224, 26),
                PasswordChar = '●',
                UseSystemPasswordChar = true
            };

            button3 = new Button
            {
                Text = "FORGOT YOUR PASSWORD?",
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Nirmala UI", 7F, FontStyle.Bold),
                ForeColor = Color.FromArgb(164, 165, 169),
                Location = new Point(102, 200),
                Size = new Size(152, 20),
                BackColor = Color.Transparent,
                Cursor = Cursors.Hand
            };
            button3.Click += button3_Click;

            button1 = new Button
            {
                Text = "LOGIN",
                Location = new Point(22, 256),
                Size = new Size(224, 44),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold),
                ForeColor = Color.FromArgb(164, 165, 169),
                Cursor = Cursors.Hand
            };
            button1.Click += button1_Click;

            button2 = new Button
            {
                Text = "SIGN UP",
                Location = new Point(22, 314),
                Size = new Size(224, 44),
                BackColor = Color.MidnightBlue,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold),
                ForeColor = Color.FromArgb(164, 165, 169),
                Cursor = Cursors.Hand
            };
            button2.Click += button2_Click;

            label5 = new Label
            {
                Text = "© 2025 OOPtimizers Inc. All rights reserved.",
                Font = new Font("Nirmala UI", 7.8F),
                ForeColor = Color.FromArgb(164, 165, 169),
                Location = new Point(18, 395),
                AutoSize = true
            };

            groupBox1.Controls.Add(label1);
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(textBox1);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(textBox2);
            groupBox1.Controls.Add(button1);
            groupBox1.Controls.Add(button2);
            groupBox1.Controls.Add(button3);
            groupBox1.Controls.Add(label5);

            this.Controls.Add(panel1);
            this.Controls.Add(groupBox1);
        }

        private bool validateTextBox()
        {
            if (string.IsNullOrEmpty(textBox1.Text) || string.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (textBox2.Text.Contains("?"))
            {
                MessageBox.Show("You can't use a question mark (?) in your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            FP forgotPasswordForm = new FP();
            this.Hide();
            forgotPasswordForm.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!validateTextBox())
            {
                return;
            }

            var username = textBox1.Text;
            var password = textBox2.Text;
            var user = manageUsers.findUser(username);

            if (user == null)
            {
                MessageBox.Show("User not found. Please register.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (user.getPassword() != password)
            {
                MessageBox.Show("Your password is wrong.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            var gameForm = new Game(username);
            this.Hide();
            gameForm.ShowDialog();
            this.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SIGNUP signupForm = new SIGNUP();
            this.Hide();
            signupForm.ShowDialog();
            this.Show();
        }
    }
}