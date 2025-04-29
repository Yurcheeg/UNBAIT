using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using Assets.UNBAIT.Develop.Gameplay.UI;
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
        public void Spawn() => Spawn(_entityToSpawn);

        public override Entity Spawn(Entity prefab)
        {
            Entity entity = base.Spawn(prefab);
            entity.Destroyable.Destroyed += OnDestroyed;

            return entity;
        }

        private void OnDestroyed() => _count--;

        private IEnumerator Start()
        {
            while (true)
            {
                if (LevelTimer.IsPaused)
                    yield return null;

                if (IsDisabled)
                    yield return null;

                if (SpawnLimitReached)
                    yield return null;

                yield return new WaitForSeconds(_spawnDelay);

                Spawn();
                _count++;
            }
        }
    }
}
