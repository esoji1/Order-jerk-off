using _Project.CameraMain;
using _Project.Enemy;
using _Project.MapGeneration.Food;
using _Project.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.MapGeneration
{
    public class MapGenerationController : MonoBehaviour
    {
        private const int MaxAmountGenerateMapWithBoss = 10;
        private const int MinAmountGenerateMapWithBoss = 3;

        [SerializeField] private EnemyFactoryBootstrap _enemyFactoryBootstrap;
        [SerializeField] private AdaptingColliderResolution _adaptingColliderResolution;
        [SerializeField] private TransitionLevel _transitionLevel;
        [SerializeField] private MapData _mapData;
        [SerializeField] private Player.Player _player;
        [SerializeField] private FoodView _foodView;

        private int _amountGenerateMapWithBoss;
        private List<Map> _maps;
        private int _currentGenerateMap;

        private void Awake()
        {
            _maps = new List<Map>();
            _currentGenerateMap = 0;
            _amountGenerateMapWithBoss = Random.Range(MinAmountGenerateMapWithBoss, MaxAmountGenerateMapWithBoss);
            _transitionLevel.OnForward += SpawnMap;
            _transitionLevel.OnBack += RemoveMap;
        }

        private void OnDestroy()
        {
            _transitionLevel.OnForward -= SpawnMap;
            _transitionLevel.OnBack -= RemoveMap;
            foreach (Map map in _maps)
            {
                TransitionLevel transitionLevel = map.GetComponentInChildren<TransitionLevel>();
                if (transitionLevel != null)
                {
                    transitionLevel.OnForward -= SpawnMap;
                    transitionLevel.OnBack -= RemoveMap;
                }
            }
        }

        public void DestroyAllMap()
        {
            if (_maps.Count <= 0)
                return;

            foreach (Map map in _maps)
                Destroy(map.gameObject);

            _currentGenerateMap = 0;
            _maps.Clear();
        }

        private void SpawnMap()
        {
            //ВСЕ ЧТО ТЫ УВИДЕЛ В ЭТОМ МЕТОДЕ, ЗНАЙ ЭТО ЕБЕЙШИЙ 

            if (_foodView.AmauntFood <= 0)
                return;

            if (_currentGenerateMap < 1)
            {
                Map mapFirst = Instantiate(_mapData.Maps[Random.Range(0, _mapData.Maps.Count - 1)], gameObject.transform);
                mapFirst.gameObject.transform.position = _transitionLevel.BackPointB.transform.position;
                TransitionLevel transitionLevelFirst = mapFirst.GetComponentInChildren<TransitionLevel>();
                BattleZone battleZoneFirst = mapFirst.GetComponentInChildren<BattleZone>();
                transitionLevelFirst.Initialize(_adaptingColliderResolution, _player, _foodView);
                if (battleZoneFirst != null)
                    battleZoneFirst.Initialize(_enemyFactoryBootstrap);
                transitionLevelFirst.OnForward += SpawnMap;
                transitionLevelFirst.OnBack += RemoveMap;
                _transitionLevel.SetStartPoints(mapFirst.PointMapBack.transform, mapFirst.PointMapForward.transform);
                _maps.Add(mapFirst);
                _currentGenerateMap++;
                _foodView.SubtractFood(1);
                return;
            }
            else if (_amountGenerateMapWithBoss <= _currentGenerateMap)
            {
                Map mapBoss = Instantiate(_mapData.Maps[_mapData.Maps.Count - 1], gameObject.transform);
                TransitionLevel transitionLevelLastBoss = _maps[_maps.Count - 1].GetComponentInChildren<TransitionLevel>();
                BattleZone battleZoneBoss = mapBoss.GetComponentInChildren<BattleZone>();
                mapBoss.gameObject.transform.position = transitionLevelLastBoss.BackPointB.transform.position;
                if (battleZoneBoss != null)
                    battleZoneBoss.Initialize(_enemyFactoryBootstrap);
                _maps[_maps.Count - 1].GetComponentInChildren<TransitionLevel>().SetStartPoints(mapBoss.PointMapBack.transform, mapBoss.PointMapForward.transform);
                _maps.Add(mapBoss);
                _foodView.SubtractFood(1);
                return;
            }
            Map map = Instantiate(_mapData.Maps[Random.Range(0, _mapData.Maps.Count - 1)], gameObject.transform);
            TransitionLevel transitionLevel = map.GetComponentInChildren<TransitionLevel>();
            TransitionLevel transitionLevelLast = _maps[_maps.Count - 1].GetComponentInChildren<TransitionLevel>();
            BattleZone battleZone = map.GetComponentInChildren<BattleZone>();
            map.gameObject.transform.position = transitionLevelLast.BackPointB.transform.position;
            transitionLevel.Initialize(_adaptingColliderResolution, _player, _foodView);
            if (battleZone != null)
                battleZone.Initialize(_enemyFactoryBootstrap);
            transitionLevel.OnBack += RemoveMap;
            transitionLevel.OnForward += SpawnMap;
            _maps[_maps.Count - 1].GetComponentInChildren<TransitionLevel>().SetStartPoints(map.PointMapBack.transform, map.PointMapForward.transform);
            _maps.Add(map);
            _currentGenerateMap++;
            _foodView.SubtractFood(1);
        }

        private void RemoveMap()
        {
            if (_foodView.AmauntFood <= 0)
                return;

            if (_maps[_maps.Count - 1].Type.Equals(MapType.MapWithBoss))
            {
                Destroy(_maps[_maps.Count - 1].gameObject);
                _maps.Remove(_maps[_maps.Count - 1]);
                _foodView.SubtractFood(1);
                return;
            }
            else
            {
                Destroy(_maps[_maps.Count - 1].gameObject);
                _maps.Remove(_maps[_maps.Count - 1]);
                _currentGenerateMap--;
                _foodView.SubtractFood(1);
                if (_currentGenerateMap < 1)
                    _currentGenerateMap = 0;
            }
        }
    }
}
