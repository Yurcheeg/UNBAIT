using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition.Abstract;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition
{
    public class HookOnCollisionCondition : ReturnCondition
    {
        private Hook _hook;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_hook == null)
                return;

            if (collision.gameObject.TryGetComponent(out Entity entity) == false)
                return;

            if (entity is Junk junk && (junk.HasReachedGround == false))
                return;

            if (entity is not IHookable hookable)
                return;

            if (hookable.IsHooked)
                return;

            MeetCondition();
            hookable.IsHooked = true;
        }

        private void Awake() => _hook = GetComponent<Hook>();
    }
}
