using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition.Abstract;
using Assets.UNBAIT.Develop.Gameplay.Entities;
using Assets.UNBAIT.Develop.Gameplay.Entities.Abstract;
using System;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition
{
    public class FishCaughtCondition : ReturnCondition    
    {
        public static event Action FishCaught;

        public void OnCaught(Entity entity)
        {
            if (entity is Fish)
            {
                MeetCondition();
                FishCaught?.Invoke();
            }
        }
    }
}
