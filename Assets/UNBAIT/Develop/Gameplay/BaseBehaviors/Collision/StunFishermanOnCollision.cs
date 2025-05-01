using Assets.UNBAIT.Develop.Gameplay.Entities;
using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Collision
{
    public class StunFishermanOnCollision : MonoBehaviour
    {
        private Entity _entity;

        private void Stun(Fisherman fisherman) => fisherman.Stun();

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent(out Fisherman fisherman) == false)
                return;

            if (_entity is not IHookable hookable)
                return;

            if (hookable.IsHooked == false)
                return;

            if (fisherman.Hook.HookedEntity == _entity)
                Stun(fisherman);
        }

        private void Awake() => _entity = GetComponent<Entity>();
    }
}
