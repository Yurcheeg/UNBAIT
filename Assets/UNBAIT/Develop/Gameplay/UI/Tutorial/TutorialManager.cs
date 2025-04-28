using System.Collections.Generic;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Cursor = Assets.UNBAIT.Develop.Gameplay.Cursor;
using UnityEngine.SceneManagement;
using Assets.UNBAIT.Develop.Gameplay.BaseBehaviors;
using Assets.UNBAIT.Develop.Gameplay.Entities;
using Assets.UNBAIT.Develop.Gameplay.Inventory;

public class TutorialManager : MonoBehaviour
{
    [SerializeField] private List<TutorialInstruction> _instructions;
    [SerializeField] private TextMeshProUGUI _tutorialText;
    [Space]
    [SerializeField] private Button _button;
    [SerializeField] private Button _restartButton;

    private GameObject _targetObject;
    private List<GameObject> _spawnedObjects = new();
    private bool _isTargetFlipped;
    private bool _restartButtonPressed;

    private IEnumerator RunTutorial()
    {
        foreach (var instruction in _instructions)
        {
        RestartInstruction:

            _isTargetFlipped = false;
            _restartButtonPressed = false;

            yield return ShowMessages(instruction.Messages);
            if (instruction.TargetObject != null)
            {
                _targetObject = Instantiate(instruction.TargetObject, transform);
                if (_targetObject.TryGetComponent<Flip>(out Flip flip))
                    flip.Flipped += OnFlip;
            }

            if (instruction.ObjectsToSpawn.Count != 0)
            {
                foreach (var @object in instruction.ObjectsToSpawn)
                {
                    if (@object != null)
                        _spawnedObjects.Add(Instantiate(@object, transform));
                }
            }

            yield return new WaitUntil(() => CheckInstruction(instruction) || _restartButtonPressed);

            //basically repeating the same as code bellow but with goto restarting everything
            if (_restartButtonPressed)
            {
                if (_targetObject != null)
                    Destroy(_targetObject);

                if (_spawnedObjects.Count != 0)
                {
                    foreach (GameObject gameObject in _spawnedObjects)
                    {
                        if (gameObject != null)
                            Destroy(gameObject);
                    }
                    _spawnedObjects.Clear();
                }

                goto RestartInstruction;
            }

            yield return new WaitForSeconds(0.5f);

            if (instruction._destroyGameObjectsOnComplete)
            {
                if (_targetObject != null)
                    Destroy(_targetObject);

                if (_spawnedObjects.Count != 0)
                {
                    foreach (GameObject gameObject in _spawnedObjects)
                    {
                        if (gameObject != null)
                            Destroy(gameObject);
                    }
                    _spawnedObjects.Clear();
                }
            }
        }

        _button.gameObject.SetActive(true);
        _button.onClick.AddListener(() => SceneManager.LoadScene("Gameplay"));
    }

    private void OnFlip()
    {
        _isTargetFlipped = true;
        _targetObject.GetComponent<Flip>().Flipped -= OnFlip;
    }

    private bool CheckInstruction(TutorialInstruction instruction)
    {
        return instruction.TriggerType switch
        {
            TutorialTrigger.OnClick => Input.GetMouseButtonDown(0) && Cursor.IsMouseOverUI(_targetObject),
            TutorialTrigger.OnFlip => _isTargetFlipped,
            TutorialTrigger.OnHookUsed => _targetObject != null && _targetObject.GetComponent<Hook>().InUse,
            TutorialTrigger.OnItemPicked => _targetObject != null && Inventory.Instance.Items.Contains(_targetObject.GetComponent<Item>()),
            TutorialTrigger.Skip => true,
            _ => false,
        };
    }

    private IEnumerator ShowMessages(string[] messages)
    {
        for (int i = 0; i < messages.Length; i++)
        {
            if (_restartButtonPressed)
                yield break;

            _tutorialText.text = messages[i];

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) || _restartButtonPressed);

            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space) == false); // prevents double skip
        }
    }

    private void Start()
    {
        _button.gameObject.SetActive(false);
        _restartButton.onClick.AddListener(() => _restartButtonPressed = true);

        StartCoroutine(RunTutorial());
    }
}
