using System;
using System.Collections.Generic;
using CHWGameEngine.GameObject;

namespace Repository.Loots
{
    public class Loot
    {
        public List<ILootable> Items { get; private set; }

        public Loot()
        {
            Items = new List<ILootable>();
        }

        public void Drop()
        {
            Random r = new Random();
            foreach(var item in Items)
                if(r.Next(100) < item.DropChance)
                    item.Drop();
        }
    }
}
