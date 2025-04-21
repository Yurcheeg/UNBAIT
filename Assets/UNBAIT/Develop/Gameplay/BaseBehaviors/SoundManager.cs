using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.BaseBehaviors
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private float _minPitch = -5f;
        [SerializeField] private float _maxPitch = 5f;
        [SerializeField] private AudioSource sfxSource;
        [Space]
        [SerializeField] private AudioClip _fishSlap;
        public SoundManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                Destroy(gameObject);

            FlipOnClick.Hit += OnHit;
        }
        private void OnDestroy()
        {
            FlipOnClick.Hit -= OnHit;
        }

        private void OnHit()
        {
            PlaySoundEffect(_fishSlap);
        }

        private void PlaySoundEffect(AudioClip source)
        {
            sfxSource.pitch = UnityEngine.Random.Range(_minPitch, _maxPitch);
            sfxSource.PlayOneShot(source);
        }
    }
}
