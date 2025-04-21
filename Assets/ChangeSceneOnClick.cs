using Assets.UNBAIT.Develop.Gameplay.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeSceneOnClick : MonoBehaviour
{
    [SerializeField] private string _gameplayScene;
    [SerializeField] private string _tutorialScene;

    [SerializeField] private SwitchSpriteOnClick _tutorialButton;
    private bool IsTutorialChecked => _tutorialButton.IsActive;
    
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();

        _button.onClick.AddListener(() => 
            SceneManager.LoadScene(IsTutorialChecked ? _tutorialScene : _gameplayScene));
    }
}
