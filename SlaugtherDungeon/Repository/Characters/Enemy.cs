using System.Drawing;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using CHWGameEngine.Motion;
using Repository.Experience;
using Repository.Spells;

namespace Repository.Characters
{
    abstract class Enemy : Character
    {
        protected Enemy(GameWorld gameWorld, DecimalPoint location) : base(gameWorld, location)
        {
            Targets.Add(typeof(Player));
            Target = gameWorld.Player;
        }

    }
}
