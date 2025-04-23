using UnityEngine;

namespace Assets._Project.Scripts.Weapon.Projectile
{
    public class SpawnProjectile
    {
        public GameObject ProjectileSpawnPoint(GameObject projectile, Vector2 direction, Transform point)
        {
            GameObject bullet = Object.Instantiate(projectile, point.position, Quaternion.identity, null);

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            bullet.transform.rotation = Quaternion.Euler(0f, 0f, angle);

            return bullet;
        }
    }
}