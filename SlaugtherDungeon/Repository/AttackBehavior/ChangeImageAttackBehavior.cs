using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using Repository.Spells;

namespace Repository.AttackBehavior
{
    class ChangeImageAttackBehavior : IAttackBehavior
    {
        public GameWorld GameWorld { get; set; }
        public void Attack(IGameObject gameObject)
        {
            gameObject.Image = Properties.Resources.Frostbolt_small;
        }

        public ChangeImageAttackBehavior(GameWorld gameWorld)
        {
            GameWorld = gameWorld;
        }
    }
}
