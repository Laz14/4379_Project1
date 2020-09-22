using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFlash : MonoBehaviour
{
    [SerializeField] Texture2D texture;
    private bool showTexture = false;
    private float timer = .05f;
    private bool timerIsActive = false;

    public void Flash()
    {
        showTexture = true;
    }

    void OnGUI()
    {
        if (showTexture)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), texture, ScaleMode.StretchToFill, false);
            timerIsActive = true;
        }
    }

    private void Update()
    {
        if (timerIsActive)
        {
            timer -= Time.deltaTime;
        }

        if (timer < 0)
        {
            showTexture = false;
            timer = .02f;
            timerIsActive = false;
        }
    }
}
