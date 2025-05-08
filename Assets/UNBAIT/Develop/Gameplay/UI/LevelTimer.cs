using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UNBAIT.Develop.Gameplay.UI
{
    [RequireComponent(typeof(Slider))]
    public class LevelTimer : MonoBehaviour
    {
        public static event Action Won;
        public static event Action<int> WaveCleared;

        [SerializeField] private float _maxTimeSeconds;

        [Space]

        [SerializeField] private float _jellyfishThreshold;
        [Space]
        [SerializeField] private Image _menu;
        [SerializeField] private Button _continueButton;

        [Min(1)]
        [SerializeField] private int _currentWaveCount;
        [SerializeField] private int _maxWaveCount;
        [Space]
        [SerializeField] private TextMeshProUGUI _waveCountText;

        private Slider _slider;

        public float SliderValue => _slider.value;

        public int CurrentWave => _currentWaveCount;

        public static bool IsPaused => Time.timeScale == 0f;

        private void Pause()
        {
            Time.timeScale = 0f;

            if (_currentWaveCount > _maxWaveCount)
                Won?.Invoke();
            else
                _menu.gameObject.SetActive(true);
        }

        private void Unpause()
        {
            Time.timeScale = 1f;
            ResetSlider();
            _menu.gameObject.SetActive(false);
        }

        private void ResetSlider() => _slider.value = _maxTimeSeconds;

        private void UpdateText() => _waveCountText.text = $"Wave {_currentWaveCount}/{_maxWaveCount}";

        private void Update()
        {
            if (IsPaused)
                return;


            _slider.value -= Time.deltaTime;

            if (_slider.value == 0f)
            {
                _currentWaveCount++;

                if (_currentWaveCount > _maxWaveCount)
                {
                    Won?.Invoke();
                }
                else
                {
                    UpdateText();
                    WaveCleared?.Invoke(_currentWaveCount);
                }

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

            _continueButton.onClick.AddListener(Unpause);
        }
    }
}
