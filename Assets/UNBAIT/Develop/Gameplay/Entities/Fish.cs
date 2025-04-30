using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Movement;
using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.Entities
{
    public sealed class Fish : Entity, IHookable
    {
        public bool IsHooked { get; set; } = false;

        public Movable Movable { get; private set; }

        private void Awake() => Movable = GetComponent<Movable>();
    }
}