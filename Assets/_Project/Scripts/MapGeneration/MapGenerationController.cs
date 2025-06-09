using _Project.CameraMain;
using _Project.Enemy;
using _Project.ScriptableObjects;
using System.Collections.Generic;
using UnityEngine;

namespace _Project.MapGeneration
{
    public class MapGenerationController : MonoBehaviour
    {
        [SerializeField] private EnemyFactoryBootstrap _enemyFactoryBootstrap;
        [SerializeField] private AdaptingColliderResolution _adaptingColliderResolution;
        [SerializeField] private TransitionLevel _transitionLevel;
        [SerializeField] private MapData _mapData;
        [SerializeField] private Player.Player _player;
        private List<Map> _maps;
        private int _currentGenerateMap;

        private void Awake()
        {
            _maps = new List<Map>();
            _currentGenerateMap = 0;
            _transitionLevel.OnForward += SpawnMap;
            _transitionLevel.OnBack += RemoveMap;
        }

        private void OnDestroy()
        {
            _transitionLevel.OnForward -= SpawnMap;
            _transitionLevel.OnBack -= RemoveMap;
            foreach (Map map in _maps)
            {
                map.GetComponentInChildren<TransitionLevel>().OnForward -= SpawnMap;
                map.GetComponentInChildren<TransitionLevel>().OnBack -= RemoveMap;
            }
        }

        private void SpawnMap()
        {
            //ВСЕ ЧТО ТЫ УВИДЕЛ В ЭТОМ МЕТОДЕ, ЗНАЙ ЭТО ЕБЕЙШИЙ 
            if (_currentGenerateMap < 1)
            {
                Map mapFirst = Instantiate(_mapData.Maps[Random.Range(0, _mapData.Maps.Count - 1)], gameObject.transform);
                mapFirst.gameObject.transform.position = _transitionLevel.BackPointB.transform.position;
                TransitionLevel transitionLevelFirst = mapFirst.GetComponentInChildren<TransitionLevel>();
                BattleZone battleZoneFirst = mapFirst.GetComponentInChildren<BattleZone>();
                transitionLevelFirst.Initialize(_adaptingColliderResolution, _player);
                if (battleZoneFirst != null)
                    battleZoneFirst.Initialize(_enemyFactoryBootstrap);
                transitionLevelFirst.OnForward += SpawnMap;
                transitionLevelFirst.OnBack += RemoveMap;
                _transitionLevel.SetStartPoints(mapFirst.PointMapBack.transform, mapFirst.PointMapForward.transform);
                _maps.Add(mapFirst);
                _currentGenerateMap++;
                return;
            }
            Map map = Instantiate(_mapData.Maps[Random.Range(0, _mapData.Maps.Count - 1)], gameObject.transform);
            TransitionLevel transitionLevel = map.GetComponentInChildren<TransitionLevel>();
            TransitionLevel transitionLevelLast = _maps[_maps.Count - 1].GetComponentInChildren<TransitionLevel>();
            BattleZone battleZone = map.GetComponentInChildren<BattleZone>();
            map.gameObject.transform.position = transitionLevelLast.BackPointB.transform.position;
            transitionLevel.Initialize(_adaptingColliderResolution, _player);
            if (battleZone != null)
                battleZone.Initialize(_enemyFactoryBootstrap);
            transitionLevel.OnBack += RemoveMap;
            transitionLevel.OnForward += SpawnMap;
            _maps[_maps.Count - 1].GetComponentInChildren<TransitionLevel>().SetStartPoints(map.PointMapBack.transform, map.PointMapForward.transform);
            _maps.Add(map);
            _currentGenerateMap++;
        }

        private void RemoveMap()
        {
            Destroy(_maps[_maps.Count - 1].gameObject);
            _maps.Remove(_maps[_maps.Count - 1]);
            _currentGenerateMap--;
            if (_currentGenerateMap < 1)
                _currentGenerateMap = 0;
        }
    }
}
