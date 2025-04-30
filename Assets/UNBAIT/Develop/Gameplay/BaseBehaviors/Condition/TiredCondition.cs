using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition.Abstract;
using Assets.UNBAIT.Develop.Gameplay.Entities;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition
{
    public class TiredCondition : ReturnCondition
    {
        private Fisherman _fisherman;

        public IEnumerator WaitUntilTiredAndHookReturned()
        {
            yield return new WaitUntil(() => _fisherman.IsTired);
            if (_fisherman.Hook != null && _fisherman.Hook.TryGetComponent<TriggerCondition>(out TriggerCondition condition))
            {
                condition.Trigger();
                yield return new WaitUntil(() => _fisherman.Hook == null);
            }

            MeetCondition();
        }

        public void Awake()
        {
            _fisherman = GetComponent<Fisherman>();

            StartCoroutine(WaitUntilTiredAndHookReturned());
        }
    }
}
