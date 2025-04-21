using System;
using System.Collections;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public class HitFlash : MonoBehaviour
    {
        [SerializeField] private float _flashDelaySeconds = 0.5f;
        [SerializeField] private float _flashDurationSeconds;

        [SerializeField] private SpriteRenderer _renderer;

        [SerializeField] Material _material;

        private void OnHit()
        {
            StartCoroutine(Recolor());
        }

        private IEnumerator Recolor()
        {
            Material originalMaterial = _renderer.material;

            yield return new WaitForSeconds(_flashDelaySeconds);

            _renderer.material = _material;

            yield return new WaitForSeconds(_flashDurationSeconds);

            _renderer.material = originalMaterial;
        }
        private void Awake() => FlipOnClick.Hit += OnHit;
        private void OnDestroy() => FlipOnClick.Hit -= OnHit;
    }
}
