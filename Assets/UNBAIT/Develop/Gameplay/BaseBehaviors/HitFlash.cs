using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
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
        [SerializeField] Material _material;

        private FlipOnClick _flipOnClick;

        private void OnHit() => StartCoroutine(Recolor());

        private IEnumerator Recolor()
        {
            Material originalMaterial = _image != null ? _image.material : _renderer.material;

            yield return new WaitForSeconds(_flashDelaySeconds);

            if (_image != null)
                _image.material = _material;
            else
                _renderer.material = _material;

            yield return new WaitForSeconds(_flashDurationSeconds);

            if (_image != null)
                _image.material = originalMaterial;
            else
                _renderer.material = originalMaterial;
        }
        private void Awake()
        {
            _flipOnClick = GetComponent<FlipOnClick>();
            _flipOnClick.Hit += OnHit;
            //FlipOnClick.Hit += OnHit;
        }

        private void OnDestroy() => _flipOnClick.Hit -= OnHit;
    }
}
