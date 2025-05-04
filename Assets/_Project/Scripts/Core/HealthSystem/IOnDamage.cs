using System;

namespace _Project.Core.HealthSystem
{
    public interface IOnDamage
    {
        event Action<int> OnDamage;
        PointHealth PointHealth { get; }    
    }
}