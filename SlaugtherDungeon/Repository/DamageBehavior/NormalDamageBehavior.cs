using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine.GameObject;
using Repository.Characters;
using Repository.Spells;

namespace Repository.DamageBehavior
{
    class NormalDamageBehavior : IDamageBehavior
    {
        public Character Source { get; set; }
        public List<Type> TargetTypes { get; private set; }
        public int Damage { get; set; }

        public NormalDamageBehavior(Character source, IEnumerable<Type> targetTypes, int damage)
        {
            Damage = damage;
            this.Source = source;
            this.TargetTypes = targetTypes.ToList();
        }

        public bool CanDamage(IGameObject target)
        {
            var killable = target as IKillable;
            if (killable == null) return false;

            bool attack = false;
            foreach (var t in TargetTypes)
            {
                if (killable.GetType().IsSubclassOf(t) || killable.GetType() == t) attack = true;
            }

            if (attack)
            {
                return true;
            }
            return false;
        }

        public bool DoDamage(IGameObject target)
        {
            if (!CanDamage(target)) return false;

            var killable = target as IKillable;
            if (killable == null) return false;
            killable.TakeDamage(this, Source);
            return true;
        }

        public object Clone()
        {
            return new NormalDamageBehavior(Source, TargetTypes, Damage);
        }
    }
}
