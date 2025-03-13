using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using System.Collections;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.Spawners
{
    public class EntitySpawner : Spawner<Entity>
    {
        [SerializeField] private Entity _entityToSpawn;

        [Min(0), SerializeField] private float _spawnDelay;

        [Min(0), SerializeField] private int _spawnLimit;
        private int _count;

        public bool SpawnLimitReached => _count >= _spawnLimit;

        public bool IsDisabled => false;

        [ContextMenu("Spawn")]
        public override Entity Spawn(Entity prefab)
        {
            Entity entity = base.Spawn(prefab);
            entity.Destroyable.Destroyed += OnDestroyed;

            return entity;
        }

        private void OnDestroyed() => _count--;

        //TODO: Replace test implementation of spawning logic
        private IEnumerator Start()
        {
            if (IsDisabled)
                yield break;

            if (SpawnLimitReached)
                yield break;

            yield return new WaitForSecondsRealtime(_spawnDelay);
            Spawn(_entityToSpawn);

            _count++;

            yield return Start();
        }

    }
}
