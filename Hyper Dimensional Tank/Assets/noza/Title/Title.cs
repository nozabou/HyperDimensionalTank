using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using TMPro;
using Unity.Properties;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    int cursorNum = 1;
    GameObject cursor;
    public float speed = 1.0f;
    private float time;
    private TextMeshProUGUI gameStartText; 
    private TextMeshProUGUI optionText; 
    GameObject gameStartTextObj;
    GameObject optionTextObj;
    

    // Start is called before the first frame update
    void Start()
    {
        gameStartTextObj = GameObject.Find("GameStartText").gameObject;
        optionTextObj = GameObject.Find("OptionText").gameObject;
        cursor = GameObject.Find("Cursor").gameObject;
        gameStartText = gameStartTextObj.GetComponent<TextMeshProUGUI>();
        optionText = optionTextObj.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        // Wキーを押したらcursorNumに1代入
        if (Input.GetKeyDown(KeyCode.W))
        {
            cursorNum = 1;
            cursor.transform.localPosition = new Vector3(-110,-50,0);
        }
        // Sキーを押したらcursorNumに2代入
        if (Input.GetKeyDown(KeyCode.S))
        {
            cursorNum = 2;
            cursor.transform.localPosition = new Vector3(-110, -110, 0);

        }
        if (cursorNum == 1)
        {
            gameStartText.color = GetTextColorAlpha(gameStartText.color);
            optionText.color = new Color32(0, 0, 0, 255);
        }
        if (cursorNum == 2)
        {
            optionText.color = GetTextColorAlpha(optionText.color);
            gameStartText.color = new Color32(0,0,0,255);
        }

        // スペースキーが押されたら決定
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cursorNum == 1)
            {
                SceneManager.LoadScene("TestScene");
            }
            if (cursorNum == 2)
            {
                Debug.Log("オプション");
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
