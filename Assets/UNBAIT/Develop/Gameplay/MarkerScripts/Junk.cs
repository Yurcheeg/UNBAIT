using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.MarkerScripts
{
    [RequireComponent(typeof(Collider2D))]
    public sealed class Junk : Entity, IHookable
    {
        public bool IsHooked { get; set; }
        public bool HasReachedGround { get; private set; }

        private Collider2D _collider;
        public void Ground()
        {
            HasReachedGround = true;
            _collider.isTrigger = false;
        }

        private void Awake()
        {
            if (HasReachedGround == false)
            {
                _collider = GetComponent<Collider2D>();
                _collider.isTrigger = true;
            }
        }
    }
}