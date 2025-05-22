using _Project.Inventory;
using _Project.Player;

namespace Assets._Project.ScriptArtifacts
{
    public class BootsSpeed
    {
        private Player _player;
        private InventoryActive _inventoryActive;

        public BootsSpeed(Player player, InventoryActive inventoryActive)
        {
            _player = player;
            _inventoryActive = inventoryActive;
        }

        public void Use()
        {
            _player.PlayerData.Speed = 2;
            _player.PlayerData.DefaultSpeed = 2;
        }

        public void SetActiveArtefact(Cell cell)
        {
            foreach (Cell item in _inventoryActive.CellList)
            {
                if(item.Item != null)
                {
                    if (item.Item.GetItemType().Equals(cell.Item.GetItemType()) && item.NumberItems <= 0)
                    {
                        _player.PlayerData.Speed = 0;
                        _player.PlayerData.DefaultSpeed = 0;
                    }
                }
            }
        }
    }
}