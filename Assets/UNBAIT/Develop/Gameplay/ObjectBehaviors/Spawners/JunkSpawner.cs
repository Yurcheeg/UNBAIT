using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.ObjectBehaviors.Spawners
{
    public class JunkSpawner : Spawner<Junk>
    {
        [SerializeField] private List<Junk> _junk;

        public override Junk Spawn(Junk prefab)
        {
            Junk junk = base.Spawn(prefab);
            return junk;
        }
        public Junk GetRandom() => _junk[Random.Range(0, _junk.Count)];

        [ContextMenu("Spawn Junk")]
        public Junk ThrowRandom() => Spawn(GetRandom());
    }
}
