using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract
{
    [RequireComponent(typeof(Destroyable))]
    public abstract class Entity : MonoBehaviour
    {
        public Destroyable Destroyable { get; private set; }

        private void Awake() => Destroyable = GetComponent<Destroyable>();
    }
}