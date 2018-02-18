using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AndroidUtility {

    public static string GetAndroidExternalStoragePath()
    {
        string path = "";
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("android.os.Environment");
            path = jc.CallStatic<AndroidJavaObject>("getExternalStorageDirectory").Call<string>("getAbsolutePath");

            if(!Directory.Exists("/sdcard/dipalash"))
            {
                Directory.CreateDirectory("/sdcard/dipalash");

            }

            File.Create("/sdcard/dipalash/2018.txt");
            File.WriteAllText(path + "/sdcard/dipalash/2018.txt", "yo baby");
            return path;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return e.Message;
        }
    }
}
