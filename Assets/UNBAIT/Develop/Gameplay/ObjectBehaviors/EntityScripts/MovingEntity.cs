using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts
{
    [RequireComponent(typeof(Movable))]
    public class MovingEntity : MonoBehaviour
    {
        public bool IsMoving { get; set; }

        public Movable Movable { get; private set; }

        public bool IsNotLookingAt(GameObject entity)
        {
            Vector2 directionToEntity = (entity.transform.position - transform.position).normalized;
            float dot = Vector2.Dot(directionToEntity, Movable.Direction.normalized);
            return dot <= 0;
        }
        //HACK: just testing, pls replace ASAP
        //summary: if direction is the same as the remainder of this pos and hook pos we skip it
        //if its the same that means that this object is already past this hook

        private void Update()
        {
            if (IsMoving)
                Movable.Move();
        }

        private void Awake() => Movable = GetComponent<Movable>();
    }
}
