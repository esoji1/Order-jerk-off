using Assets._Project.Scripts.ActionButton;
using Assets._Project.Scripts.Player;
using Assets._Project.Scripts.ResourceExtraction.OreMining;
using Assets._Project.Scripts.UseWeapons;
using UnityEngine;

namespace Assets._Project.Sctipts.ResourceExtraction
{
    public class ManagerMining : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private UseWeapons _useWeapons;
        [SerializeField] private ActionButton _actionButton;
        [SerializeField] private MiningFactoryBootstrap _mineringFactoryBootstrap;

        private BaseMining _mining;

        private void OnEnable()
        {
            _actionButton.OnMining += StartMining;
        }

        private void OnDisable()
        {
            _actionButton.OnMining -= StartMining;
        }

        private void StartMining(Ore ore)
        {

        }
    }
}
