using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay
{
    public sealed class CursorFollow : MonoBehaviour
    {
        private void Update() => transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
