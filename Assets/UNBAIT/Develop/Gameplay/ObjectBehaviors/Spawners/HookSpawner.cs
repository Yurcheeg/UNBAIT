using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.Spawners
{
    public class HookSpawner : Spawner<Hook>
    {
        public event Action<Hook> HookSpawned;
        public event Action HookDestroyed;
        [SerializeField] private Hook _hookPrefab;

        [SerializeField] private float _spawnDelay = 5f;

        public override Hook Spawn(Hook prefab)
        {
            StartCoroutine(SpawnDelay());
            Hook hook = base.Spawn(prefab);
            HookSpawned?.Invoke(hook);
            return hook;
        }
        public Hook ThrowHook() => Spawn(_hookPrefab);

        private IEnumerator SpawnDelay()
        {
            yield return new WaitForSecondsRealtime(_spawnDelay);
        }
    }
}
