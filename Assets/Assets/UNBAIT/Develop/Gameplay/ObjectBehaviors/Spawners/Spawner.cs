using Assets.Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private List<Transform> _spawnPoints = new();
    [SerializeField] private Entity _entityToSpawn;
    public void Spawn<T>(T entity) where T : Entity
    {
        foreach (Transform spawnpoint in _spawnPoints)
        {
            Vector2 direction = (Vector2.zero - (Vector2)spawnpoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            //TODO: Rotate on Y axis so the sprites rotate properly (probably clamp to 180)
            angle = Mathf.Clamp(angle, 0f, 180f);
            //TODO: Hardcode. Replace
            Instantiate(entity, spawnpoint.position, Quaternion.Euler(0,0,angle));
        }
    }

    private IEnumerator Start()
    {
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
