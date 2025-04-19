using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.Entities
{
    public sealed class Fish : Entity, IHookable
    {
        public bool IsHooked { get; set; } = false;
    }
}