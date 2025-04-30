using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Movement;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.FishFlip
{
    public sealed class Flip : MonoBehaviour
    {
        public event Action Flipped;
        public Movable Movable { get; private set; }

        public void FlipObject()
        {
            Vector2 direction = Movable.Direction;
            direction = new Vector2(direction.x *= -1, direction.y);

            Movable.SetDirection(direction);

            Flipped?.Invoke();
        }

        private void Awake() => Movable = GetComponent<Movable>();
    }
}
