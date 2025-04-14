using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.MarkerScripts
{
    public sealed class Hook : Entity//TODO: put all the logic into desegnated class
    {
        private Vector3 startPosition;

        private Entity _hookedEntity = null;

        [field: SerializeField] public bool InUse { get; private set; } = false;
        public bool HasReturned { get; private set; }

        public bool TryHookEntity(Entity entity)
        {
            if (InUse)
                return false;

            if (entity is not IHookable hookable)
                return false;

            if (hookable.IsHooked)
                return false;

            _hookedEntity = entity;
            InUse = true;
            hookable.IsHooked = true;

            return true;
        }

        private void Update()
        {
            if (InUse)
                DestroyWhenReturned();

            if (_hookedEntity != null)
                _hookedEntity.transform.position = transform.position;
        }

        private void OnDestroy()
        {
            if (_hookedEntity != null && (HasReturned || _hookedEntity.TryGetComponent<Item>(out _)))
                Destroy(_hookedEntity.gameObject);
            
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