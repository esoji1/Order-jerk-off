using Assets._Project.Scripts.Player.Pumping;
using Assets._Project.Sctipts.Inventory;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace Assets._Project.Scripts.ConstructionBuildings.Buildings
{
    public class House : BaseBuilding
    {
        private CharacteristicsView _characteristicsView;
        private ChangeItem _changeItem;
        private SaleItem _saleItem;
        private UseWeapons.UseWeapons _useWeapons;

        public void Initialize(GameObject playerHomeMenu, Canvas staticCanvas, Player.Player player, UseWeapons.UseWeapons useWeapons)
        {
            base.Initialize(playerHomeMenu, staticCanvas, player);

            _useWeapons = useWeapons;
            _characteristicsView = Window.GetComponentInChildren<CharacteristicsView>();
            _characteristicsView.Initialize(Player);
            _changeItem = Window.GetComponentInChildren<ChangeItem>();
            _changeItem.Initialize(_useWeapons);
            _saleItem = Window.GetComponentInChildren<SaleItem>();
            _saleItem.Initialize(Player);
        }
    }
}