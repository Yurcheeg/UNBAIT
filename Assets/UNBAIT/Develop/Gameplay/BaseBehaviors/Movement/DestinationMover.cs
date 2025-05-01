using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Movement
{
    [RequireComponent(typeof(MovementController))]
    public class DestinationMover : MonoBehaviour
    {
        private MovementController _movementController;
        private MovingEntity _entity;

        private void OnPositionSet() => _entity.IsMoving = true;

        private void OnPositionReached() => _entity.IsMoving = false;

        private void OnEnable()
        {
            _movementController.PositionReached += OnPositionReached;
            _movementController.PositionSet += OnPositionSet;
        }

        private void OnDisable()
        {
            _movementController.PositionReached -= OnPositionReached;
            _movementController.PositionSet -= OnPositionSet;
        }
        
        private void Awake()
        {
            _movementController = GetComponent<MovementController>();
            _entity = GetComponent<MovingEntity>();
        }

    }
}
