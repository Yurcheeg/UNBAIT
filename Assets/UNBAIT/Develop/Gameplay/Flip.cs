using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay
{
    public sealed class Flip : MonoBehaviour
    {
        public static event Action Flipped;
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
