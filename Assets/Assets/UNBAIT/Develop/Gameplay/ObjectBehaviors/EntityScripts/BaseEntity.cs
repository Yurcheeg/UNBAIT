using Assets.Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using System;
using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts
{
    [RequireComponent(typeof(Movable))]
    [RequireComponent(typeof(Rotatable))]
    public class BaseEntity : MonoBehaviour
    {
        public Action<bool> MovementStarted;

        private bool _isMoving = false;
        public bool IsMoving
        {
            get => _isMoving;
            set
            {
                if (value == _isMoving)
                    return;

                MovementStarted?.Invoke(value);
                _isMoving = value;
            }
        }

        public Movable Movable { get; private set; }

        public Rotatable Rotatable { get; private set; }


        private void Update()
        {
            if (IsMoving)
                Movable.Move();
        }

        private void Awake()
        {
            Movable = GetComponent<Movable>();
            Rotatable = GetComponent<Rotatable>();
        }
    }
}
