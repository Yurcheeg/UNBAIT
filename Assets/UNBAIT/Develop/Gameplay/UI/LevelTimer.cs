using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UNBAIT.Develop.Gameplay.UI
{
    [RequireComponent(typeof(Slider))]
    public class LevelTimer : MonoBehaviour
    {
        [SerializeField] private float _maxTimeSeconds;

        [Space]

        [SerializeField] private float _jellyfishThreshold;
        [Space]
        [SerializeField] private Image _menu;

        [SerializeField] private int _waveCount;
        [SerializeField] private int _maxWaveCount;
        [Space]
        [SerializeField] private TextMeshProUGUI _waveCountText;

        private Slider _slider;

        public bool IsPaused => Time.timeScale == 0f;

        private void Pause()
        {
            Time.timeScale = 0f;
            _menu.gameObject.SetActive(true);
        }

        private void Unpause()
        {
            Time.timeScale = 1f;
            ResetSlider();
            _menu.gameObject.SetActive(false);
        }

        private void ResetSlider() => _slider.value = _maxTimeSeconds;

        private void UpdateText() => _waveCountText.text = $"Wave {_waveCount}/{_maxWaveCount}";

        private void Update()
        {
            if (IsPaused)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                    Unpause();

                return;
            }

            _slider.value -= Time.deltaTime;

            if (_slider.value == 0f)
            {
                _waveCount++;
                UpdateText();
                Pause();
            }
        }

        private void Awake()
        {
            _slider = GetComponent<Slider>();

            _slider.minValue = 0f;
            _slider.maxValue = _maxTimeSeconds;

            _menu.gameObject.SetActive(false);

            ResetSlider();
            UpdateText();
        }
    }
}
