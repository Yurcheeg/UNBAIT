using System;
using UnityEngine;
using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Collision;
using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Movement
{
    public class TargetTracker : MonoBehaviour
    {
        public event Action<Entity> TargetFound;

        [SerializeField] private FindHookOnCollision _findHookOnCollision;

        private Entity _currentTarget;
        public Entity CurrentTarget
        {
            get => _currentTarget;
            private set
            {
                if (_currentTarget != value)
                {
                    _currentTarget = value;
                    TargetFound?.Invoke(_currentTarget);
                }
            }
        }

        public void SetTarget(Entity target) => CurrentTarget = target;

        public void ClearTarget() => CurrentTarget = null;
    }
}
