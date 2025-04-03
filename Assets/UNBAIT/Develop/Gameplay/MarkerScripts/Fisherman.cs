using Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Abstract;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.MarkerScripts
{
    public sealed class Fisherman : Entity
    {
        [field: SerializeField] public bool IsTired { get; set; }
        [field: SerializeField] public bool IsStunned { get; set; }
        public bool IsReeling => Hook.InUse;

        [field: SerializeField] public float StunDuration { get; private set; }
        public Hook Hook { get; set; }
    }
}
