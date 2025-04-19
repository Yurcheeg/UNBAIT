using Assets.UNBAIT.Develop.Gameplay.Entities;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Collision
{
    public class StunFishermanOnCollision : MonoBehaviour
    {
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
            if (collision.gameObject.TryGetComponent(out Fisherman fisherman))
                Stun(fisherman);
        }
    }
}
