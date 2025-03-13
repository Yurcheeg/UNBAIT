using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.StateMachine.Abstract
{
    public abstract class BaseState<TFSM> where TFSM : MonoBehaviour
    {
        protected TFSM FSM;

        protected BaseState(TFSM fsm)
        {
            FSM = fsm;
        }

        public virtual void Enter()
        {
#if UNITY_EDITOR
            Debug.Log($"Entering {GetType().Name}");
#endif
        }
        public virtual void Exit()
        {
#if UNITY_EDITOR
            Debug.Log($"Exiting {GetType().Name}");
#endif
        }
        public virtual void Update() { }

        #region Equals Override
        public override bool Equals(object obj)
        {
            return obj.GetType() == GetType();
        }

        public override int GetHashCode()
        {
            return GetType().GetHashCode();
        }
        #endregion
    }
}
