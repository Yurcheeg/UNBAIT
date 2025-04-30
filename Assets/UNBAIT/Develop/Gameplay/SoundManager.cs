using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.FishFlip;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private float _minPitch = -5f;
        [SerializeField] private float _maxPitch = 5f;
        [SerializeField] private AudioSource sfxSource;
        [Space]
        [Header("Fish SFX")]
        [SerializeField] private AudioClip _fishSlap;
        [SerializeField] private AudioClip _fishFlipped;
        [Space]
        [Header("Item SFX")]
        [SerializeField] private AudioClip _itemPicked;
        [SerializeField] private AudioClip _itemUsed;
        [Space]
        [Header("Fisherman SFX")]
        [SerializeField] private AudioClip _fishermanStunned;

        public SoundManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            FlipOnClick.Slapped += OnSlapped;
            FlipOnClick.Flipped += OnFlipped;

            Inventory.Inventory.ItemPicked += OnItemPicked;
            Inventory.Inventory.ItemUsed += OnItemUsed;
        }

        private void OnDestroy()
        {
            FlipOnClick.Slapped -= OnSlapped;
            FlipOnClick.Flipped -= OnFlipped;

            Inventory.Inventory.ItemPicked -= OnItemPicked;
            Inventory.Inventory.ItemUsed -= OnItemUsed;
        }
        #region Fish SFX
        private void OnSlapped() => PlaySoundEffect(_fishSlap);

        private void OnFlipped() => PlaySoundEffect(_fishFlipped);
        #endregion

        #region Item SFX
        private void OnItemUsed() => PlaySoundEffect(_itemUsed);

        private void OnItemPicked() => PlaySoundEffect(_itemPicked);
        #endregion
       
        //Fisherman.OnStunned

        private void PlaySoundEffect(AudioClip source)
        {
            sfxSource.pitch = Random.Range(_minPitch, _maxPitch);
            sfxSource.PlayOneShot(source);
        }
    }
}
