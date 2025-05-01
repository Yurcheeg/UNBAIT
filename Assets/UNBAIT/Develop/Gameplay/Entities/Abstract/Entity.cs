using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.Entities.Abstract
{
    [RequireComponent(typeof(Destroyable))]
    public abstract class Entity : MonoBehaviour
    {
        public Destroyable Destroyable { get; private set; }

        //allows to call base awake in child classes
        protected virtual void Awake() => Destroyable = GetComponent<Destroyable>();
    }
}