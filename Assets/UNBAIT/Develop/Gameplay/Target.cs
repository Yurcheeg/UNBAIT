using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System;
using System.Collections.Generic;

namespace Assets.UNBAIT.Develop.Gameplay
{
    public static class Target
    {
        private static readonly Dictionary<EntityType, Type> _targetsToType = new Dictionary<EntityType, Type>()
        {
            {EntityType.None, null },
            {EntityType.Cursor, typeof(Cursor) },
            {EntityType.Hook, typeof(Hook) },
            {EntityType.Fisherman, typeof(Fisherman) },
            {EntityType.Fish, typeof(Fish) }
        };

        public static Type GetType(EntityType target)
        {
            if (_targetsToType[target] is null) //Special treatment because type cannot be null
                throw new ArgumentException($"{nameof(target)} cannot be set to {nameof(EntityType.None)}");

            return _targetsToType[target];
        }
    }

    [Flags]
    public enum EntityType
    {
        None = 0b_0000,

        Hook = 0b_0001,
        Cursor = 0b_0010,
        Fisherman = 0b_0100,
        Fish = 0b_1000
    }
}
