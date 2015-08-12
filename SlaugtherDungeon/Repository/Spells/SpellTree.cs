using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Repository.Characters;

namespace Repository.Spells
{
    class SpellTree
    {
        private readonly Character character;
        public Dictionary<Type, Func<Spell>> AttackMap;
        private Dictionary<Type, int> spells;

        public SpellTree(Character character)
        {
            this.character = character;
            AttackMap = new Dictionary<Type, Func<Spell>>();
            spells = new Dictionary<Type, int>();
            LoadSpells();
        }

        private void LoadSpells()
        {
            spells.Add(typeof(Fireball), 1);
            spells.Add(typeof(Frostball), 2);
        }
    }
}
