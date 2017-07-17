using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapLoad : MonoBehaviour
{
    void Start()
    {
        //readStringFromFile("map1");
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
}
