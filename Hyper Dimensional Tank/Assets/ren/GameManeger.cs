using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneTemplate;

public class GameManeger : MonoBehaviour
{
    private GameObject playerObj;
    private PlayerScript playerScript;

    //HpUI
    public Slider hpBar;

    //残機
    public GameObject[] stockUi;

    private int respawnTime = 180;
    private GameObject respownPanel;
    private GameObject respownTimerObj;
    private TextMeshProUGUI respownTimerText;
    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        playerObj = GameObject.Find("Player1");
        playerScript = playerObj.GetComponent<PlayerScript>();

        hpBar.value = 1;

        //リスポーンタイマー
        respownPanel = GameObject.Find("Canvas/RespownPanel");
        respownTimerObj = GameObject.Find("Canvas/RespownPanel/RespownTimer");
        respownTimerText = respownTimerObj.GetComponent<TextMeshProUGUI>();
        respownPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript.playerStock < 0)
        {
            Debug.Log("あなたの負け");
            return;
        }
        if (playerScript.isDead)
        {
            playerObj.SetActive(false);
            respownPanel.SetActive(true);
            respawnTime--;
            respownTimerText.text = ((respawnTime / 60) + 1).ToString("d1");

            if (respawnTime < 0)//復活
            {
                respownPanel.SetActive(false);
                stockUi[playerScript.playerStock].SetActive(false);
                playerScript.isDead = false;
                playerScript.myHp = 100;
                playerObj.SetActive(true);
                respawnTime = 180;
                playerObj.transform.position = new Vector3(0, 10, 0);
            }
        }
       
    }

    private void FixedUpdate()
    {
        hpBar.value = (float)(playerScript.myHp) / 100.0f;
    }
}
