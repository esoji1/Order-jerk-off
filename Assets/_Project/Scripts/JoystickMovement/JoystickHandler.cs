using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Project.JoystickMovement
{
    public abstract class JoystickHandler : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Image _joystickBackground;
        [SerializeField] private Image _joystick;
        [SerializeField] private Image _joystickArea;

        protected Vector2 InputVector;

        private Vector2 _joystickBackgroundStartPosition;

        private void Start() =>
            _joystickBackgroundStartPosition = _joystickBackground.rectTransform.anchoredPosition;

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 joystickPosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickBackground.rectTransform,
                eventData.position, null, out joystickPosition))
            {
                joystickPosition.x = joystickPosition.x * 2 / _joystickBackground.rectTransform.sizeDelta.x;
                joystickPosition.y = joystickPosition.y * 2 / _joystickBackground.rectTransform.sizeDelta.y;

                InputVector = new Vector2(joystickPosition.x, joystickPosition.y);

                InputVector = InputVector.magnitude > 1f ? InputVector.normalized : InputVector;

                _joystick.rectTransform.anchoredPosition = new Vector2(InputVector.x * (_joystickBackground.rectTransform.sizeDelta.x / 2),
                    InputVector.y * (_joystickBackground.rectTransform.sizeDelta.y / 2));
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            Vector2 joystickBackgroundPosition;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(_joystickArea.rectTransform,
                eventData.position, null, out joystickBackgroundPosition))
            {
                _joystickBackground.rectTransform.anchoredPosition = new Vector2(joystickBackgroundPosition.x, joystickBackgroundPosition.y);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _joystickBackground.rectTransform.anchoredPosition = _joystickBackgroundStartPosition;

            InputVector = Vector2.zero;
            _joystick.rectTransform.anchoredPosition = Vector2.zero;
        }
    }
}