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
            #region Weird angle math which spawner shouldn't be responsible for
            Vector2 direction = (Vector2.zero - (Vector2)spawnpoint.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            //TODO: Move this logic somewhere else...
            float angleX = Mathf.Abs(angle) >= 90 ? 180f : 0f;

            float angleZ = Mathf.Abs(angleX%360f) == 180f ? (Mathf.Sign(direction.x) * angle) : angle;
            #endregion
            //TODO: Hardcode. Replace

            Instantiate(entity, spawnpoint.position, Quaternion.Euler(angleX, 0f, angleZ));
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
