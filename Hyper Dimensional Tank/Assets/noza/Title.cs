using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Title : MonoBehaviour
{
    int cursorNum = 1;
    GameObject cursor;
    // 点滅させる対象
    private Renderer _Cursor;
    // 点滅周期
    private float _cycle = 1;

    private double _time;

    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("Cursor").gameObject;
       // _Cursor = ;
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

        // スペースキーが押されたら決定
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (cursorNum == 1)
            {
                Debug.Log("ゲームスタート");
            }
            if (cursorNum == 2)
            {
                Debug.Log("オプション");
            }
        }
        // 内部時刻を経過させる
        _time += Time.deltaTime;
        // 周期cycleで繰り返す値の取得
        // 0~cycleの範囲の値が得られる
        var repeatValue = Mathf.Repeat((float)_time, _cycle);
        // 内部時刻timeにおける明滅状態を反映
        _Cursor.enabled = repeatValue >= _cycle * 0.5f;
    }
}
