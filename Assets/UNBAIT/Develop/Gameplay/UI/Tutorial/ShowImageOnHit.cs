using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.FishFlip;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowImageOnHit : MonoBehaviour
{
    [SerializeField] private Image _image;

    [SerializeField] private float _showImageDelaySeconds;
    [SerializeField] private float _showImageDurationSeconds;
    
    private FlipOnClick _flipOnClick;

    private void OnCanFlip() => StartCoroutine(ShowImage());

    private IEnumerator ShowImage()
    {
        yield return new WaitForSeconds(_showImageDelaySeconds);

        _image.gameObject.SetActive(true);

        yield return new WaitForSeconds(_showImageDurationSeconds);

        _image.gameObject.SetActive(false);
    }

    private void Start() => _image.gameObject.SetActive(false);

    private void Awake()
    {
        _flipOnClick = GetComponent<FlipOnClick>();
        _flipOnClick.CanFlip += OnCanFlip;
    }

    private void OnDestroy() => _flipOnClick.CanFlip -= OnCanFlip;
}
