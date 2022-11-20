using System;
using UnityEngine;

namespace Src.Utility.Screen
{
    public class CursorHandlerBehavior : MonoBehaviour
    {
        [SerializeField]
        private Texture2D cursorTexture;
        
        [SerializeField]
        private CursorMode cursorMode = CursorMode.Auto;
        
        [SerializeField]
        private Vector2 hotSpot = Vector2.zero;

        private bool mouseEntered;
        
        private void OnMouseEnter()
        {
            mouseEntered = true;
        }

        private void Update()
        {
            if (mouseEntered)
            {
                var texSize = new Vector2(cursorTexture.width, cursorTexture.height);
                Cursor.SetCursor(cursorTexture, hotSpot + texSize / 2, cursorMode);
            }
        }

        private void OnMouseExit()
        {
            mouseEntered = false;
            Cursor.SetCursor(null, Vector2.zero, cursorMode);
        }
    }
}