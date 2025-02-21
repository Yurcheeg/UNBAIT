using Assets.Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System;
using System.Collections.Generic;

namespace Assets.Assets.UNBAIT.Develop.Gameplay
{
    //TODO: Add fisherman
    public static class Target
    {
        private static Dictionary<EntityType, Type> _targetsToType = new Dictionary<EntityType, Type>()
        {
            {EntityType.None, null },
            {EntityType.Cursor, typeof(Cursor) },
            {EntityType.Hook, typeof(Hook) },
        };

        public static Type GetType(EntityType target) => _targetsToType[target];
    }

    [Flags]
    public enum EntityType
    {
        None = 0b_0000,

        Hook = 0b_0001,
        Cursor = 0b_0010,
    }
}
