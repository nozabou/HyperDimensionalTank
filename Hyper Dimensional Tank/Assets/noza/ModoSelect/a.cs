using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Unity.Properties;
using UnityEngine.SceneManagement;


public class NewBehaviourScript : MonoBehaviour
{
    int cursorNum = 1;
    GameObject cursor;
    public float speed = 1.0f;
    private float time;
    private TextMeshProUGUI single;
    private TextMeshProUGUI multi;
    private TextMeshProUGUI title;
    GameObject singleObj;
    GameObject multiObj;
    GameObject titleObj;

    // Start is called before the first frame update
    void Start()
    {
        singleObj = GameObject.Find("Single").gameObject;
        multiObj = GameObject.Find("Multi").gameObject;
        titleObj = GameObject.Find("Title").gameObject;
        cursor = GameObject.Find("Cursor").gameObject;
        single = singleObj.GetComponent<TextMeshProUGUI>();
        multi = multiObj.GetComponent<TextMeshProUGUI>();
        title =titleObj.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            cursorNum++;
            
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            cursorNum--;
            
        }
        if (cursorNum == 1)
        {
            title.color = GetTextColorAlpha(title.color);
            single.color = new Color32(0, 0, 0, 255);
            multi.color = new Color32(0, 0, 0, 255);
            cursor.transform.localPosition = new Vector3(-230, -144, 0);
        }
        if (cursorNum == 2)
        {
            multi.color = GetTextColorAlpha(multi.color);
            title.color = new Color32(0, 0, 0, 255);
            single.color = new Color32(0, 0, 0, 255);
            cursor.transform.localPosition = new Vector3(-300, -38, 0);
        }
        if (cursorNum == 3)
        {
            single.color = GetTextColorAlpha(single.color);
            multi.color = new Color32(0, 0, 0, 255);
            title.color = new Color32(0, 0, 0, 255);
            cursor.transform.localPosition = new Vector3(-363, 66, 0);
        }
        // スペースキーが押されたら決定
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cursorNum == 1)
            {
                Debug.Log("ひとりで");
            }
            if (cursorNum == 2)
            {
                Debug.Log("みんなで");
            }
            if (cursorNum == 2)
            {
                Debug.Log("タイトルに戻る");
            }
        }
    }
    Color GetTextColorAlpha(Color color)
    {
        time += Time.deltaTime * speed * 5.0f;
        color.a = Mathf.Sin(time);

        return color;
    }
}
