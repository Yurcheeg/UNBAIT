using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using Assets.UNBAIT.Develop.Gameplay.Spawners.Abstract;
using Assets.UNBAIT.Develop.Gameplay.UI;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.Spawners
{
    public class EntitySpawner : Spawner<Entity>
    {
        [SerializeField] private Entity _entityToSpawn;

        [Min(0), SerializeField] private float _spawnDelay;

        [Min(0), SerializeField] private int _spawnLimit;
        private int _count;

        public bool SpawnLimitReached => _count >= _spawnLimit;

        [ContextMenu("Spawn")]
        public void Spawn() => Spawn(_entityToSpawn);

        public override Entity Spawn(Entity prefab)
        {
            Entity entity = base.Spawn(prefab);

            if (entity != null)
            {
                _count++;
                StartCoroutine(WaitUntilDestroyableInitialized(entity));
            }

            return entity;
        }

        private IEnumerator WaitUntilDestroyableInitialized(Entity entity)
        {
            //skip a frame to let entity's awake assign destroyable
            yield return null;

            if (entity == null)
                yield break;

            if (entity.Destroyable == null)
                yield break;

            SubscribeToDestroyable(entity);
        }

        private void OnWaveCleared(int count)
        {
            if(count > 2)
                _spawnLimit++;
        }

        private void SubscribeToDestroyable(Entity entity)
        {
            Destroyable destroyable = entity.Destroyable;

            void OnDestroyed()
            {
                destroyable.Destroyed -= OnDestroyed;
                _count--;
            }

            destroyable.Destroyed += OnDestroyed;
        }

        private IEnumerator Start()
        {
            while (true)
            {
                if (LevelTimer.IsPaused)
                    yield return null;
                else if (SpawnLimitReached)
                    yield return null;
                else
                {
                    yield return new WaitForSeconds(_spawnDelay);

                    Spawn();
                }
            }
        }
        private void OnEnable() => LevelTimer.WaveCleared += OnWaveCleared;

        private void OnDisable() => LevelTimer.WaveCleared -= OnWaveCleared;

    }
}
