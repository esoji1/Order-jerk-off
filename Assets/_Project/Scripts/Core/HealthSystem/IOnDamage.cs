using Assets._Project.Scripts.Core.HealthSystem;
using System;

namespace Assets._Project.Sctipts.Core.HealthSystem
{
    public interface IOnDamage
    {
        event Action<int> OnDamage;
        PointHealth PointHealth { get; }    
    }
}