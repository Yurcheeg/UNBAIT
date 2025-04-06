using UnityEngine;

namespace Assets.UNBAIT.Develop.Gameplay.MarkerScripts
{
    public static class Cursor
    {
        public static Vector2 GetMousePosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        public static bool IsMouseOverTarget(GameObject target)
        {
            Vector2 mousePos = GetMousePosition();

            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);//HACK: expensive. consider other options(IPointerDownHandler)
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject == target)
                    return true;
            }
            return false;
        }
    }
}