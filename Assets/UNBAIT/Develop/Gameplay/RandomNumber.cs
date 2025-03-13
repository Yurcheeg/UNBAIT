#define UNITY_EDITOR

using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay
{
    public static class RandomNumber
    {
        public static float GetInRange(float startNumber, float endNumber)
        {
            float min = Mathf.Min(startNumber, endNumber);
            float max = Mathf.Max(startNumber, endNumber);

            float randomNumber = Random.Range(min, max);

            randomNumber = Mathf.Clamp(randomNumber, min, max);

#if UNITY_EDITOR
            Debug.Log($"Random number is: {randomNumber} ");
#endif
            return min == max ? max : randomNumber;
        }
    }
}
