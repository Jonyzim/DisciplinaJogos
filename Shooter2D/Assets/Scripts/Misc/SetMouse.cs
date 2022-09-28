using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Constants;

public class SetMouse : MonoBehaviour
{
    public Texture2D CursorTexture;
    public CursorMode CursorMode = CursorMode.Auto;
    public Vector2 HotSpot = Vector2.zero;
    void Start()
    {
        Cursor.SetCursor(CursorTexture, HotSpot, CursorMode);
        Screen.SetResolution(426, 240, true);
    }
}
