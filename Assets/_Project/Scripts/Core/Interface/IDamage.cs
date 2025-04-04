using Assets._Project.Scripts.Enemy;
using Assets._Project.Scripts.ScriptableObjects.Configs;
using Assets._Project.Scripts.SelectionGags;
using Assets._Project.Scripts.Weapon;
using Assets._Project.Sctipts.Core.HealthSystem;
using UnityEngine;

namespace Assets._Project.Scripts.Core.Interface
{
    public interface IDamage
    {
        void Damage(int damage);
    }
}