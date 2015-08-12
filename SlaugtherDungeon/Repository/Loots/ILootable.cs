using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CHWGameEngine.GameObject;

namespace Repository.Loots
{
    public interface ILootable : IGameObject
    {
        double DropChance { get; set; }
        void Drop();
    }
}
