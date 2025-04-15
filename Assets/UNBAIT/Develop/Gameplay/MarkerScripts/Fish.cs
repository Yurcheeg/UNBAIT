using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.MarkerScripts
{
    public sealed class Fish : Entity, IHookable
    {
        public bool IsHooked { get; set; } = false;
    }
}