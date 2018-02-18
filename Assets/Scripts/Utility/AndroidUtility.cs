using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class AndroidUtility {

    public static bool SaveExternally(string a_text)
    {
        string path = "";
        try
        {
            AndroidJavaClass jc = new AndroidJavaClass("android.os.Environment");
            path = jc.CallStatic<AndroidJavaObject>("getExternalStorageDirectory").Call<string>("getAbsolutePath");

            if(!Directory.Exists("/sdcard/palashFiles"))
            {
                Directory.CreateDirectory("/sdcard/palashFiles");

            }
            StreamWriter sw = System.IO.File.CreateText("/sdcard/palashFiles/2018.txt");
            sw.Close();

            System.IO.File.WriteAllText("/sdcard/palashFiles/2018.txt", a_text);
            return true;
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
            return false;
        }
    }
}
