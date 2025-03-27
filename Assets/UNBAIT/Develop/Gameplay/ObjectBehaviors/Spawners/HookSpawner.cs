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
            Hook hook = base.Spawn(prefab);
            hook.gameObject.SetActive(false);
            StartCoroutine(ActivateAfterDelay(hook));
            return hook;
        }

        public Hook ThrowHook() => Spawn(_hookPrefab);

        private IEnumerator ActivateAfterDelay(Hook hook)
        {
            yield return new WaitForSecondsRealtime(_spawnDelay);

            if (hook == null)
                yield break;
            
            hook.gameObject.SetActive(true);
        }
    }
}
