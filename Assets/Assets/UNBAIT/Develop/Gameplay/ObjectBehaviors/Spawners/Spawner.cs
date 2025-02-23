using Assets.Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints = new();
    [SerializeField] private Entity _entityToSpawn;

    [Min(0), SerializeField] private float _spawnDelay;

    public bool IsDisabled => false;

    public void Spawn<T>(T entity) where T : Entity
    {
        foreach (Transform spawnpoint in _spawnPoints)
            Instantiate(entity, spawnpoint.position, Quaternion.identity);
    }

    //TODO: Replace test implementation of spawning logic
    private IEnumerator Start()
    {
        if (IsDisabled)
            yield break;

        yield return new WaitForSecondsRealtime(3f);
        Spawn(_entityToSpawn);


        yield return Start();
    }

    private void Awake()
    {
        if(_spawnPoints.Count == 0)
            _spawnPoints.Add(transform);
    }
}
