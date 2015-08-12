using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using Repository.Characters;

namespace Repository.Loots
{
    class HealthPowerUp : PowerUp
    {
        private Player p;
        private double health;

        public HealthPowerUp(DecimalPoint location, GameWorld gameWorld) : base(location, gameWorld)
        {
            Image = Properties.Resources.heart;
            health = 500;
            DropChance = 50;
        }

        public override void gameWorld_Collision(ref IGameObject object1, ref IGameObject object2)
        {
            if ((object1 == GameWorld.Player || object2 == GameWorld.Player) && (object1 == this || object2 == this))
            {
                p = GameWorld.Player as Player;
                Duration = 5;
                Interval = 100;
                Start();
                GameWorld.Remove(this);
            }
        }

        public override void Effect()
        {
            p.Health.Regenerate((health/((1000d/Interval)*Duration)));
        }

        public override void UndoEffect()
        {
            
        }
    }
}
