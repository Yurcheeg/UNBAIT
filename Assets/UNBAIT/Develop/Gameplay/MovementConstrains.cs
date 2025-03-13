using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay
{
    [Serializable]
    public class MovementConstrains
    {
        [field: SerializeField] public bool MoveX { get; private set; }
        [field: SerializeField] public bool MoveY { get; private set; }

        public float GetLargestAxis(Vector2 position)
        {
            if (MoveX && MoveY)
                throw new ArgumentException("Cannot apply constraints to both axes");

            if ((MoveX || MoveY) == false)
                throw new ArgumentException("No axis chosen");

            float newX = MoveX ? position.x : 0;
            float newY = MoveY ? position.y : 0;
            
            float biggestValue = Mathf.Abs(newX) > Mathf.Abs(newY) ? newX : newY;

            return biggestValue;
        }
    }
}
