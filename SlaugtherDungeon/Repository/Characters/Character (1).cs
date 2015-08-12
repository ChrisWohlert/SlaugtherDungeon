using SlaugtherDungeon.Energies;
using SlaugtherDungeon.Spells;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlaugtherDungeon.Characters
{
    class Character
    {
        public int Experience { get; set; }
        public int Level { get; private set; }
        public int Health { get; set; }
        public Energy Energy { get; set; }
        public List<Spell> AllowedSpells { get; set; }
        private Spell activeSpell;
        public Spell ActiveSpell
        {
            get { return activeSpell; }
            set
            {
                if (AllowedSpells.Contains(value))
                    activeSpell = value;
                else
                    throw new InvalidOperationException();
            }
        }

        public void Walk()
        {

        }

        public void UseSpell()
        {

        }
    }
}
