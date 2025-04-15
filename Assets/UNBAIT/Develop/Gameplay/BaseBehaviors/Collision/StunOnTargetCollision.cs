using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Collision
{
    public class StunOnTargetCollision : MonoBehaviour
    {
        [SerializeField] private EntityType _targetToFind;
        private Type _targetType;

        private void Stun(Fisherman fisherman)
        {
            fisherman.Stun();

            Animator animator = fisherman.Animator;
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            animator.SetTrigger("IsShocked");

            CustomCoroutine.Instance.WaitOnConditionThenExecute(
                () => stateInfo.normalizedTime >= 1f,
                () => animator.SetBool("IsStunned", true)
                );
            //TODO: indicate that it's stunned somehow

            CustomCoroutine.Instance.WaitOnConditionThenExecute(
                () => fisherman.IsStunned == false,
                () => animator.SetBool("IsStunned", false)
                    );
            //TODO: remove indicator;

        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Entity entity) == false)
                return;
            if (entity.GetType() == _targetType)
                Stun(entity as Fisherman);//HACK: fix if bored
        }

        private void Awake() => _targetType = Target.GetType(_targetToFind);
    }
}
