using System.IO;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// File handler Class. This class Handles File related activities, like Reading data from file, saving data into the file
/// </summary>
public partial class FileHandler {

    /// <summary>
    /// This class reads the data from the file & return the key-value as Hashtable
    /// </summary>
    /// <returns>The data as Hashtable</returns>
    /// <param name="a_fileName">name of the file</param>
    public static Hashtable GetData(string a_fileName, ref Text a_Text)
    {
        Hashtable l_dataHash = new Hashtable();

        //Load the file as TextAsset & Read via StreamReader Object
        TextAsset l_txtAsst = Resources.Load(a_fileName) as TextAsset;
        StreamReader l_fileReader = new StreamReader(new MemoryStream(l_txtAsst.bytes));

        //Iterate till we don't reach the end of the file
        while (!l_fileReader.EndOfStream)
        {
            //Read next line
            string l_line = l_fileReader.ReadLine();
            string[] l_data = l_line.Split('=');

            //store the splitted data into the hashtable
            l_dataHash[l_data[0]] = l_data[1];
            a_Text.text = a_Text.text+("key: "+l_data[0]+". value: "+l_data[1]);

        }
        l_fileReader.Close();
        return l_dataHash;

    }
    public static string GetData(string a_fileName)
    {
        string l_string = "";
        string path = "";
       
#if UNITY_EDITOR
        path = Application.dataPath + "/Resources/" + a_fileName + ".txt";
#else
        path = Application.persistentDataPath + a_fileName + ".txt";
#endif

        l_string = System.IO.File.ReadAllText(path);

        return l_string;
    }

    /// <summary>
    /// Sets the data entirely in a file
    /// </summary>
    /// <returns><c>true</c>, if data was set, <c>false</c> otherwise.</returns>
    /// <param name="a_fileName">name of the file</param>
    /// <param name="a_data">data to set</param>
    public static bool SetData(string a_fileName, string a_data)
    {

        TextAsset l_txtAsst = Resources.Load<TextAsset>(a_fileName);

        string path = Application.persistentDataPath + a_fileName + ".txt";
#if UNITY_EDITOR
        path = Application.dataPath + "/Resources/" + a_fileName + ".txt";
#endif
        File.WriteAllText(path,a_data);

        return true;
    }
}
