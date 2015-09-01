using System;
using System.Collections.Generic;
using System.Reflection;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using CHWGameEngine.Motion;
using Repository.Characters;
using Repository.DamageBehavior;

namespace Repository.Spells {
    public class SpellHandler {
        #region Properties
        public Dictionary<Type, Cooldown> Cooldowns { get; set; }
        public GameWorld GameWorld { get; set; }
        public Character Source { get; set; }
        public List<Spell> Spells { get; set; }

        #endregion

        public SpellHandler(GameWorld gameWorld, Character source) {
            this.GameWorld = gameWorld;
            this.Source = source;
            Init();
        }

        private void Init() {
            Cooldowns = new Dictionary<Type, Cooldown>();
            Spells = new List<Spell>();
        }

        public void Cast(Spell spell) {
            if (Source.Target != null)
                if (GameWorld.GetDistance(Source.Location, Source.Target.Location) > spell.Range) return;
            if (!Cooldowns[spell.GetType()].Start()) return;
            if (!Source.Energy.Use(spell.Cost)) return;
            spell.Cast();
        }

        public void Remove(Spell spell) {
            GameWorld.Remove(spell);
            Spells.Remove(spell);
        }
    }
}
