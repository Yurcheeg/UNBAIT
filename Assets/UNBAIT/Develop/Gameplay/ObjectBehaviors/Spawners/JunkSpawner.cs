using Assets.UNBAIT.Develop.Gameplay.Entities;
using Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.EntityScripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.Spawners
{
    public class JunkSpawner : Spawner<Junk>
    {
        [SerializeField] private List<Junk> _junk;
        [Space]

        [Min(0)]
        [SerializeField] private float _spawnThreshold;

        [SerializeField] private TiredMeter _tiredMeter;

        private MovingEntity _entity;

        private bool _spawned;

        public Junk GetRandom() => _junk[Random.Range(0, _junk.Count)];

        [ContextMenu("Spawn Junk")]
        public Junk ThrowRandom() => Spawn(GetRandom());

        private void Update()
        {
            if (_spawned)
                return;

            if (_entity.IsMoving)
                return;

            if (_tiredMeter.SliderValue > _spawnThreshold)
                return;

            ThrowRandom();
            _spawned = true;
        }

        private void Awake() => _entity = GetComponent<MovingEntity>();
    }
}
