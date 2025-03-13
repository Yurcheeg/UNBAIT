using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.MarkerScripts
{
    public sealed class Hook : Entity//TODO: put all the logic into desegnated class
    {
        private Entity _currentEntity = null;

        [field: SerializeField] public bool InUse { get; private set; } = false;

        public bool TryHookEntity(Entity entity)
        {
            if (InUse)
                return false;

            if (entity is not IHookable hookable)
                return false;

            if (hookable.IsHooked)
                return false;

            _currentEntity = entity;
            InUse = true;
            hookable.IsHooked = true;

            return true;
        }
        public bool TryUnhookEntity(Entity entity)
        {
            if (entity is not IHookable hookable)
                return false;

            if (entity == _currentEntity)
            {
                _currentEntity = null;
                InUse = false;
                hookable.IsHooked = false;
                return true;
            }

            return false;
        }
        private void Update()
        {
            if (InUse)
                GoUp();

            if (_currentEntity != null)
                _currentEntity.transform.position = transform.position;
        }

        private void GoUp()
        {//TODO fix
            //fix what?
        }
    }
}