using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Desktop_Assistant.DB;

namespace Desktop_Assistant.Forms
{
    public partial class AddGame : Form
    {
        public AddGame()
        {
            InitializeComponent();
        }
        Main main = new Main();
        private void button1_Click(object sender, EventArgs e)
        {
            List<Game> newGame = new List<Game>() {
                new Game { Name = gameName.Text, Path = gamePath.Text, LogoPath = logoPath.Text }
            };

            main.lb.SaveLibrary(newGame);
            System.Windows.MessageBox.Show($"{gameName.Text} saved!");
            this.Close();
        }
    }
}
