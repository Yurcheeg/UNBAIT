using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.UNBAIT.Develop.Gameplay
{
    public static class Cursor
    {
        public static Vector2 GetMousePosition() => Camera.main.ScreenToWorldPoint(Input.mousePosition);

        public static bool IsMouseOverTarget(GameObject target)
        {
            Vector2 mousePos = GetMousePosition();

            //raycasts are unreliable, and single overlap doesn't work
            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);
            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject == target)
                    return true;
            }
            return false;
        }

        public static bool IsMouseOverUI(GameObject target)
        {
            PointerEventData pointerData = new(EventSystem.current)
            {
                position = Input.mousePosition
            };

            List<RaycastResult> results = new();

            EventSystem.current.RaycastAll(pointerData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject == target)
                    return true;
            }

            return false;
        }
    }
}