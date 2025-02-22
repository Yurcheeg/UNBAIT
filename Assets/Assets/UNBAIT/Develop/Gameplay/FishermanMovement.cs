using Assets.Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using System;
using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts
{
    [RequireComponent(typeof(BaseEntity))]
    public class FishermanMovement : MonoBehaviour
    {
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;

        private float _positionX;

        private BaseEntity _entity;
        private Movable Movable => _entity.Movable;

        private void Update()
        {
            if(Mathf.Abs(transform.position.x - _positionX) <= 0.05f)
            {
                //TODO: Fix
                _entity.IsMoving = false;
            }
        }

        private void Start()
        {
            Movable.SetDirection(new Vector2(Vector2.zero.x - transform.position.x,0));

            _positionX = RandomNumber.GetInRange(_startPoint.position.x, _endPoint.position.x);
        }

        //TODO: add new class for hook bhvr
        //private void ThrowHook() { }

        private void Awake() => _entity = GetComponent<BaseEntity>();
    }
}
