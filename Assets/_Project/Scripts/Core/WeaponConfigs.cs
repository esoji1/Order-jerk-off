using Assets._Project.Scripts.ScriptableObjects.Configs;
using UnityEngine;

namespace Assets._Project.Sctipts.Core
{
    public class WeaponConfigs : MonoBehaviour
    {
        [Header("Player")]
        [SerializeField] private WeaponConfig _woodenSwordPlayerConfig;
        [SerializeField] private WeaponConfig _woodenAxePlayerConfig;

        [Header("Enemy")]
        [SerializeField] private WeaponConfig _woodenSwordEnemyConfig;
        [SerializeField] private WeaponConfig _woodenAxeEnemyConfig;

        public static WeaponConfig WoodenSwordPlayerConfig { get; private set; }
        public static WeaponConfig WoodenSwordEnemyConfig { get; private set; }
        public static WeaponConfig WoodenAxePlayerConfig { get; private set; }
        public static WeaponConfig WoodenAxeEnemyConfig { get; private set; }

        private void Awake()
        {
            WoodenSwordPlayerConfig = _woodenSwordPlayerConfig;
            WoodenSwordEnemyConfig = _woodenSwordEnemyConfig;
            WoodenAxePlayerConfig = _woodenAxePlayerConfig;
            WoodenAxeEnemyConfig = _woodenAxeEnemyConfig;
        }
    }
}