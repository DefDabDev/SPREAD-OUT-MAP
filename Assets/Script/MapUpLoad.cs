using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MapUpLoad : MonoBehaviour
{
    [SerializeField]
    GameObject target;

    public char blockCode = 'a';
    public int j = 0, i = 0;

    List<StringBuilder> sbSB = new List<StringBuilder>();  // 가로
    Dictionary<int, string> bDic = new Dictionary<int, string>();

    void Start()
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("0, ");
        sbSB.Add(sb);
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
}
