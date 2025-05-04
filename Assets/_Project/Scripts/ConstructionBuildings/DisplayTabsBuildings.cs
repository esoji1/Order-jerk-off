using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.ConstructionBuildings
{
    public class DisplayTabsBuildings : MonoBehaviour
    {
        [SerializeField] private GameObject _firstWindow;
        [SerializeField] private GameObject _twoWindow;
        [SerializeField] private Button _firstButton;
        [SerializeField] private Button _twoButtons;
        [SerializeField] private List<GameObject> _listWindows;

        private void OnEnable()
        {
            _firstButton.onClick.AddListener(HideFirst);
            _twoButtons.onClick.AddListener(HideTwo);
        }

        private void OnDisable()
        {
            _firstButton.onClick.RemoveListener(HideFirst);
            _twoButtons.onClick.RemoveListener(HideTwo);
        }

        private void HideFirst() => SetWindowState(_firstWindow);
        private void HideTwo() => SetWindowState(_twoWindow);

        private void SetWindowState(GameObject window)
        {
            foreach (GameObject item in _listWindows)
            {
                if (item == window)
                    window.SetActive(true);
                if (item != window)
                    item.SetActive(false);
            }
        }
    }
}
