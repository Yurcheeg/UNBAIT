using Assets.UNBAIT.Develop.Gameplay.Inventory;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.MarkerScripts
{
    public sealed class Hook : Entity//TODO: put all the logic into desegnated class
    {
        private Vector3 startPosition;

        public Entity HookedEntity { get; private set; }

        [field: SerializeField] public bool InUse { get; private set; } = false;
        public bool HasReturned { get; private set; }

        public bool TryHookEntity(Entity entity)
        {
            if (InUse)
                return false;

            if (entity is Junk junk && (junk.HasReachedGround == false))//HACK wow another junk ground check very cool pls fix
                return false;

            if (entity is not IHookable hookable)
                return false;

            if (hookable.IsHooked)
                return false;

            HookedEntity = entity;
            InUse = true;
            //hookable.IsHooked = true; //race condition w/ HookOnCollision

            return true;
        }

        private void Update()
        {
            if (InUse)
                DestroyWhenReturned();

            if (HookedEntity != null)
                HookedEntity.transform.position = transform.position;
        }

        private void OnDestroy()
        {
            if (HookedEntity != null && (HasReturned || HookedEntity.TryGetComponent<Item>(out _)))
                Destroy(HookedEntity.gameObject);
            
        }

        private void Start() => startPosition = transform.position;

        private void DestroyWhenReturned()
        {
            if (transform.position.y > startPosition.y)
            {
                HasReturned = true;
                OnDestroy();//TODO: feels illegal. also doesn't work with hooks being close
            }
        }
    }
}