using System;
using UnityEngine;
using _Project.ScriptableObjects;
using _Project.Inventory;

namespace _Project.Potions
{
    public class PotionFactory
    {
        private PotionConfig _explosionConfig, _healingConfig;
        private Player.Player _player;
        private InventoryActivePotions _inventoryActivePotions;
        private Transform _content;
        private ParticleSystem _bom;
        private Explosion _explosion;

        public PotionFactory(PotionConfig explosionConfig, PotionConfig healingConfig, Player.Player player, InventoryActivePotions inventoryActivePotions, Transform content,
            ParticleSystem bom, Explosion explosion)
        {
            _explosionConfig = explosionConfig;
            _healingConfig = healingConfig;
            _player = player;
            _inventoryActivePotions = inventoryActivePotions;
            _content = content;
            _bom = bom;
            _explosion = explosion;
        }

        public BasePotion Get(TypesPotion potionType)
        {
            PotionConfig config = GetConfigBy(potionType);
            BasePotion instance = UnityEngine.Object.Instantiate(config.Prefab, _content);
            instance.gameObject.transform.SetParent(_content);
            BasePotion basePotion = InitializeObject(instance, config);
            return basePotion;
        }

        private PotionConfig GetConfigBy(TypesPotion potionType)
        {
            switch (potionType)
            {
                case TypesPotion.Hilka:
                    return _healingConfig;

                case TypesPotion.Explosion:
                    return _explosionConfig;

                default:
                    throw new ArgumentException(nameof(potionType));
            }
        }

        private BasePotion InitializeObject(BasePotion instance, PotionConfig config)
        {
            if (instance is HealingPotions healingPotions)
            {
                healingPotions.Initialize(_player, _inventoryActivePotions);
                return healingPotions;
            }
            else if(instance is ExplosivePotion explosivePotion)
            {
                explosivePotion.Initialize(_player, _inventoryActivePotions, _bom, _explosion);
                return explosivePotion;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}