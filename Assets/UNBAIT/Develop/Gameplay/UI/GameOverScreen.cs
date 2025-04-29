using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors.Condition;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Assets.UNBAIT.Develop.Gameplay.UI;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Image _resultScreen;
    [SerializeField] private TextMeshProUGUI _ResultScreenText;
    [Header("Game Over Screen")]
    [SerializeField] private string _gameOverText;
    [SerializeField] private string _sceneToLoadOnLoseClick;
    [SerializeField] private string _loseButtonText;
    [Space]

    [Header("Win Screen")]
    [SerializeField] private string _winText;
    [SerializeField] private string _sceneToLoadOnWinClick;
    [SerializeField] private string _winButtonText;

    [Header("Button")]
    [SerializeField] private Button _button;
    [SerializeField] private TextMeshProUGUI _buttonText;

    private void OnFishCaught()
    {
        _resultScreen.gameObject.SetActive(true);

        _ResultScreenText.text = _gameOverText;
        _buttonText.text = _loseButtonText;
        _button.onClick.AddListener(() => SceneManager.LoadScene(_sceneToLoadOnLoseClick));
    }

    private void OnWin()
    {
        _resultScreen.gameObject.SetActive(true);

        _ResultScreenText.text = _winText;
        _buttonText.text = _winButtonText;
        _button.onClick.AddListener(() => SceneManager.LoadScene(_sceneToLoadOnWinClick));
    }

    private void Awake()
    {
        FishCaughtCondition.FishCaught += OnFishCaught;
        LevelTimer.Won += OnWin;
    }

    private void OnDestroy()
    {
        FishCaughtCondition.FishCaught -= OnFishCaught;
        LevelTimer.Won -= OnWin;
    }
}
