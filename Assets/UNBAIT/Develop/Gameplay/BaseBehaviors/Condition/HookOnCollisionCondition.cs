using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition.Abstract;
using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using Assets.UNBAIT.Develop.Gameplay.Entities;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition
{
    public class HookOnCollisionCondition : ReturnCondition
    {
        private Hook _hook;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Entity>(out Entity entity) == false)
                return;

            if(_hook.TryHookEntity(entity))
                MeetCondition();
        }

        private void Awake() => _hook = GetComponent<Hook>();
    }
}
