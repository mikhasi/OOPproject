using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Second_Project_Test__Attempt_2_
{
    public partial class FP : Form
    {
        private Label label2, label3, label4, label5, label6, label7;
        private TextBox textBox3, textBox4;
        private Button button4, button5;
        private Panel panel1;
        private GroupBox groupBox1;

        public FP()
        {
            InitializeComponent();
            CreateForgetPasswordUI();
        }

        private void CreateForgetPasswordUI()
        {
            this.Text = "Forgot Password";
            this.Size = new Size(400, 600);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterScreen;

            panel1 = new Panel
            {
                BackColor = SystemColors.Window,
                Location = new Point(0, 0),
                Size = new Size(1797, 51)
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
                Text = "Forgot Password",
                Font = new Font("Microsoft Sans Serif", 14, FontStyle.Bold),
                Location = new Point(45, 15),
                AutoSize = true
            };

            label7 = new Label
            {
                Text = "Please reset your password",
                Location = new Point(60, 45),
                AutoSize = true
            };

            label3 = new Label
            {
                Text = "Username",
                Location = new Point(22, 79),
                AutoSize = true,
            };

            label5 = new Label
            {
                Text = "New Password",
                Location = new Point(22, 149),
                AutoSize = true
            };

            textBox3 = new TextBox
            {
                Location = new Point(22, 170),
                Size = new Size(224, 26),
                Font = new Font("Microsoft Sans Serif", 12),
                BorderStyle = BorderStyle.FixedSingle,
                PasswordChar = '●',
                UseSystemPasswordChar = true
            };

            textBox4 = new TextBox
            {
                Location = new Point(22, 100),
                Size = new Size(224, 26),
                Font = new Font("Microsoft Sans Serif", 12),
                BorderStyle = BorderStyle.FixedSingle
            };

            label2 = new Label
            {
                Text = "© 2025 OOPtimizers Inc. All rights reserved.",
                Location = new Point(18, 395),
                AutoSize = true,
                Font = new Font("Nirmala UI", 7.8F),
                ForeColor = Color.Gray
            };

            button5 = new Button
            {
                Text = "ADD NEW PASSWORD",
                Location = new Point(22, 256),
                Size = new Size(223, 44),
                BackColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold),
                ForeColor = Color.Gray
            };
            button5.Click += button5_Click;

            button4 = new Button
            {
                Text = "BACK",
                Location = new Point(22, 313),
                Size = new Size(223, 44),
                BackColor = Color.MidnightBlue,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Font = new Font("Microsoft Sans Serif", 10.2F, FontStyle.Bold),
                ForeColor = Color.Gray
            };
            button4.Click += button4_Click;

            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(label7);
            groupBox1.Controls.Add(label5);
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
            string newPassword = textBox3.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(newPassword))
            {
                MessageBox.Show("Please enter both username and password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (newPassword.Contains("?"))
            {
                MessageBox.Show("You can't use a question mark (?) in your password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                string[] lines = File.ReadAllLines("users.txt");
                bool found = false;

                for (int i = 0; i < lines.Length; i++)
                {
                    var parts = lines[i].Split('|');
                    if (parts.Length == 2 && parts[0] == username)
                    {
                        lines[i] = $"{username}|{newPassword}";
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    File.WriteAllLines("users.txt", lines);
                    MessageBox.Show("Password updated successfully.");
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Username not found. Cannot update.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating password: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
