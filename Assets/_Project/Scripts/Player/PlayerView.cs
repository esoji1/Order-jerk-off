using UnityEngine;

namespace Assets._Project.Scripts.Player
{
    public class PlayerView : MonoBehaviour
    {
        private SpriteRenderer _spriteRenderer;

        public SpriteRenderer SpriteRenderer => _spriteRenderer;

        public void Initialize()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
