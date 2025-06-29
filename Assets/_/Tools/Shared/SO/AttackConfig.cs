using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Shared
{
    [CreateAssetMenu(fileName = "AttackConfig", menuName = "Scriptable Objects/AttackConfig")]
    public class AttackConfig : ScriptableObject
    {
        [Header("Projectile")]
        public bool m_isProjectile;
        public float m_projectileSpeed;
        public float m_projectileCount;

        [Header("Range Behaviour")]
        public float m_range;
        public float m_angleX;
        public float m_angleY;
        
        [Header("Stats")]
        public int m_damage;
        public float m_cooldown;
    }
}
