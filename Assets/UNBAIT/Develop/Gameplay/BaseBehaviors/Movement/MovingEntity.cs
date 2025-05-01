using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Movement
{
    [RequireComponent(typeof(Movable))]
    public class MovingEntity : MonoBehaviour
    {
        public bool IsMoving { get; set; }

        public Movable Movable { get; private set; }

        private void Update()
        {
            if (IsMoving)
                Movable.Move();
        }

        private void Awake() => Movable = GetComponent<Movable>();
    }
}
