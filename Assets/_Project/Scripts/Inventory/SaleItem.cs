using Assets._Project.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Sctipts.Inventory
{
    public class SaleItem : MonoBehaviour
    {
        [SerializeField] private Button _sell;
        [SerializeField] private Inventory _inventory;

        private Player _player;

        public void Initialize(Player player)
        {
            _player = player;
        }
    }
}