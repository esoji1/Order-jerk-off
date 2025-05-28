using System;
using UnityEngine;
using _Project.ScriptableObjects;
using _Project.Inventory;

namespace _Project.Potions
{
    public class PotionFactory
    {
        private PotionConfig _explosionConfig, _healingConfig, _speedConfig, _molotovCocktail;
        private Player.Player _player;
        private InventoryActivePotions _inventoryActivePotions;
        private Transform _content;
        private ParticleSystem _bom;
        private Explosion _explosion;
        private ManagerPotion _managerPotion;
        private Molotov _molotov;
        private IncendiaryZone _incendiaryZonePrefab;

        public PotionFactory(PotionConfig explosionConfig, PotionConfig healingConfig, PotionConfig speedConfig, PotionConfig molotovCocktail, Player.Player player, InventoryActivePotions inventoryActivePotions, Transform content,
            ParticleSystem bom, Explosion explosion, ManagerPotion managerPotion, Molotov molotov, IncendiaryZone incendiaryZonePrefab)
        {
            _explosionConfig = explosionConfig;
            _healingConfig = healingConfig;
            _speedConfig = speedConfig;
            _molotovCocktail = molotovCocktail;
            _player = player;
            _inventoryActivePotions = inventoryActivePotions;
            _content = content;
            _bom = bom;
            _explosion = explosion;
            _managerPotion = managerPotion;
            _molotov = molotov;
            _incendiaryZonePrefab = incendiaryZonePrefab;
        }

        public BasePotion Get(TypesPotion potionType)
        {
            PotionConfig config = GetConfigBy(potionType);
            BasePotion instance = UnityEngine.Object.Instantiate(config.Prefab, _content);
            instance.gameObject.transform.SetParent(_content);
            BasePotion basePotion = InitializeObject(instance);
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

                case TypesPotion.SpeedUp:
                    return _speedConfig;

                case TypesPotion.MolotovCocktail:
                    return _molotovCocktail;

                default:
                    throw new ArgumentException(nameof(potionType));
            }
        }

        private BasePotion InitializeObject(BasePotion instance)
        {
            if (instance is HealingPotions healingPotions)
            {
                healingPotions.Initialize(_player, _inventoryActivePotions, _managerPotion);
                return healingPotions;
            }
            else if(instance is ExplosivePotion explosivePotion)
            {
                explosivePotion.Initialize(_player, _inventoryActivePotions, _bom, _explosion, _managerPotion);
                return explosivePotion;
            }
            else if(instance is SpeedPotions speddPotion)
            {
                speddPotion.Initialize(_player, _inventoryActivePotions, _managerPotion);
                return speddPotion;
            }
            else if(instance is MolotovCocktailPotion molotovPotion) 
            {
                molotovPotion.Initialize(_player, _inventoryActivePotions, _molotov, _managerPotion, _incendiaryZonePrefab);
                return molotovPotion;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}