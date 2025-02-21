﻿using UnityEngine;

namespace Assets.Assets.UNBAIT.Develop.Gameplay
{
    public static class RandomNumber
    {
        public static float GetInRange(float startNumber, float endNumber)
        {
            float min = Mathf.Min(startNumber, endNumber);
            float max = Mathf.Max(startNumber, endNumber);

            float randomNumber = Random.Range(min, max);

            Debug.Log($"Random number is: {randomNumber} ");
            return Random.Range(min, max);
        }
    }
}
