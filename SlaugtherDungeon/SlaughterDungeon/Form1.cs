using System;
using System.Windows.Forms;
using CHWGameEngine;

namespace SlaughterDungeon
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var game = new Game(this.CreateGraphics());
            game.Start();
        }
    }
}
