using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition
{
    public class TriggerCondition : ReturnCondition
    {
        public void Trigger() => MeetCondition();
    }
}
