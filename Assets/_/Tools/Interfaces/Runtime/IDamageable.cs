using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Interfaces.Runtime
{
    public interface IDamageable
    {
        public int Health { get; }
        public int MaxHealth { get; }

        void TakeDamage(int amount);
        void Heal(int amount);
    }
}
