using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using Repository.Characters;
using Repository.Spells;

namespace Repository.AttackBehavior
{
    class SplitShotAttackBehavior : IAttackBehavior
    {
        public GameWorld GameWorld { get; set; }
        public void Attack(IGameObject gameObject)
        {
            var g = gameObject.Clone() as Fireball;
            g.Angle += 30;
            g.AttackBehaviors.Remove(this);
            g.Cast();
            var g2 = g.Clone() as Fireball;
            g2.Angle -= 60;
            g2.Cast();
        }

        public SplitShotAttackBehavior(GameWorld gameWorld)
        {
            GameWorld = gameWorld;
        }
    }
}
