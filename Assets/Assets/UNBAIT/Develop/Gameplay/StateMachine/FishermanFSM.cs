using Assets.Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using System;
using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay.StateMachine
{
    public class FishermanFSM : MonoBehaviour
    {
        private BaseState<FishermanFSM> _currentState;

        private BaseEntity _entity;

        private FishermanMovement _fishermanMovement;

        private void ChangeState(BaseState<FishermanFSM> newState)
        {
            _currentState?.Exit();

            _currentState = newState;
            _currentState.Enter();
        }

        public void StartMovement() => _entity.IsMoving = true;

        public void StopMovement() => _entity.IsMoving = false;

        private void OnPositionSet() => ChangeState(new MovingState(this));

        public void ThrowHook()
        {
            throw new NotImplementedException();
        }

        private void Update() => _currentState?.Update();

        private void Start()
        {
            if(_currentState == null)
                ChangeState(new IdleState(this));
        }

        private void Awake()
        {
            _entity = GetComponent<BaseEntity>();
            _fishermanMovement = GetComponent<FishermanMovement>();

            _fishermanMovement.PositionSet += OnPositionSet;
            _fishermanMovement.PositionReached += OnPositionReached;
            //_entity.MovementStarted += OnMovementChange;
        }

        private void OnPositionReached() => ChangeState(new FishingState(this));


        //private void OnMovementChange(bool isMoving)
        //{
        //    //if(isMoving)
        //    //{
        //    //    ChangeState(new MovingState(this)); // movement change is basically called by MovingState
        //    //}
        //}
    }
}