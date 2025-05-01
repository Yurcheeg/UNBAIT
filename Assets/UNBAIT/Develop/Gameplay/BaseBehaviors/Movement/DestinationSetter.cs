using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Movement
{
    public class DestinationSetter : MonoBehaviour
    {
        [SerializeField] private float _startRange;
        [SerializeField] private float _endRange;

        private MovementController _movementController;

        private void Start()
        {
            float randomValue = RandomNumber.GetInRange(_startRange, _endRange);

            _movementController.MoveTo(randomValue);
        }

        private void Awake() => _movementController = GetComponent<MovementController>();
    }
}
