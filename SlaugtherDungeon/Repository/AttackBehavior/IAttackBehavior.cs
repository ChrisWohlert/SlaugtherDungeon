using System.Collections.Generic;
using CHWGameEngine;
using CHWGameEngine.GameObject;

namespace Repository.AttackBehavior
{
    public interface IAttackBehavior
    {
        GameWorld GameWorld { get; set; }
        void Attack(IGameObject gameObject);
    }
}