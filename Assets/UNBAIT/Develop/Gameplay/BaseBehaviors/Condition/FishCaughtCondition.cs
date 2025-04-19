using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition.Abstract;
using Assets.UNBAIT.Develop.Gameplay.Entities;
using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition
{
    public class FishCaughtCondition : ReturnCondition    
    {
        public void OnCaught(Entity entity)
        {
            if (entity is Fish)
                MeetCondition();
        }
    }
}
