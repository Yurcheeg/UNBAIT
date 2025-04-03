using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public class StunOnTargetCollision : MonoBehaviour
    {
        [SerializeField] private EntityType _targetToFind;
        private Type _targetType;

        private void Stun(Fisherman fisherman)
        {
            fisherman.IsStunned = true;

            CustomCoroutine.Instance.WaitThenExecute(fisherman.StunDuration,
                () => fisherman.IsStunned = false
                );
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Entity entity) == false)   
                return;
            if(entity.GetType() == _targetType)
                Stun(entity as Fisherman);//TODO: fix if bored
        }
        
        private void Awake() => _targetType = Target.GetType(_targetToFind);
    }
}
