using Assets.Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using NUnit.Framework;
using System;
using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts
{
    [RequireComponent(typeof(Movable))]
    [RequireComponent(typeof(Rotatable))]
    public class BaseEntity : MonoBehaviour
    {
        public Action MovementStarted;
        public Action MovementStopped;

        public Movable Movable { get; private set; }

        public Rotatable Rotatable { get; private set; }

        [field: SerializeField] public bool IsMoving { get; set; } = false;

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
