using System;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public interface IConditionMeetable //TODO: bad name
    {
        public event Action ConditionMet;
    }
}
