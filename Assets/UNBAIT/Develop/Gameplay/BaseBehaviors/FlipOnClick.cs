using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    [RequireComponent(typeof(Flip))]
    [RequireComponent(typeof(HitFlash))]
    public sealed class FlipOnClick : MonoBehaviour, IPointerClickHandler
    {
        //events for SFX
        public static event Action Slapped;
        public static event Action Flipped;
        //events for VFX
        public event Action Hit;
        public event Action CanFlip;

        [SerializeField, Min(1)] private int _hitCountToFlip;
        [SerializeField] private int _currentHitCount;

        private Flip _flip;

        private void Flip()
        {
            _flip.FlipObject();
            Flipped?.Invoke();
        }

        private bool IsFlipConditionMet() => _currentHitCount > _hitCountToFlip;

        //Method for ui(tutorial). 
        //TODO replace
        public void OnPointerClick(PointerEventData eventData)
        {
            if (Cursor.IsMouseOverUI(gameObject) == false)
                return;

            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    _currentHitCount++;
                    Hit?.Invoke();

                    if (IsFlipConditionMet() == false)
                        return;
                    //TODO: Add exclamation mark and remove it in 0.3 sec

                    break;

                case PointerEventData.InputButton.Right:
                    if (IsFlipConditionMet())
                        Flip();
                    break;

                default:
                    break;
            }
        }

        private void Update()
        {
            if (Cursor.IsMouseOverTarget(gameObject) == false)
                return;

            if (Input.GetMouseButtonDown(0))
            {
                _currentHitCount++;
                Hit?.Invoke();
                Slapped?.Invoke();

                if (IsFlipConditionMet() == false)
                    return;

                CanFlip?.Invoke();
            }

            if (IsFlipConditionMet() == false)
                return;

            if (Input.GetMouseButtonDown(1))
                Flip();
        }

        private void Awake() => _flip = GetComponent<Flip>();
    }
}
