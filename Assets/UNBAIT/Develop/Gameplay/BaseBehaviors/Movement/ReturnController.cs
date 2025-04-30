using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition.Abstract;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Movement
{
    public class ReturnController : MonoBehaviour
    {
        [SerializeField] private float _returnDelaySeconds = 0.5f;

        private MovementController _movementController;
        private float _startPositionValue;

        private List<ReturnCondition> _conditions;
        private bool _hasReturned;

        private void OnConditionMet()
        {
            if (_hasReturned)
                return;

            _hasReturned = true;
            StartCoroutine(ReturnCoroutine());
        }

        private IEnumerator ReturnCoroutine()
        {
            yield return new WaitForSeconds(_returnDelaySeconds);

            _movementController.MoveTo(_startPositionValue);

            _movementController.PositionReached += OnPositionReached;
        }

        private void OnPositionReached()
        {
            _movementController.PositionReached -= OnPositionReached;

            Destroy(gameObject);
        }

        private void Start()
        {
            foreach (var condition in _conditions)
                condition.ConditionMet += OnConditionMet;
        }

        private void OnDestroy()
        {
            foreach (var condition in _conditions)
                condition.ConditionMet -= OnConditionMet;

            _movementController.PositionReached -= OnPositionReached; //in case position wasn't reached
        }

        private void Awake()
        {
            _movementController = GetComponent<MovementController>();
            _conditions = GetComponents<ReturnCondition>().ToList();

            _movementController.PositionSet += OnPositionSet;
        }

        private void OnPositionSet()
        {
            _startPositionValue = _movementController.MovementAxis switch
            {
                MovementAxis.X => transform.position.x,
                MovementAxis.Y => transform.position.y,
                _ => throw new ArgumentException("No valid axis found")
            };

            _movementController.PositionSet -= OnPositionSet;
        }
    }
}
