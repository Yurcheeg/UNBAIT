using UnityEngine;
using UnityEngine.UI;

namespace Assets.UNBAIT.Develop.Gameplay.UI
{
    public class SwitchSpriteOnClick : MonoBehaviour
    {
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _inactiveSprite;

        public bool IsActive { get; private set; }

        private Image _image;
        private Button _button;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _button = GetComponent<Button>();

            _button.onClick.AddListener(() =>
            {
                IsActive = !IsActive;
                UpdateSprite();
            });

            UpdateSprite();
        }

        private void UpdateSprite() => _image.sprite = IsActive ? _activeSprite : _inactiveSprite;
    }
}
