using UnityEngine;

namespace Assets._Project.Scripts.Inventory
{
    public class Cell : MonoBehaviour
    {
        public Item Item;
        public int NumberItems;
        private bool _IsCellBusy;

        public bool IsCellBusy => _IsCellBusy;

        public void SetIsCellBusy(bool value) => _IsCellBusy = value;
    }
}
