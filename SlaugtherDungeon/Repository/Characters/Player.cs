using System;
using System.Collections.Generic;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using CHWGameEngine.Motion;
using Repository.AttackBehavior;
using Repository.Buffs;
using Repository.DamageBehavior;
using Repository.Energies;
using Repository.Experience;
using Repository.Spells;

namespace Repository.Characters
{
    public class Player : Character
    {
        public Player(GameWorld gameWorld, DecimalPoint location) : base(gameWorld, location)
        {
            Image = Properties.Resources.Player;
            Acceleration = 2;
            Inertia.X = 2;
            Inertia.Y = 2;
            DrawBehavior.IsCircle = true;
            Health.Max = 5000;
            Health.Current = Health.Max;
            Targets.Add(typeof(Enemy));
        }
    }
}
