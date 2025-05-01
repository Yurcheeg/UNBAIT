using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.FishFlip;
using Assets.UNBAIT.Develop.Gameplay.Entities;
using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private float _minPitch;
        [SerializeField] private float _maxPitch;
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

            Fisherman.Shocked += OnShocked;
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

        #region Fisherman SFX
        private void OnShocked() => PlaySoundEffect(_fishermanStunned, changePitch: false);
        #endregion


        private void PlaySoundEffect(AudioClip source, bool changePitch = true)
        {
            if (changePitch)
            {
                float originalPitch = sfxSource.pitch;
                sfxSource.pitch = Random.Range(_minPitch, _maxPitch);

                sfxSource.PlayOneShot(source);

                sfxSource.pitch = originalPitch;
            }
            else
                sfxSource.PlayOneShot(source);
        }
    }
}
