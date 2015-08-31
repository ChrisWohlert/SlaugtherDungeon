using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using CHWGameEngine.Tiles;
using Repository.Characters;
using Repository.Properties;
using Repository.Spells;

namespace Repository
{
    public class SlaugtherDungeonGame
    {
        private GameWorld gameWorld;
        private Game game;
        public bool GameOver;

        public Player Player { get; private set; }
        public IEnumerable<IGameObject> GameObjects { get { return gameWorld.GameObjects; } }
        public float OffsetX { get { return gameWorld.OffsetX; } }
        public float OffsetY { get { return gameWorld.OffsetY; } }

        public event EventHandler Loaded;
        public event EventHandler Updated;
        public event Game.SendGraphics Draw;
        public SlaugtherDungeonGame(Graphics g, Size gameSize)
        {
            gameWorld = new GameWorld(@"C:\Users\54430\Google Drev\level1.txt");
            game = new Game(g, gameWorld, gameSize);
            game.Draw += game_Draw;
            game.OutOfVision += game_OutOfVision;
        }

        void game_OutOfVision(IGameObject gameObject)
        {

        }

        void game_Draw(Graphics g)
        {
            DrawHealthBars(g);
            if (Draw != null) Draw(g);
        }

        private void DrawHealthBars(Graphics g)
        {
            foreach (var go in gameWorld.VisibleGameObjects)
            {
                var obj = go as Enemy;
                if (obj != null)
                {
                    Brush healthColor;
                    if (obj.Health.Percentage < 15)
                        healthColor = Brushes.Red;
                    else if (obj.Health.Percentage < 50)
                        healthColor = Brushes.Yellow;
                    else
                        healthColor = Brushes.Green;

                    int hpBorderWidth = go.Image.Width;
                    int hpWidth = (int)(hpBorderWidth / (100d / obj.Health.Percentage));
                    int hpHeight = 10;
                    int hpX = (int)(go.Location.X - OffsetX + go.MotionBehavior.Area.X);
                    int hpY = (int)(go.Location.Y - hpHeight - OffsetY + go.MotionBehavior.Area.Y);
                    g.FillRectangle(healthColor, hpX + 1, hpY + 1, hpWidth, hpHeight - 1);
                    g.DrawRectangle(new Pen(Color.Black), hpX, hpY, hpBorderWidth, hpHeight);

                    if (Player.Target != null)
                        g.DrawRectangle(new Pen(Color.OrangeRed, 4f), GetObjectArea(Player.Target));
                }
            }
        }

        #region Private Methods

        private void GameLoop()
        {
            var t = new Task(() =>
            {
                while (!GameOver)
                {
                    int timeToSleep = 40;
                    var watch = Stopwatch.StartNew();
                    try
                    {
                        var list = gameWorld.GameObjects.ToList();
                        foreach (var go in list)
                        {
                            go.MotionBehavior.Move();

                            var o = go as Monster;
                            if(o != null) o.Attack(ActionSlot.First); 
                        }

                        gameWorld.CheckCollisions();
                        game.Render();
                        if (Updated != null) Updated(this, EventArgs.Empty);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                    watch.Stop();

                    timeToSleep = Math.Max(0, timeToSleep - (int)watch.ElapsedMilliseconds);
                    Console.WriteLine(timeToSleep);
                    Thread.Sleep(timeToSleep);
                }
                game.Render();

                
            });
            t.Start();
        }

        private void Init()
        {
            Tile t = new Tile(Resources.Grass, 0);
            TileRepository.Add(t);
            t = new Tile(Resources.Rock, 1);
            t.IsSolid = true;
            TileRepository.Add(t);
            Tile brick = new Tile(Properties.Resources.BrickWall, 2);
            brick.IsSolid = true;
            TileRepository.Add(brick);
            Tile grassstone = new Tile(Properties.Resources.FloorsMedieval, 3);
            grassstone.IsSolid = false;
            TileRepository.Add(grassstone);

            Player = CreatePlayer();
            gameWorld.Player = Player;
            gameWorld.GameObjects.Add(Player);
            Random r = new Random();

            for (int i = 0; i < 5; i++)
            {
                DecimalPoint loc;
                int x;
                int y;
                do
                {
                    x = r.Next(2000);
                    y = r.Next(2000);
                    loc = new DecimalPoint(x, y);
                } while (gameWorld.GetTile(x / Tile.Width, y / Tile.Height).IsSolid);
                Enemy monster = new Monster(gameWorld, loc);
                gameWorld.GameObjects.Add(monster);
            }

            if(Loaded != null) Loaded(this, EventArgs.Empty);
        }

        private Player CreatePlayer()
        {
            var p = new Player(gameWorld, new DecimalPoint(1000, 2000));
            p.Killed += p_Killed;

            return p;
        }

        void p_Killed(IKillable killed, IGameObject source)
        {
            GameOver = true;
        }

        private static double CalcAngle(Point first, Point second)
        {
            return GameWorld.CalcAngle(first, second);
        }

        void target_Killed(IKillable killed, IGameObject source)
        {
            Player.Target = null;
        }

        #endregion


        #region Public Methods

        public void Play()
        {
            Init();
            GameLoop();
        }

        public void Exit()
        {

        }

        public void Pause()
        {

        }
        public void MovePlayer()
        {
            Player.TargetSpeed = 10;
        }

        public void BreakPlayer()
        {
            Player.TargetSpeed = 0;
        }

        public void SetPlayerAnglePoint(Point destination)
        {
            if (Player != null)
                Player.Angle = (int) CalcAngle(new Point(Game.Size.Width/2, Game.Size.Height/2), destination);
        }

        public void CastSpell(ActionSlot actionSlot)
        {
            try
            {
                Player.Attack(actionSlot);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public Spell GetSpell(ActionSlot actionSlot)
        {
            try
            {
                return Player.SpellHandler.Spells[(int)actionSlot];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return null;
        }

        public Cooldown GetPlayerCooldown(ActionSlot actionSlot)
        {
            Spell s = Player.SpellTree.GetSpell(actionSlot);
            Type t = s != null ? s.GetType() : null;
            if(t != null)
                if (Player.SpellHandler.Cooldowns.ContainsKey(t))
                    return Player.SpellHandler.Cooldowns[t];

            return null;
        }

        public void ChangeTarget(Point targetLocation, int range, bool fromMap)
        {
            Player.Target = null;
            IGameObject target = null;
            target = fromMap ? gameWorld.GetClosestGameObjectFromMap(targetLocation, range) : gameWorld.GetClosestGameObject(targetLocation, null, range);

            if (target != null)
            {
                bool isTarget = false;
                foreach (var t in Player.Targets)
                {
                    if (target.GetType() == t || target.GetType().IsSubclassOf(t)) isTarget = true;
                }

                Player.Target = isTarget ? target : null;

                var isKillable = target as IKillable;
                if (isKillable != null)
                    isKillable.Killed += target_Killed;
            }
        }

        public Rectangle GetObjectArea(IGameObject gameObject)
        {
            return new Rectangle((int)(gameObject.Location.X - OffsetX + gameObject.MotionBehavior.Area.X), (int)(gameObject.Location.Y - OffsetY + gameObject.MotionBehavior.Area.Y), gameObject.MotionBehavior.Area.Width, gameObject.MotionBehavior.Area.Height);
        }

        #endregion

    }
}
