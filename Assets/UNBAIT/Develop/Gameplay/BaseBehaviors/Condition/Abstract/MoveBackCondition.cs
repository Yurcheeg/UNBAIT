using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition.Abstract
{
    public abstract class MoveBackCondition : MonoBehaviour
    {
        public Action ConditionMet;
        public bool IsConditionMet { get; protected set; }

        protected void MeetCondition()
        {
            if (IsConditionMet)
                return;

            IsConditionMet = true;
            ConditionMet?.Invoke();
        }
    }
}
