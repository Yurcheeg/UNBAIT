using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts
{
    [RequireComponent(typeof(StopOnRandomPoint))]
    public class DestinationMover : MonoBehaviour
    {
        private StopOnRandomPoint _randomAxisMover;
        private BaseEntity _baseEntity;

        private void OnPositionSet() => _baseEntity.IsMoving = true;

        private void OnPositionReached() => _baseEntity.IsMoving = false;
        
        private void OnEnable() => _randomAxisMover.PositionReached += OnPositionReached;

        private void OnDisable()
        {
            _randomAxisMover.PositionReached -= OnPositionReached;

            _randomAxisMover.PositionSet += OnPositionSet;
        }
        
        private void Awake()
        {
            _randomAxisMover = GetComponent<StopOnRandomPoint>();
            _baseEntity = GetComponent<BaseEntity>();

            _randomAxisMover.PositionSet += OnPositionSet;
        }

    }
}
