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

            if (entity != null)
            {
                //subscribes to local OnEntityDestroyed on next Update,
                //so that Awake in entity that assigns the Destroyable finishes.
                CustomCoroutine.Instance.WaitThenExecute(-1,
                    () =>
                    {
                        if (entity == null)
                            return;
                        if (entity.Destroyable == null)
                            return;

                        //The local method safely unsubscribes to avoid leaks.
                        void OnEntityDestroyed()
                        {
                            entity.Destroyable.Destroyed -= OnEntityDestroyed;
                            _count--;
                        }

                        entity.Destroyable.Destroyed += OnEntityDestroyed;
                    });
            }

            return entity;
        }

        private IEnumerator Start()
        {
            while (true)
            {
                if (LevelTimer.IsPaused)
                    yield return null;
                else if (IsDisabled)
                    yield return null;
                else if (SpawnLimitReached)
                    yield return null;
                else
                {
                    yield return new WaitForSeconds(_spawnDelay);

                    Spawn();
                    _count++;
                }
            }
        }
    }
}
