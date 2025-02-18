using System;

namespace Assets.Assets.UNBAIT.Develop.Gameplay
{
    [Flags]
    public enum Targets
    {
        None = 0b_0000,

        Hook = 0b_0001,
        Cursor = 0b_0010,
    }
}
