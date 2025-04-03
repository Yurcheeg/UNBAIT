using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.Spawners
{
    public class HookSpawner : Spawner<Hook>
    {
        [SerializeField] private Hook _hookPrefab;

        [SerializeField] private float _spawnDelay;

        public override Hook Spawn(Hook prefab)
        {
            if (prefab == null)
                throw new NullReferenceException("Prefab is not assigned");

            Hook hook = base.Spawn(prefab);
            return hook;
        }

        public void Spawn(float delay, Action spawned)
        {
            CustomCoroutine.Instance.WaitThenExecute(delay, () =>
            {
                Hook hook = Spawn(_hookPrefab);
                spawned?.Invoke();
            });
        }
        public Hook ThrowHook() => Spawn(_hookPrefab);

        public void ThrowHook(float delay, Action<Hook> hookReady)
        {
            Spawn(delay, () =>
            {
                Hook hook = Spawn(_hookPrefab);
                hookReady.Invoke(hook);
            });
        }


    }
}
