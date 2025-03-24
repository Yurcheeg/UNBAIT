using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition.Abstract;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System.Collections;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition
{
    public class TiredCondition : MoveBackCondition
    {
        private Fisherman _fisherman;

        public IEnumerator TiredMeterEmpty()
        {
            yield return new WaitUntil(() => _fisherman.IsTired);//TODO: update after decided on tired logic

            MeetCondition();
        }

        public void Awake()
        {
            _fisherman = GetComponent<Fisherman>();

            if (_fisherman == null)
                throw new System.ArgumentNullException("Fisherman is null");
        }
    }
}
