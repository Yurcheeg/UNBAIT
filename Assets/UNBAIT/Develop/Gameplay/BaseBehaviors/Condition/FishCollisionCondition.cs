using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition.Abstract;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition
{
    public class FishCollisionCondition : ReturnCondition//TODO: replace
    {
        private Fisherman _fisherman;
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Fish fish) == false)
                return;

            if(_fisherman.Hook.HookedEntity is Fish)
                MeetCondition();
        }
        private void Awake() => _fisherman = GetComponent<Fisherman>();
    }
}
