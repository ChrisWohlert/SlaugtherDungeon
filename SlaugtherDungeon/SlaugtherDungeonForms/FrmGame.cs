using System;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using Repository;
using Repository.Spells;

namespace SlaugtherDungeonForms
{
    public partial class FrmGame : Form
    {
        private SlaugtherDungeonGame game;
        private Font hudFont;
        private Brush hudBrush;
        private Point mouseLocation;
        public FrmGame()
        {
            InitializeComponent();
        }

        private void FrmGame_Load(object sender, EventArgs e)
        {
            game = new SlaugtherDungeonGame(this.CreateGraphics(), this.Size);

            InitHUD();
            
            game.Play();
            game.Draw += game_Draw;
        }

        private void InitHUD()
        {
            hudFont = new Font("Gothic", 12f, FontStyle.Bold);
            hudBrush = Brushes.Black;
        }

        void game_Draw(Graphics g)
        {
            DrawLifeBar(g);
            DrawManaBar(g);
            DrawExpBar(g);
            DrawSpellBar(g);

            if(game.GameOver)
                g.DrawString("GAME OVER!", new Font("Ariel", 72, FontStyle.Bold), Brushes.Maroon, Game.Size.Width / 2 - 320, Game.Size.Height / 2 - 50);
        }

        private void DrawLifeBar(Graphics g)
        {
            var player = game.Player;
            Brush healthColor;

            if (player.Health.Percentage < 15)
                healthColor = Brushes.Red;
            else if (player.Health.Percentage < 50)
                healthColor = Brushes.Yellow;
            else
                healthColor = Brushes.Green;

            int borderBrushWidth = 3;
            int borderWidth = 300;
            int hpWidth = (int)(borderWidth / (100d / player.Health.Percentage));
            int borderHeight = 15;

            g.DrawString("Health", hudFont, hudBrush, 0, 20);
            g.FillRectangle(healthColor, new Rectangle(5, 40, hpWidth, borderHeight));
            g.DrawRectangle(new Pen(Color.Black, borderBrushWidth), new Rectangle(5, 40, borderWidth, borderHeight));
        }

        private void DrawManaBar(Graphics g)
        {
            var player = game.Player;

            int borderWidth = 300;
            int borderHeight = 15;
            int manaWidth = (int)(borderWidth / (100d / player.Energy.Percentage)) - 1;

            g.DrawString("Mana", hudFont, hudBrush, 0, 60);
            g.FillRectangle(Brushes.Blue, new Rectangle(5, 80, manaWidth, borderHeight));
            g.DrawRectangle(new Pen(Color.Black, 3f), new Rectangle(5, 80, borderWidth, borderHeight));
        }

        private void DrawExpBar(Graphics g)
        {
            var player = game.Player;
            string xp = player.Experience.Current.ToString();
            string maxXp = player.Experience.Max.ToString();
            string level = player.Level.ToString();

            g.DrawString("Level: " + level, hudFont, hudBrush, 0, 100);
            g.DrawString("XP: " + maxXp + " / " + xp, hudFont, hudBrush, 0, 120);

            int borderWidth = 300;
            int borderHeight = 15;
            int expWidth = (int)(borderWidth / (100d / player.Experience.Percentage));

            g.FillRectangle(Brushes.White, new Rectangle(5, 140, expWidth, borderHeight));
            g.DrawRectangle(new Pen(Color.Black, 3f), new Rectangle(5, 140, borderWidth, borderHeight));
        }

        private void DrawSpellBar(Graphics g)
        {
            Image fireball = Repository.Properties.Resources.Fireball;
            Image frostball = Repository.Properties.Resources.Frostbolt_small;
            Bitmap spellbar = Properties.Resources.SpellBar;

            int firstslotX = (this.Size.Width/2 - spellbar.Width/2) + 88;
            int firstslotY = this.Height - spellbar.Height + 50;

            int secondslotX = (this.Size.Width/2 - spellbar.Width/2) + 138;
            int secondslotY = this.Height - spellbar.Height + 57;

            g.DrawImage(spellbar, this.Size.Width / 2 - spellbar.Width / 2, this.Height - spellbar.Height);
            g.DrawImage(fireball, firstslotX, firstslotY);
            g.DrawImage(frostball, secondslotX, secondslotY);

            int cdFirstSlot = game.GetPlayerCooldown(ActionSlot.First).Current;
            int cdSecondSlot = game.GetPlayerCooldown(ActionSlot.Second).Current;

            if (cdFirstSlot != 0)
                g.DrawString(cdFirstSlot.ToString(), new Font("Arial", 20f), Brushes.Black, firstslotX + 10, firstslotY + 10);
            if(cdSecondSlot != 0)
                g.DrawString(cdSecondSlot.ToString(), new Font("Arial", 20f), Brushes.Black, secondslotX + 5, firstslotY + 10);
        }

        private void FrmGame_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLocation = e.Location;
            game.SetPlayerAnglePoint(mouseLocation);
        }

        private void FrmGame_MouseDown(object sender, MouseEventArgs e)
        {
            game.MovePlayer();
        }

        private void FrmGame_MouseUp(object sender, MouseEventArgs e)
        {
            game.BreakPlayer();
        }

        private void FrmGame_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Q:
                    game.CastSpell(ActionSlot.First);
                    break;
                case Keys.W:
                    game.CastSpell(ActionSlot.Second);
                    break;
                case Keys.E:
                    game.CastSpell(ActionSlot.Third);
                    break;
                case Keys.R:
                    game.CastSpell(ActionSlot.Fourth);
                    break;
                case Keys.Space:
                    game.ChangeTarget(mouseLocation, 300, false);
                    break;
                case Keys.Tab:
                    game.ChangeTarget(game.Player.Location.ToPoint(), 2000, true);
                    break;
            }
        }
    }
}
