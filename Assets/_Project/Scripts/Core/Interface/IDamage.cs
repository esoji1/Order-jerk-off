using _Project.Core.HealthSystem;
using _Project.ScriptableObjects.Configs;
using _Project.SelectionGags;
using _Project.Weapon.Projectile;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.Core.Interface
{
    public interface IDamage
    {
        void Damage(int damage);
    }
}