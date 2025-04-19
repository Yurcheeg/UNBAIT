using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using System;

namespace Assets.UNBAIT.Develop.Gameplay.Entities
{
    public sealed class JellyFish : Entity, IHookable
    {
        public bool IsHooked { get; set; }
    }
}