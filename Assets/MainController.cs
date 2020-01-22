using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MainController : MonoBehaviour {
    private void OnApplicationQuit() {
        PlayerPrefs.SetInt("times", PlayerPrefs.GetInt("times", 0) + 1);
    }

    public void Share() {
        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());


        // To avoid memory leaks
        Destroy(ss);

        new NativeShare().AddFile(filePath).SetSubject(PlayerPrefs.GetInt("times", 0).ToString()).SetText("Hello world!").Share();
    }
}