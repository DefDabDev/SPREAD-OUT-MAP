using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapLoad : MonoBehaviour
{
    void Start()
    {
        Debug.Log(readStringFromFile("MapData/map0"));
    }

    string readStringFromFile(string fileName)
    {
        TextAsset textAsset = Resources.Load(fileName) as TextAsset;
        string str = textAsset.text;

        char sp = ',';
        string[] spString = str.Split(sp);

        for (int i = 0; i < spString.Length; i++)
        {
            Debug.Log(i + " " + spString[i]);
        }
        return str;
    }
    string pathForDocuments(string fileName)
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            string path = Application.persistentDataPath;
            path = "jar:file://" + Application.dataPath + "!/assets/";
            //path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, fileName);
        }
        else
        {
            string path = Application.dataPath;
            path = path.Substring(0, path.LastIndexOf('/'));
            return Path.Combine(path, fileName);
        }
    }
}
