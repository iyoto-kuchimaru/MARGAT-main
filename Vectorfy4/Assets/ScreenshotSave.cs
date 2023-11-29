using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotSave : MonoBehaviour
{
    public void GetScreenshot()
    {
        string timestamp = DateTime.Now.ToString("HH_mm_ss");
        ScreenCapture.CaptureScreenshot("MARGAT_" + timestamp + ".png");
    }

}