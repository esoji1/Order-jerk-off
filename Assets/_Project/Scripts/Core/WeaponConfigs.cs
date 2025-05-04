using _Project.ScriptableObjects.Configs;
using UnityEngine;

namespace _Project.Core
{
    public class WeaponConfigs : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private WeaponConfig _woodenSwordPlayerConfig;
        [SerializeField] private WeaponConfig _woodenAxePlayerConfig;
        [SerializeField] private WeaponConfig _weaponOnionPlayerConfig;

        [Header("Enemy")]
        [SerializeField] private WeaponConfig _woodenSwordEnemyConfig;
        [SerializeField] private WeaponConfig _woodenAxeEnemyConfig;
        [SerializeField] private WeaponConfig _weaponOnionEnemyConfig;

        public static WeaponConfig WoodenSwordEnemyConfig { get; private set; }
        public static WeaponConfig WoodenAxeEnemyConfig { get; private set; }
        public static WeaponConfig WeaponOnionEnemyConfig { get; private set; }

        public static WeaponConfig WoodenSwordPlayerConfig { get; private set; }
        public static WeaponConfig WoodenAxePlayerConfig { get; private set; }
        public static WeaponConfig WeaponOnionPlayerConfig { get; private set; }

        private void Awake()
        {
            WoodenSwordEnemyConfig = _woodenSwordEnemyConfig;
            WoodenAxeEnemyConfig = _woodenAxeEnemyConfig;
            WeaponOnionEnemyConfig = _weaponOnionEnemyConfig;

            WoodenSwordPlayerConfig = _woodenSwordPlayerConfig;
            WoodenAxePlayerConfig = _woodenAxePlayerConfig;
            WeaponOnionPlayerConfig = _weaponOnionPlayerConfig;
        }
    }
}