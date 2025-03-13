using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    [RequireComponent(typeof(Movable))]
    public class RotateOnChangedDirection : MonoBehaviour
    {
        private Movable _movable;

        [SerializeField] private Rotatable _rotatable;

        public void OnDirectionChanged(Vector2 direction) => _rotatable.RotateTowards(direction);

        private void OnDisable() => _movable.DirectionChanged -= OnDirectionChanged;
        
        private void Awake()
        {
            _movable = GetComponent<Movable>();
            _movable.DirectionChanged += OnDirectionChanged;
        }
    }
}
