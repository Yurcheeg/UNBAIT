using UnityEngine;

public sealed class CursorFollow : MonoBehaviour
{
    private void Update() => transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
}
