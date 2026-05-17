using UnityEngine;
public class CursorChanger : MonoBehaviour
{
    public Texture2D cursorNormal;
    public Texture2D cursorClick;
    public Vector2 hotspot = Vector2.zero;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.SetCursor(cursorClick, hotspot, CursorMode.Auto);
        }

        if (Input.GetMouseButtonUp(0))
        {
            Cursor.SetCursor(cursorNormal, hotspot, CursorMode.Auto);
        }
    }
}

