using Assets.UNBAIT.Develop.Gameplay.Entities;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TiredMeter : MonoBehaviour
{
    [SerializeField] private Fisherman _fisherman;
    [Space]
    [SerializeField] private float _value;

    [SerializeField] private int _minimumValue;
    [SerializeField] private int _maximumValue;
    [Space]
    [SerializeField] private float _drainSpeed;
    [SerializeField] private float _drainRate;
    [Space]
    [SerializeField] private float _reelingMultiplier;

    private Slider _slider;

    public float SliderValue => _value;

    private IEnumerator DrainAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        Drain();

        StartCoroutine(DrainAfterDelay(delay));
    }

    private void Drain()
    {
        _value -= _fisherman.IsReeling ? _drainRate * _reelingMultiplier : _drainRate;

        _slider.value = _value;
    }

    private void Start()
    {
        if (_maximumValue == _minimumValue)
            throw new System.Exception("Values cannot be equal");
        
        _slider.minValue = _minimumValue;
        _slider.maxValue = _maximumValue;
        _slider.value = _maximumValue;
        _value = _maximumValue;

        StartCoroutine(DrainAfterDelay(_drainRate));
    }
    private void Update()
    {
        if (_fisherman.IsTired)
            return;

        if(_value <= _slider.minValue)
            _fisherman.IsTired = true;
    }

    private void Awake() => _slider = GetComponent<Slider>();
}
