using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay
{
    public class Movable : MonoBehaviour
    {
        [SerializeField] private int _speed;
        [SerializeField] private Vector2 _direction;

        public void Move() => transform.Translate(_speed * Time.deltaTime * _direction);

        public void SetDirection(Vector2 direction) => _direction = direction.normalized;

        private void Awake() => _direction.Normalize();
    }
}
