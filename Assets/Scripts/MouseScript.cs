using UnityEngine;

public class MouseScript : MonoBehaviour
{
    public Texture2D cursorSprite;
    public Texture2D cursorClickedSprite;
    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1) || Input.GetMouseButtonDown(2))
        {
            Cursor.SetCursor(cursorClickedSprite, Vector2.zero, CursorMode.Auto);
        }
        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1) || Input.GetMouseButtonUp(2))
        {
            Cursor.SetCursor(cursorSprite, Vector2.zero, CursorMode.Auto);
        }
    }

}
