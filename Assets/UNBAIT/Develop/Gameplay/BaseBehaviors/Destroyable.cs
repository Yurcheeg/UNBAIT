using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public class Destroyable : MonoBehaviour
    {
        public event Action Destroyed;

        private void OnDestroy() => Destroyed?.Invoke();
    }
}