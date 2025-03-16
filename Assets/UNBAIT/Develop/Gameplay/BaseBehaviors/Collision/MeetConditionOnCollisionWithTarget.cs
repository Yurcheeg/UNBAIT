using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public class MeetConditionOnCollisionWithTarget : MonoBehaviour, IConditionMeetable //TODO: awful name
    {
        public event Action ConditionMet;

        [SerializeField] private EntityType _collisionTarget;
        
        public bool IsConditionMet { get; private set; }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (IsConditionMet)
                return;

            if (collision.gameObject.TryGetComponent(out Entity entity) == false)
                return;

            if (Target.GetType(_collisionTarget) == entity.GetType())
            {
                IsConditionMet = true;
                ConditionMet?.Invoke();
            }
        }
    }
}
