using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : Entity
    {
        [SerializeField] private Transform _spawnPoint;

        protected Transform SpawnPoint
        {
            get
            {
                return _spawnPoint != null ? _spawnPoint : transform;
            }

            set => _spawnPoint = value;
        }

        public virtual T Spawn(T prefab) => Instantiate(prefab, SpawnPoint.position, Quaternion.identity);
    }
}
