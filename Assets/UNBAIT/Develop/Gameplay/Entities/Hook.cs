using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Collision;
using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using Assets.UNBAIT.Develop.Gameplay.Inventory;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.Entities
{
    public sealed class Hook : Entity
    {
        public event System.Action<Entity> Caught;

        private Vector3 _startPosition;

        private MovementController _movementController;

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
            hookable.IsHooked = true;
            return true;
        }

        private void Update()
        {
            if (HookedEntity != null)
                HookedEntity.transform.position = transform.position;
        }


        private void OnPositionReached()
        {
            if(InUse | HookedEntity != null)
            {
                HasReturned = true;
                Caught?.Invoke(HookedEntity);
            }

            StartCoroutine(DestroyWhenAboveStartPosition());
        }

        private IEnumerator DestroyWhenAboveStartPosition()
        {
            while (transform.position.y < _startPosition.y)
                yield return null;

            Destroy(gameObject);
        }

        private void OnDestroy()
        {
            if (HookedEntity != null && (HasReturned || HookedEntity.TryGetComponent<Item>(out _)))
                Destroy(HookedEntity.gameObject);

            _movementController.PositionReached -= OnPositionReached;
        }

        private void Start() => _startPosition = transform.position;

        private void Awake()
        {
            _movementController = GetComponent<MovementController>();

            _movementController.PositionReached += OnPositionReached;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var isTutorial = FindAnyObjectByType(typeof(TutorialManager)) != null;
            if(isTutorial)
                InUse = true;
        }
    }
}