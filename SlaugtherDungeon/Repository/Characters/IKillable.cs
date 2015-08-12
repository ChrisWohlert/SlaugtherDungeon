using System.Collections.Generic;
using CHWGameEngine.GameObject;
using Repository.DamageBehavior;
using Repository.Experience;
using Repository.Loots;
using Repository.Spells;

namespace Repository.Characters
{
    public delegate void SendIKillable(IKillable killed, IGameObject source);

    public interface IKillable
    {
        int ExperienceWhenKilled { get; set; }
        Loot Loot { get; set; }

        event SendIKillable Killed;
        void TakeDamage(IDamageBehavior damageBehavior, Character source);
    }
}
