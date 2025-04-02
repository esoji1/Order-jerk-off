using UnityEngine;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Core
{
    public class GameButtonManager : MonoBehaviour
    {
        [SerializeField] private Button _characteristics;
        [SerializeField] private GameObject _characteristicsMenu;

        private bool _isOnemCharacteristicsMenu;

        private void OnEnable()
        {
            _characteristics.onClick.AddListener(ShowHaracteristics);
        }

        private void OnDisable()
        {
            _characteristics.onClick.RemoveListener(ShowHaracteristics);
        }

        private void ShowHaracteristics()
        {
            if (_isOnemCharacteristicsMenu == false)
            {
                _characteristicsMenu.SetActive(true);
                _isOnemCharacteristicsMenu = true;
            }
            else if(_isOnemCharacteristicsMenu)
            {
                _characteristicsMenu.SetActive(false);
                _isOnemCharacteristicsMenu = false;
            }
        }
    }
}