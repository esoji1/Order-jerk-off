using System;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings.Buildings
{
    public abstract class BaseBuilding : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;
        private Type _type;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Type Type => _type;

        private void Awake()
        {
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _type = GetType();
        }
    }
}
