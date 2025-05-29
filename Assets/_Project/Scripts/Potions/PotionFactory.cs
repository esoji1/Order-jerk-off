using System;
using UnityEngine;
using _Project.ScriptableObjects;
using _Project.Inventory;
using _Project.Potions.Projectile;
using TMPro;

namespace _Project.Potions
{
    public class PotionFactory
    {
        private PotionConfig _explosionConfig, _healingConfig, _speedConfig, _molotovCocktail, _invisibilityConfig, _sleepingConfig;
        private Player.Player _player;
        private InventoryActivePotions _inventoryActivePotions;
        private Transform _content;
        private ParticleSystem _bom;
        private Explosion _explosion;
        private ManagerPotion _managerPotion;
        private Molotov _molotov;
        private IncendiaryZone _incendiaryZonePrefab; 
        private TextMeshProUGUI _textDamage;
        private Canvas _dynamic;

        public PotionFactory(PotionConfig explosionConfig, PotionConfig healingConfig, PotionConfig speedConfig, PotionConfig molotovCocktail,
            PotionConfig invisibilityConfig, PotionConfig sleepingConfig, Player.Player player, InventoryActivePotions inventoryActivePotions, Transform content,
            ParticleSystem bom, Explosion explosion, ManagerPotion managerPotion, Molotov molotov, IncendiaryZone incendiaryZonePrefab, TextMeshProUGUI textDamage,
            Canvas dynamic)
        {
            _explosionConfig = explosionConfig;
            _healingConfig = healingConfig;
            _speedConfig = speedConfig;
            _molotovCocktail = molotovCocktail;
            _invisibilityConfig = invisibilityConfig;
            _sleepingConfig = sleepingConfig;
            _player = player;
            _inventoryActivePotions = inventoryActivePotions;
            _content = content;
            _bom = bom;
            _explosion = explosion;
            _managerPotion = managerPotion;
            _molotov = molotov;
            _incendiaryZonePrefab = incendiaryZonePrefab;
            _textDamage = textDamage;
            _dynamic = dynamic;
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

                case TypesPotion.Invisibility:
                    return _invisibilityConfig;

                case TypesPotion.Sleeping:
                    return _sleepingConfig;

                default:
                    throw new ArgumentException(nameof(potionType));
            }
        }

        private BasePotion InitializeObject(BasePotion instance)
        {
            if (instance is HealingPotions healingPotion)
            {
                healingPotion.Initialize(_player, _inventoryActivePotions, _managerPotion);
                return healingPotion;
            }
            else if(instance is ExplosivePotion explosivePotion)
            {
                explosivePotion.Initialize(_player, _inventoryActivePotions, _bom, _explosion, _managerPotion, _textDamage, _dynamic);
                return explosivePotion;
            }
            else if(instance is SpeedPotions speddPotion)
            {
                speddPotion.Initialize(_player, _inventoryActivePotions, _managerPotion);
                return speddPotion;
            }
            else if(instance is MolotovCocktailPotion molotovPotion) 
            {
                molotovPotion.Initialize(_player, _inventoryActivePotions, _molotov, _managerPotion, _incendiaryZonePrefab, _textDamage, _dynamic);
                return molotovPotion;
            }
            else if(instance is InvisibilityPotion invisibilityPotion)
            {
                invisibilityPotion.Initialize(_player, _inventoryActivePotions, _managerPotion);
                return invisibilityPotion;
            }
            else if (instance is SleepingPotion sleepingPotion)
            {
                sleepingPotion.Initialize(_player, _inventoryActivePotions, _managerPotion);
                return sleepingPotion;
            }
            else
            {
                throw new ArgumentException(nameof(instance));
            }
        }
    }
}