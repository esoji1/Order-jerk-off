using UnityEngine;

namespace _Project.Player
{
    public class PlayerView : MonoBehaviour
    {
        [SerializeField] private Sprite _back;
        [SerializeField] private Sprite _front;

        private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Sprite Back => _back;
        public Sprite Front => _front;

        public void Initialize()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
