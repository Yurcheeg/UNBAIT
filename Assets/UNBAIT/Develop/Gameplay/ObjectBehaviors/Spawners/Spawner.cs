using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : Entity
    {
        [SerializeField] private Transform _spawnPoint;

        protected Transform SpawnPoint
        {
            get => _spawnPoint != null ? _spawnPoint : transform;

            set => _spawnPoint = value;
        }

        //null check to negate the MissingReferenceError from CustomCoroutine in TryThrowHook
        public virtual T Spawn(T prefab) => this == null ? null : Instantiate(prefab, SpawnPoint.position, Quaternion.identity);
    }
}
