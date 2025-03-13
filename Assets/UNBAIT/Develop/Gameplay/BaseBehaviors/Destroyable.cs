using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public class Destroyable : MonoBehaviour
    {
        public Action Destroyed;

        private void OnDestroy() => Destroyed?.Invoke();
    }
}