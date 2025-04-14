using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition.Abstract;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System.Collections;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition
{
    public class TiredCondition : MoveBackCondition
    {
        private Fisherman _fisherman;

        public IEnumerator WaitUntilTired()
        {
            yield return new WaitUntil(() => _fisherman.IsTired);

            MeetCondition();
        }

        public void Awake()
        {
            _fisherman = GetComponent<Fisherman>();

            StartCoroutine(WaitUntilTired());
        }
    }
}
