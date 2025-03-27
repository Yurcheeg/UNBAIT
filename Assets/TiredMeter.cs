using Assets.UNBAIT.Develop.Gameplay.MarkerScripts;
using UnityEngine;
using UnityEngine.UI;

public class TiredMeter : MonoBehaviour
{
    [SerializeField] private Fisherman _fisherman;

    [SerializeField] private int _value;

    [SerializeField] private int _minimumValue;
    [SerializeField] private int _maximumValue;

    [SerializeField] private float _drainSpeed;

    [SerializeField] private float _reelingMultiplier;

    private Slider _slider;

    private void Start()
    {
        if (_maximumValue == _minimumValue)
        {
            throw new System.Exception("Values cannot be equal");
        }

        _slider.maxValue = _maximumValue;
        _slider.minValue = _minimumValue;
        
    }
    private void Update()
    {
        if(_value <= _slider.minValue)
        {
            _fisherman.IsTired = true;
        }
    }
    private void Awake()
    {
        _slider = GetComponent<Slider>();
    }
}
