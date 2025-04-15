using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.MarkerScripts
{
    public sealed class Fisherman : Entity
    {
        public event Action Stunned;
        public event Action Unstunned;

        [field: SerializeField] public bool IsTired { get; set; }
        public bool IsReeling => Hook != null && Hook.InUse;

        [field: Space]

        [field: SerializeField] public bool IsStunned { get; private set; }
        [field: SerializeField] public float StunDuration { get; private set; }

        [field: Space]

        [field: SerializeField] public float ThrowDelay { get; private set; }
        public Hook Hook { get; set; }

        public Animator Animator { get; private set; }

        public void Stun()
        {
            if (IsStunned)
                return;

            IsStunned = true;
            Stunned?.Invoke();

            CustomCoroutine.Instance.WaitThenExecute(StunDuration, () =>
            {
                IsStunned = false;
                Unstunned?.Invoke();
            });

        }

        private void Awake() => Animator = GetComponent<Animator>();
    }
}
