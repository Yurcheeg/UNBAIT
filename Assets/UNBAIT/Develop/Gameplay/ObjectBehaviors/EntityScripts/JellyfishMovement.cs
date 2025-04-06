using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts
{
    [RequireComponent(typeof(MovingEntity))]
    public class JellyfishMovement : MonoBehaviour
    {
        [SerializeField] private float _moveDuration = 2f;
        [SerializeField] private float _movementCooldown = 2f;

        private float _timer;

        private MovingEntity _entity;

        private void Update()
        {
            _timer -= Time.deltaTime;

            if(_entity.IsMoving)
            {
                if(_timer <= 0f)
                {
                    _entity.IsMoving = false;
                    _timer = _movementCooldown;
                }
            }
            else
            {
                if(_timer <= 0f)
                {
                    _entity.IsMoving = true;
                    _timer = _moveDuration;
                }
            }
        }

        private void Start() => _timer = _moveDuration;

        private void Awake() => _entity = GetComponent<MovingEntity>();
    }
}
