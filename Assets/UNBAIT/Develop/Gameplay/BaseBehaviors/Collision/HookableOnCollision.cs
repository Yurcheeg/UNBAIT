using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using System;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public class HookableOnCollision : MonoBehaviour
    {
        public event Action<Hook> Hooked;

        private Entity _entity;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.TryGetComponent<Hook>(out Hook hook) == false)
                return;

            if (hook.TryHookEntity(_entity))
                Hooked?.Invoke(hook);
        }

        private void Awake() => _entity = GetComponent<Entity>();
    }
}
