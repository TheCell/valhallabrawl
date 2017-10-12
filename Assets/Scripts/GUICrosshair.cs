using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUICrosshair : MonoBehaviour {
    public Texture2D crosshairTexture  ;
    private Rect position;
    private Rect position2;
    static bool OriginalOn = true;

    void Start()
    {
        position = new Rect((Screen.width/2 + crosshairTexture.width/2) / 2, (Screen.height) / 2, crosshairTexture.width, crosshairTexture.height);
        position2 = new Rect((Screen.width + Screen.width/2 + crosshairTexture.width/2) / 2, (Screen.height) / 2, crosshairTexture.width, crosshairTexture.height);
    }

    void OnGUI()
    {
        if (OriginalOn == true)
        {
            GUI.DrawTexture(position, crosshairTexture);
            GUI.DrawTexture(position2, crosshairTexture);
        }
    }
}
