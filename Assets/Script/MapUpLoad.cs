using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class MapUpLoad : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    [SerializeField]
    GameObject block;

    public char blockCode = 'a';
    public int j = 0, i = 0;

    List<StringBuilder> sbSB = new List<StringBuilder>();  // 가로
    Dictionary<int, string> bDic = new Dictionary<int, string>();

    string path = "";
    FileStream file = null;
    StreamWriter swNote = null;
    float zOrder = 0;

    string mapName = "0";

    void Start()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("0, ");
        sbSB.Add(sb);

    }

    public void load()
    {
        path = pathForDocuments("Assets/Resources/MapData/map" + mapName + ".txt");
        file = new FileStream(path, FileMode.Create, FileAccess.Write);
        swNote = new StreamWriter(file);
        //Debug.Log(readStringFromFile("MapData/map" + mapName));

       // StartCoroutine("bringWaiting");
    }

    public void chgMapName(UnityEngine.UI.Text txt)
    {
        mapName = txt.text;
    }

    //string readStringFromFile(string fileName)
    //{
    //    TextAsset textAsset = Resources.Load(fileName) as TextAsset;
    //    string str = textAsset.text;

    //    string[] spString = str.Split('\n');

    //    for (int i = 0; i < spString.Length; i++)
    //    {
    //        sbSB[i].Append(spString[i]);
            
    //        string[] code = spString[i].Split(',');
    //        for (int j = 0; j < code.Length; j++)
    //        {
    //            if (!code[j].Equals("0"))
    //            {
    //                GameObject obj = Instantiate(block) as GameObject;
    //                obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Tile/Harlem/" + blockCode.ToString());
    //                obj.transform.parent = transform;
    //                obj.transform.localPosition = new Vector2(3 * j, 3 * i);
    //                obj.transform.localScale = Vector2.one;
    //            }
    //        }
    //    }
    //    return str;
    //}

    IEnumerator bringWaiting()
    {
        while (swNote.Equals(null) || file.Equals(null))
            yield return null;

        TextAsset textAsset = Resources.Load("MapData/map" + mapName) as TextAsset;
        string str = textAsset.text;

        string[] spString = str.Split('\n');

        for (int i = 0; i < spString.Length; i++)
        {
            if (sbSB.Count > i)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append("");
                sbSB.Add(sb);
            }

            sbSB[i].Append(spString[i]);

            string[] code = spString[i].Split(',');
            for (int j = 0; j < code.Length; j++)
            {
                if (!code[j].Equals("0"))
                {
                    GameObject obj = Instantiate(block) as GameObject;
                    obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Tile/Harlem/" + blockCode.ToString());
                    obj.transform.parent = transform;
                    obj.transform.localPosition = new Vector2(3 * j, 3 * i);
                    obj.transform.localScale = Vector2.one;
                }
            }
        }
    }


    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow)) Camera.main.transform.position += Vector3.right * Time.deltaTime * 10;
        else if (Input.GetKey(KeyCode.LeftArrow)) Camera.main.transform.position += Vector3.left * Time.deltaTime * 10;
        else if (Input.GetKey(KeyCode.UpArrow)) Camera.main.transform.position += Vector3.up * Time.deltaTime * 10;
        else if (Input.GetKey(KeyCode.DownArrow)) Camera.main.transform.position += Vector3.down * Time.deltaTime * 10;

        if (Input.GetKeyDown(KeyCode.W))
        {
            target.transform.position += new Vector3(0, 3);
            j++; check();
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            target.transform.position -= new Vector3(0, 3);
            j--; check();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            target.transform.position -= new Vector3(3, 0);
            i--; check();
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            target.transform.position += new Vector3(3, 0);
            i++; check();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            sbSB[j].Replace(sbSB[j][3 * i], blockCode, 3 * i, 1);

            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < sbSB.Count; k++)
                sb.Append(sbSB[k] + "\n");
            Debug.Log(sb);

            GameObject obj = Instantiate(block, target.transform.position, Quaternion.identity) as GameObject;
            obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Tile/Harlem/" + blockCode.ToString() );

            zOrder += 0.001f;
            obj.transform.position -= new Vector3(0,0,zOrder);
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            sbSB[j].Replace(sbSB[j][3 * i], '0', 3 * i, 1);

            StringBuilder sb = new StringBuilder();
            for (int k = 0; k < sbSB.Count; k++)
                sb.Append(sbSB[k] + "\n");
            Debug.Log(sb);

            GameObject obj = Instantiate(block, target.transform.position, Quaternion.identity) as GameObject;
            obj.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Sprite/Tile/0");

            zOrder += 0.001f;
            obj.transform.position -= new Vector3(0, 0, zOrder);
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            for (int k = 0; k < sbSB.Count; k++)
                swNote.WriteLine(sbSB[k]);
            Debug.Log("FFFFF");
            swNote.Close();
            file.Close();
        }
    }

    int idx = 0;
    public void check()
    {
        if (sbSB.Count <= j)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("");
            sbSB.Add(sb);
        }

        while (sbSB[j].Length <= 3 * i)
        {
            sbSB[j].Append("0, ");
        }
    }

    public void chgBlockCode(string s)
    {
        blockCode = s[0];
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
