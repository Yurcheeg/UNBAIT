using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.Entities
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class Junk : Entity, IHookable
    {
        private Collider2D _collider;

        public bool IsHooked { get; set; }
        public bool HasReachedGround { get; private set; }

        public void Ground()
        {
            HasReachedGround = true;
            _collider.isTrigger = false;
        }

        protected override void Awake()
        {
            base.Awake();
            if (HasReachedGround == false)
            {
                _collider = GetComponent<Collider2D>();
                _collider.isTrigger = true;
            }
        }
    }
}