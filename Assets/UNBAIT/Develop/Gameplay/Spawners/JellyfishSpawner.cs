using Assets.UNBAIT.Develop.Gameplay.Entities;
using Assets.UNBAIT.Develop.Gameplay.Spawners.Abstract;
using Assets.UNBAIT.Develop.Gameplay.UI;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.Spawners
{
    public class JellyfishSpawner : Spawner<JellyFish>
    {
        [SerializeField] private JellyFish _prefab;

        [Space]

        [SerializeField] private float _threshold;

        [Range(0f, 1f)]
        [SerializeField] private float _spawnChance;
        [SerializeField] private float _delayBetweenSpawnAttempts;

        [Space]

        [SerializeField] private LevelTimer _levelTimer;

        private IEnumerator WaitThenSpawn()
        {

            while (true)
            {
                yield return new WaitUntil(() => _levelTimer.SliderValue <= _threshold);
                
                if (TrySpawn())
                    break;

                yield return new WaitForSeconds(_delayBetweenSpawnAttempts);
            }
        }

        private bool TrySpawn()
        {
            if (LevelTimer.IsPaused)
                return false;

            if (UnityEngine.Random.Range(0f, 1f) <= _spawnChance)
            {
                Spawn(_prefab);
                return true;
            }

            return false;
        }

        private void Start() => StartCoroutine(WaitThenSpawn());
    }
}
