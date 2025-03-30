using Assets._Project.Scripts.Enemy.Interface;
using System;
using UnityEngine;

namespace Assets._Project.Scripts.Enemy
{
    public class AttackEnemyFactory : MonoBehaviour
    {
        private Enemy _enemy;
        private IBaseEnemy _baseEnemy;

        public IBaseEnemy BaseEnemy => _baseEnemy;

        private void Start()
        {
            _enemy = GetComponent<Enemy>();
            Get();
        }

        private void Get()
        {
            switch (_enemy)
            {
                case ICommonEnemy:
                    _baseEnemy = new Attacks.CommonEnemy(_enemy);
                    break;

                default:
                    throw new ArgumentException($"There is no such type of attack {_enemy}");
            }
        }
    }
}