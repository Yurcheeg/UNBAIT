using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialInstruction",menuName = "Tutorial/Instruction")]
public class TutorialInstruction : ScriptableObject
{
    [TextArea(2,5)]
    public string[] Messages;

    public List<GameObject> ObjectsToSpawn;
    public int ObjectSpawningMessageIndex;

    public bool _destroyGameObjectsOnComplete;
    public GameObject TargetObject;
    public TutorialTrigger TriggerType;
}

public enum TutorialTrigger
{
    OnClick,
    OnFlip,
    OnItemPicked,
    OnHookUsed,
    Skip
}
