using Assets.Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using System;
using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay.StateMachine
{
    public class FishermanFSM : MonoBehaviour
    {
        private State _currentState;

        private BaseEntity _entity;

        private void ChangeState(State newState)
        {
            if (_currentState != null)
                _currentState.Exit();

            _currentState = newState;
            _currentState.Enter();
        }

        public void StartMovement() => _entity.IsMoving = true;

        public void StopMovement() => _entity.IsMoving = false;

        public void ThrowHook()
        {
            throw new NotImplementedException();
        }

        private void Update()
        {
            if (_currentState != null)
                _currentState.Update();
        }

        private void Start() => ChangeState(new IdleState(this));

        private void Awake() => _entity = GetComponent<BaseEntity>();
    }
}