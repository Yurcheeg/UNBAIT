using Assets.Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System;
using System.Collections.Generic;

namespace Assets.Assets.UNBAIT.Develop.Gameplay
{
    public static class Target
    {
        private static Dictionary<Targets, Type> _targetsToType = new Dictionary<Targets, Type>()
        {
            {Targets.None, null },
            {Targets.Cursor, typeof(Player) },
            {Targets.Hook, typeof(Hook) },
        };

        public static Type GetType(Targets target) => _targetsToType[target];
    }
}
