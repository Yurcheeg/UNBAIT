using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.UI
{
    public class EnableOnButtonPress : MonoBehaviour
    {
        [SerializeField] private GameObject _gameObject;
        [SerializeField] private KeyCode _buttonToPress;

        private void Update()
        {
            if (Input.GetKeyDown(_buttonToPress))
                _gameObject.gameObject.SetActive(true);
        }
    }
}
