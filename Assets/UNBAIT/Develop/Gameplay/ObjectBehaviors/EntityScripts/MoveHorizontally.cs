using Assets.UNBAIT.Develop.Gameplay;
using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using System;
using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts
{
    [RequireComponent(typeof(MovingEntity))]
    public class MoveHorizontally : MonoBehaviour//TODO: ADD COLLISION PREVENTION
    {
        private MovingEntity _entity;

        private Movable Movable => _entity.Movable;

        private void Start()
        {
            Vector2 direction = new Vector2(Vector2.zero.x - transform.position.x, 0).normalized;
            Movable.SetDirection(direction);
        }

        private void Awake() => _entity = GetComponent<MovingEntity>();
    }
}
