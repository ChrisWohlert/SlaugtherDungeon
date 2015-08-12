using System;
using System.Collections.Generic;
using CHWGameEngine.GameObject;
using Repository.Characters;

namespace Repository.DamageBehavior
{
    public interface IDamageBehavior : ICloneable
    {
        Character Source { get; set; }
        List<Type> TargetTypes { get; } 
        int Damage { get; set; }
        bool CanDamage(IGameObject target);
        bool DoDamage(IGameObject target);
    }
}