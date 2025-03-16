using System;
using UnityEngine;
using Cursor = Assets.UNBAIT.Develop.Gameplay.MarkerScripts.Cursor;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    [RequireComponent(typeof(Flip))]
    public sealed class FlipOnClick : MonoBehaviour
    {
        public static event Action Hit;

        [SerializeField, Min(1)] private int _hitCountToFlip;
        [SerializeField] private int _currentHitCount;

        private Flip _flip;
        private bool _canFlip;

        private void Flip() => _flip.FlipObject();

        private bool IsConditionMet() => _currentHitCount > _hitCountToFlip || _canFlip;

        private void Update()
        {
            if (Cursor.IsMouseOverTarget(gameObject) == false)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _currentHitCount++;
                Hit?.Invoke();
            }

            if (IsConditionMet() == false)
                return;
            
            _canFlip = true;

            if (Input.GetMouseButtonDown(1))
                Flip();
        }

        private void Awake() => _flip = GetComponent<Flip>();
    }
}
