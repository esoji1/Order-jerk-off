using _Project.ConstructionBuildings.Buildings;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.ConstructionBuildings
{
    public class SellBuilding : MonoBehaviour
    {
        private Button _sellButton;
        private BaseBuilding _baseBuilding;

        public void Initialize(BaseBuilding baseBuilding, Button sellButton)
        {
            _baseBuilding = baseBuilding;
            _sellButton = sellButton;

            _sellButton.onClick.AddListener(Sell);
        }

        private void Sell()
        {
            _baseBuilding.Player.Wallet.AddMoney(_baseBuilding.Config.Price);
            _baseBuilding.BuildingArea.SetZoneOccupeid(false);
            _baseBuilding.BuildingArea.SetBaseBuilding(null);
            Destroy(_baseBuilding.gameObject);
        }

        private void OnDestroy()
        {
            _sellButton.onClick.RemoveListener(Sell);
        }
    }
}