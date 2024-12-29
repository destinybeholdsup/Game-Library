using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Desktop_Assistant.DB;
using Desktop_Assistant.Forms;

namespace Desktop_Assistant
{

    public partial class Main : Form
    {
        public List<Game> loadedGames;
        public List<Game> saveGames;

        private Dictionary<Panel, Game> gamePanelMap = new Dictionary<Panel, Game>();

        private List<string> displayedGames = new List<string>();

        public Main()
        {
            InitializeComponent();
        }

        public LibraryHandler lb = new LibraryHandler();

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AddGame ag = new AddGame();
            ag.Show();
        }

        private void RemoveGameFromUI(Game game, Panel gamePanel)
        {
            flowLayoutPanel1.Controls.Remove(gamePanel);
            loadedGames.Remove(game);
            lb.SaveLibrary(loadedGames);
            displayedGames.Remove(game.Name);
        }

        private void RunGame(string gamePath)
        {
            try {
                System.Diagnostics.Process.Start(gamePath);
            }
            catch (Exception ex) {
                MessageBox.Show($"Error running the game: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LibraryUpdater_Tick(object sender, EventArgs e)
        {
            if (lb.LibraryExists(create: true)) {
                loadedGames = lb.LoadLibrary();

                foreach (var game in loadedGames) {
                    if (!displayedGames.Contains(game.Name)) {
                        Panel gamePanel = new Panel {
                            Size = new System.Drawing.Size(400, 120), 
                            BorderStyle = BorderStyle.FixedSingle
                        };

                        Label nameLabel = new Label {
                            Text = $"Name: {game.Name}",
                            Location = new System.Drawing.Point(10, 10),
                            AutoSize = true
                        };

                        Label pathLabel = new Label {
                            Text = $"Path: {game.Path}",
                            Location = new System.Drawing.Point(10, 30),
                            AutoSize = true
                        };

                        PictureBox logoPictureBox = new PictureBox {
                            Size = new System.Drawing.Size(50, 50),
                            Location = new System.Drawing.Point(250, 10),
                            ImageLocation = game.LogoPath,
                            SizeMode = PictureBoxSizeMode.StretchImage
                        };

                        Button runButton = new Button {
                            Text = "Run",
                            Location = new System.Drawing.Point(250, 70),
                            Size = new System.Drawing.Size(100, 30)
                        };

                        runButton.Click += (btnSender, btnEvent) => RunGame(game.Path);

                        gamePanel.Controls.Add(nameLabel);
                        gamePanel.Controls.Add(pathLabel);
                        gamePanel.Controls.Add(logoPictureBox);
                        gamePanel.Controls.Add(runButton);

                        flowLayoutPanel1.Controls.Add(gamePanel);
                        gamePanelMap[gamePanel] = game;
                        displayedGames.Add(game.Name);
                    }
                }
            }
        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {

        }
    }
}
