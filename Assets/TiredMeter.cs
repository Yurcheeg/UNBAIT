using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using System;
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


    private IEnumerator DrainAfterDelay(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        Drain();

        StartCoroutine(DrainAfterDelay(delay));
    }

    private void Drain()
    {
        bool isReeling = false;
        _value -= isReeling ? _drainRate * _reelingMultiplier : _drainRate;

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
        if(_value <= _slider.minValue)
            _fisherman.IsTired = true;
    }
    //private void FixedUpdate()
    //{
    //    //_value = _value <= _minimumValue ? _value  
    //}
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
}
