using Assets.Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public class StopOnRandomPoint : MonoBehaviour, IDestinationSetter
    {
        public event Action PositionSet;
        public event Action PositionReached;

        [SerializeField] private float _startP;
        [SerializeField] private float _endP;

        [Space]

        [SerializeField] private MovementConstrains _movementConstrains;

        private float _position;

        private float _targetPosition;

        private Vector3 _startPos;

        private Hook _hook;

        public bool HasReachedPosition { get; private set; } = false;

        //TODO pls replace the region
        #region "IT DOESN'T EVEN WORK"

        private IEnumerator MoveToTarget()
        {
            yield return new WaitUntil(() => HasReachedTargetPosition(_targetPosition));

            HasReachedPosition = true;
            PositionReached?.Invoke();

            yield return new WaitUntil(() => _hook != null && _hook.InUse);

            StartCoroutine(MoveBack());
        }

        private IEnumerator MoveBack()//todo replace
        {
            var entity = GetComponent<BaseEntity>();
            entity.Movable.SetDirection(_startPos - transform.position);
            entity.IsMoving = true;

            float startValue = _movementConstrains.GetLargestAxis(_startPos);
            yield return new WaitUntil(() => HasReachedTargetPosition(startValue));
            entity.IsMoving = false;
        }

        private bool HasReachedTargetPosition(float targetPosition)
        {
            _position = _movementConstrains.GetLargestAxis(transform.position);
            return Mathf.Abs(_position - targetPosition) < 1f;
        }

        #endregion

        private void Awake() => _hook = GetComponent<Hook>();

        private void Start()
        {
            _startPos = transform.position;

            _targetPosition = RandomNumber.GetInRange(_startP, _endP);

            PositionSet?.Invoke();
            StartCoroutine(MoveToTarget());
        }
    }
}
