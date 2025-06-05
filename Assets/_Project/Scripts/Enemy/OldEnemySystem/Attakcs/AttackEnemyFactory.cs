//using _Project.Enemy.Attack.Interface;
//using _Project.Enemy.Attakcs.Interface;
//using System;
//using UnityEngine;

//namespace _Project.Enemy.Attakcs
//{
//    public class AttackEnemyFactory : MonoBehaviour
//    {
//        private Enemys.Enemy _enemy;

//        private IBaseAttack _attack;

//        private void Start()
//        {
//            _enemy = GetComponent<Enemys.Enemy>();
//            Get();
//        }

//        private void Update() => _attack.Update();

//        private void Get()
//        {
//            switch (_enemy)
//            {
//                case IMelee normalAttack:
//                    _attack = new MeleeNormalAttack(_enemy);
//                    break;

//                case IRanged rangedAttack:
//                    _attack = new Ranged(_enemy);
//                    break;

//                case IHeavyAttack heavyAttack:
//                    _attack = new HeavyAttack(_enemy);
//                    break;

//                default:
//                    throw new ArgumentException($"There is no such type of attack {nameof(_enemy)}");
//            }
//        }

//        private void OnDestroy()
//        {
//            _attack.OnDestroy();
//        }
//    }
//}
