using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CHWGameEngine;

namespace SlaugtherDungeonForm
{
    public partial class FrmGame : Form
    {
        public FrmGame()
        {
            InitializeComponent();

            var game = new Game(this.CreateGraphics(), "asd", new Point(50, 0));
            game.Start();
        }
    }
}
