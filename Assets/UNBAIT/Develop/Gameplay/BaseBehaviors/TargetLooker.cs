using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public class TargetLooker : MonoBehaviour, IDestinationSetter
    {
        public event Action PositionSet;

        [SerializeField] private FindTargetOnCollision _findTargetOnCollision;

        [SerializeField] private List<Transform> _borders;

        private Vector2 _borderPosition;
        private BaseEntity _entity;

        [SerializeField] private Entity _target;

        private Vector2 GetFurthermostPoint()
        {
            Vector2 furthermostPoint = Vector2.zero;
            float longestDistance = default;

            foreach (Transform vector in _borders)
            {
                float distance = Vector2.Distance(transform.position, vector.position);

                if (distance > longestDistance)
                {
                    longestDistance = distance;
                    furthermostPoint = vector.position;
                }
            }
            return furthermostPoint == default ? throw new ArgumentException("No possible points found") : furthermostPoint;
        }

        private void OnTargetFound(Entity entity)
        {
            if (_target == null)
                _target = entity;

            SetDirection(entity);
        }

        private void SetDirection(Entity entity)
        {
            Vector2 direction = entity == null || _entity.IsNotLookingAt(entity.gameObject) 
                ? MoveInAStraightLine() 
                : GetDirectionTo(entity);

            _entity.Movable.SetDirection(direction);

            PositionSet?.Invoke();
        }

        private Vector2 GetDirectionTo(Entity entity) => entity.transform.position - transform.position;

        private Vector2 MoveInAStraightLine()
        {
            foreach (Transform border in _borders)
            {
                Vector2 direction = (border.position - transform.position).normalized;
                float dot = Vector2.Dot(direction, _entity.Movable.Direction);

                if (dot > 0)
                    return direction;
            }

            throw new ArgumentException($"no valid direction is found");
        }

        private void Update()
        {
            if (_target != null)
            {
                if (_target.GetComponent<Hook>().InUse == false)
                    SetDirection(_target);
            }
        }

        private void OnDestroy()
        {
            _findTargetOnCollision.TargetFound -= OnTargetFound;
            Destroy(gameObject);
        }

        private void Start() => _borderPosition = GetFurthermostPoint();

        private void Awake()
        {
            DestroyColidedObjects[] borders = FindObjectsByType<DestroyColidedObjects>(FindObjectsSortMode.None);//TODO : bad. replace asap
            _borders.AddRange(borders.Select(border => border.transform));//

            _entity = GetComponent<BaseEntity>();

            _findTargetOnCollision.TargetFound += OnTargetFound;
        }
    }
}
