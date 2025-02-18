using Assets.Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay
{
    [RequireComponent(typeof(SpriteRenderer))]
    public sealed class FlipOnClick : MonoBehaviour
    {
        [SerializeField, Min(1)] private int _hitCountToFlip;

        private int _currentHitCount;

        [SerializeField] private Vector2 _direction;

        private SpriteRenderer _spriteRenderer;

        public bool IsFlipped => _spriteRenderer.flipX;

        private void Flip()
        {
            //invert direction
            _direction *= -1;

            _spriteRenderer.flipX = true;
        }

        private void OnTriggerStay2D(Collider2D collision)
        {
            if (IsFlipped)
                return;

            if (collision.gameObject.TryGetComponent<Player>(out _) == false)
                return;

            if (Input.GetMouseButton(0))
                _hitCountToFlip--;

            if (_hitCountToFlip < 0)
                Flip();
        }


        private void Start()
        {
            //find closest hook
            //set normalized dir to this hook pos - fish pos
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }
}
