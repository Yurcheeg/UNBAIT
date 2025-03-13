using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public sealed class Movable : MonoBehaviour
    {
        public event Action<Vector2> DirectionChanged;
        [SerializeField] private int _speed;
        [SerializeField] private Vector2 _direction;

        public Vector2 Direction => _direction;

        public void Move() => transform.Translate(_speed * Time.deltaTime * _direction);

        public void SetDirection(Vector2 direction)
        {
            _direction = direction.normalized;
            DirectionChanged?.Invoke(_direction);
        }
    }
}
