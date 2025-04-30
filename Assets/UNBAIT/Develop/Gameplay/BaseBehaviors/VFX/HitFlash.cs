using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.FishFlip;
using Assets.UNBAIT.Develop.Gameplay.Entities;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.VFX
{
    [RequireComponent(typeof(FlipOnClick))]
    public class HitFlash : MonoBehaviour
    {
        [SerializeField] private float _flashDelaySeconds = 0.5f;
        [SerializeField] private float _flashDurationSeconds;
        [Space]
        [Header("Choose one!")]
        [SerializeField] private SpriteRenderer _renderer;
        [SerializeField] private Image _image;
        [Space]
        [SerializeField] private Material _material;

        private Material _originalMaterial;
        private Coroutine _flashCoroutine;
        private FlipOnClick _flipOnClick;

        private Fish _fish;

        private void OnHit()
        {
            if (_fish.IsHooked)
                return;

            if (_flashCoroutine != null)
                StopCoroutine(_flashCoroutine);

            _flashCoroutine = StartCoroutine(Recolor());
        }

        private IEnumerator Recolor()
        {
            yield return new WaitForSeconds(_flashDelaySeconds);

            if (_image != null)
                _image.material = _material;
            else
                _renderer.material = _material;

            yield return new WaitForSeconds(_flashDurationSeconds);

            if (_image != null)
                _image.material = _originalMaterial;
            else
                _renderer.material = _originalMaterial;

            _flashCoroutine = null;
        }
        private void Awake()
        {
            _flipOnClick = GetComponent<FlipOnClick>();
            _fish = GetComponent<Fish>();
            _flipOnClick.Hit += OnHit;

            _originalMaterial = _image != null ? _image.material : _renderer.material;
        }

        private void OnDestroy() => _flipOnClick.Hit -= OnHit;
    }
}
