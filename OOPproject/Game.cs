using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Second_Project_Test__Attempt_2_
{
    public partial class Game : Form
    {
        private int gridSize = 4;
        private Button[,] buttons;
        private List<int> numbers;
        private int score = 0;
        private Label scoreLabel, timerLabel;
        private Button firstClicked, secondClicked, startButton, stopButton, easyButton, hardButton, leaderboardButton, howToPlayButton;
        private Timer gameTimer;
        private int time = 0;
        private Panel gridPanel, uiPanel;
        private string currentUsername;
        private const string LEADERBOARD_FILE = "leaderboard.txt";
        private string difficulty = "Easy";
        private GameMode gameMode = new EasyMode();
        Random r = new Random();

        public Game(string username)
        {
            currentUsername = username;
            InitializeComponent();
            CreateUI();
            this.ClientSize = new Size(800, 500);
        }

        private void CreateUI()
        {
            this.Text = "Memory Game";
            this.ClientSize = new Size(900, 500);

            gridPanel = new Panel { Location = new Point(10, 10), Size = new Size(500, 500) };
            uiPanel = new Panel { Location = new Point(520, 10), Size = new Size(360, 480) };

            this.Controls.Add(gridPanel);
            this.Controls.Add(uiPanel);

            startButton = new Button { Text = "Start", Size = new Size(150, 40), Location = new Point(100, 20), BackColor = Color.LightGreen };
            stopButton = new Button { Text = "Stop", Size = new Size(150, 40), Location = new Point(100, 70), BackColor = Color.IndianRed, Enabled = false };
            easyButton = new Button { Text = "Easy (4x4)", Size = new Size(150, 40), Location = new Point(100, 130) };
            hardButton = new Button { Text = "Hard (6x6)", Size = new Size(150, 40), Location = new Point(100, 180) };
            leaderboardButton = new Button { Text = "Leaderboard", Size = new Size(150, 40), Location = new Point(100, 330), BackColor = Color.Gold, Font = new Font("Arial", 12, FontStyle.Bold) };
            howToPlayButton = new Button { Text = "How to Play", Size = new Size(150, 40), Location = new Point(100, 380), BackColor = Color.LightBlue, Font = new Font("Arial", 12, FontStyle.Bold) };

            scoreLabel = new Label { Text = "Score: 0", Font = new Font("Arial", 14, FontStyle.Bold), Location = new Point(100, 240), AutoSize = true };
            timerLabel = new Label { Text = "Time: 0s", Font = new Font("Arial", 14, FontStyle.Bold), Location = new Point(100, 280), AutoSize = true };

            uiPanel.Controls.Add(startButton);
            uiPanel.Controls.Add(stopButton);
            uiPanel.Controls.Add(easyButton);
            uiPanel.Controls.Add(hardButton);
            uiPanel.Controls.Add(scoreLabel);
            uiPanel.Controls.Add(timerLabel);
            uiPanel.Controls.Add(leaderboardButton);
            uiPanel.Controls.Add(howToPlayButton);

            startButton.Click += StartButton_Click;
            stopButton.Click += StopButton_Click;
            easyButton.Click += EasyButton_Click;
            hardButton.Click += HardButton_Click;
            leaderboardButton.Click += ShowLeaderboard;
            howToPlayButton.Click += HowToPlayButton_Click;

            gameTimer = new Timer {
                Interval = 1000
            };
            gameTimer.Tick += GameTimer_Tick;

            CreateGrid();
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            StartGame();
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            UpdateTimer();
        }

        private void StopButton_Click(object sender, EventArgs e)
        {
            StopGame();
        }

        private void EasyButton_Click(object sender, EventArgs e)
        {
            gridSize = 4;
            CreateGrid();
            difficulty = "Easy";
            gameMode = new EasyMode();
        }

        private void HardButton_Click(object sender, EventArgs e)
        {
            gridSize = 6;
            CreateGrid();
            difficulty = "Hard";
            gameMode = new HardMode();
        }

        private void HowToPlayButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Match pairs of numbers by clicking on the buttons. " +
                            "You earn points based on how quickly you find them. " +
                            "Try to beat your best time and score!", "How to Play", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void CreateGrid()
        {
            gridPanel.Controls.Clear();
            buttons = new Button[gridSize, gridSize];
            int size = 400 / gridSize;
            int padding = 5;

            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    Button btn = new Button
                    {
                        Size = new Size(size - padding, size - padding),
                        Location = new Point(col * size, row * size),
                        Font = new Font("Arial", 14, FontStyle.Bold),
                        BackColor = Color.Gray,
                        ForeColor = Color.Black,
                        Tag = new Point(row, col),
                        Enabled = false
                    };
                    btn.Click += Button_Click;
                    gridPanel.Controls.Add(btn);
                    buttons[row, col] = btn;
                }
            }
        }

        private void StartGame()
        {
            score = 0;
            time = 0;
            scoreLabel.Text = "Score: 0";
            timerLabel.Text = "Time: 0s";
            gameTimer.Start();
            startButton.Enabled = false;
            stopButton.Enabled = true;
            easyButton.Enabled = false;
            hardButton.Enabled = false;

            numbers = new List<int>();
            int pairCount = (gridSize * gridSize) / 2;

            List<int> tempNumbers = new List<int>();
            while (tempNumbers.Count < pairCount)
            {
                int candidate = r.Next(1, 100);
                if (!tempNumbers.Contains(candidate))
                    tempNumbers.Add(candidate);
            }

            foreach (var num in tempNumbers)
            {
                numbers.Add(num);
                numbers.Add(num);
            }
            numbers = numbers.OrderBy(x => r.Next()).ToList();

            int index = 0;
            for (int row = 0; row < gridSize; row++)
            {
                for (int col = 0; col < gridSize; col++)
                {
                    var btn = buttons[row, col];
                    btn.Tag = numbers[index++];
                    btn.Text = "";
                    btn.BackColor = Color.Black;
                    btn.ForeColor = Color.Black;
                    btn.Enabled = true;
                }
            }
            firstClicked = null;
            secondClicked = null;
        }

        private void StopGame()
        {
            gameTimer.Stop();
            time = 0;
            timerLabel.Text = "Time: 0s";
            startButton.Enabled = true;
            stopButton.Enabled = false;
            easyButton.Enabled = true;
            hardButton.Enabled = true;

            foreach (var btn in buttons)
            {
                btn.Enabled = false;
                btn.BackColor = Color.Gray;
                btn.ForeColor = Color.Black;
                btn.Text = "";
            }
        }
        private void UpdateTimer()
        {
            time++;
            timerLabel.Text = $"Time: {time}s";
        }

        private void SaveScore()
        {
            try
            {
                string newScore = $"{currentUsername}|{score}|{time}|{difficulty}{Environment.NewLine}";
                File.AppendAllText(LEADERBOARD_FILE, newScore);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving score: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ShowLeaderboard(object sender, EventArgs e)
        {
            try
            {
                if (!File.Exists(LEADERBOARD_FILE) || new FileInfo(LEADERBOARD_FILE).Length == 0)
                {
                    MessageBox.Show("No scores recorded yet!", "Leaderboard", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                string[] lines = File.ReadAllLines(LEADERBOARD_FILE);
                List<ScoreEntry> scoreList = new List<ScoreEntry>();

                for (int i = 0; i < lines.Length; i++)
                {
                    string line = lines[i];
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    string[] parts = line.Split('|');
                    if (parts.Length < 4) continue;

                    ScoreEntry entry = new ScoreEntry();
                    entry.Username = parts[0];

                    try
                    {
                        entry.Score = int.Parse(parts[1]);
                    }
                    catch
                    {
                        entry.Score = 0;
                    }

                    try
                    {
                        entry.Time = int.Parse(parts[2]);
                    }
                    catch
                    {
                        entry.Time = 0;
                    }

                    entry.Difficulty = parts[3];
                    scoreList.Add(entry);
                }

                for (int i = 0; i < scoreList.Count - 1; i++)
                {
                    for (int j = i + 1; j < scoreList.Count; j++)
                    {
                        bool swap = false;
                        if (scoreList[j].Score > scoreList[i].Score)
                        {
                            swap = true;
                        }
                        else if (scoreList[j].Score == scoreList[i].Score && scoreList[j].Time < scoreList[i].Time)
                        {
                            swap = true;
                        }
                        if (swap)
                        {
                            ScoreEntry temp = scoreList[i];
                            scoreList[i] = scoreList[j];
                            scoreList[j] = temp;
                        }
                    }
                }

                List<string> scores = new List<string>();
                int count = scoreList.Count < 10 ? scoreList.Count : 10;
                for (int i = 0; i < count; i++)
                {
                    ScoreEntry entry = scoreList[i];
                    scores.Add((i + 1) + ". " + entry.Username + " - Score: " + entry.Score + " - Time: " + entry.Time + "s - " + entry.Difficulty);
                }

                string leaderboardText = "Top 10 Players:\n\n" + string.Join("\n", scores.ToArray());
                MessageBox.Show(leaderboardText, "Leaderboard", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading leaderboard: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Button_Click(object sender, EventArgs e)
        {
            Button clicked = sender as Button;
            if (clicked == null || clicked == firstClicked || clicked.ForeColor == Color.Yellow) return;

            clicked.Text = clicked.Tag.ToString();
            clicked.BackColor = Color.Yellow;
            clicked.ForeColor = Color.Black;

            if (firstClicked == null)
            {
                firstClicked = clicked;
                return;
            }

            secondClicked = clicked;

            if (firstClicked.Tag.Equals(secondClicked.Tag))
            {
                firstClicked.Enabled = false;
                secondClicked.Enabled = false;
                int points = gameMode.CalculatePoints(time);

                score += points;
                scoreLabel.Text = "Score: " + score;

                firstClicked = null;
                secondClicked = null;

                bool allDisabled = true;
                foreach (Button b in gridPanel.Controls.OfType<Button>())
                {
                    if (b.Enabled)
                    {
                        allDisabled = false;
                        break;
                    }
                }
                if (allDisabled)
                {
                    gameTimer.Stop();
                    SaveScore();
                    MessageBox.Show($"You win!\nFinal Score: {score}\nTime: {time}s",
                                    "Congratulations",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    StopGame();
                }
            }
            else
            {
                MessageBox.Show("Wrong Match!", "Memory Game", MessageBoxButtons.OK, MessageBoxIcon.Information);

                {
                    firstClicked.Text = "";
                    secondClicked.Text = "";
                    firstClicked.BackColor = Color.Black;
                    secondClicked.BackColor = Color.Black;
                    firstClicked.ForeColor = Color.Black;
                    secondClicked.ForeColor = Color.Black;
                    firstClicked = null;
                    secondClicked = null;
                };
            }
        }
    }
}
