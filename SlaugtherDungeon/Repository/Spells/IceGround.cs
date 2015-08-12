using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using CHWGameEngine;
using CHWGameEngine.GameObject;
using CHWGameEngine.Motion;
using Repository.Buffs;
using Repository.Characters;

namespace Repository.Spells
{
    class IceGround : Spell
    {
        private List<Character> affectedCharacters;
        public double SlowPercentage { get; set; }
        public IceGround(Character source, GameWorld gameWorld, int range) : base(source, gameWorld, range)
        {
            Image = Properties.Resources.IceGround;
            this.MotionBehavior = new StationaryMotionBehavior(this, gameWorld);
            Paralax = Paralax.Background;
            affectedCharacters = new List<Character>();
            Cost = 0;
            gameWorld.Split += gameWorld_Split;
            SlowPercentage = 25;
            Exit();
        }

        private async void Exit()
        {
            await Task.Delay(5000);
            affectedCharacters.ForEach(x => x.TargetSpeed *= (100d / SlowPercentage));
            SpellHandler.Remove(this);
        }

        protected override void MotionBehavior_WallCollision(object sender, EventArgs e)
        {

        }

        //public override void SpellCollision(Spell spell, IGameObject Target)
        //{
        //    if (spell != this) return;
        //    var character = Target as Character;
        //    if (character == null) return;

        //    affectedCharacters.Add(character);
        //    character.TargetSpeed /= (100d / SlowPercentage);
        //}

        public override object Clone()
        {
            throw new NotImplementedException();
        }

        void gameWorld_Split(ref IGameObject object1, ref IGameObject object2)
        {
            Character character = null;

            if (object1 == this)
                character = object2 as Character;
            else if (object2 == this)
                character = object1 as Character;

            if (character == null) return;

            if (affectedCharacters.Contains(character))
            {
                affectedCharacters.Remove(character);
                character.TargetSpeed *= (100d / SlowPercentage);
            }
        }
    }
}
