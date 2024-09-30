using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEditor.SceneTemplate;

public class GameManeger : MonoBehaviour
{
    private GameObject playerObj1P;
    private GameObject playerObj2P;
    private PlayerScript playerScript1P;
    private PlayerScript playerScript2P;

    //HpUI
    public Slider hpBar1P;
    public Slider hpBar2P;
    //beamUI
    public Slider beamBar1P;
    public Slider beamBar2P;

    //残機
    public GameObject[] stockUi1P;
    public GameObject[] stockUi2P;

    private int respawnTime1P = 180;
    private int respawnTime2P = 180;
    private GameObject respownPanel1P;
    private GameObject respownPanel2P;
    private GameObject respownTimerObj1P;
    private GameObject respownTimerObj2P;
    private TextMeshProUGUI respownTimerText1P;
    private TextMeshProUGUI respownTimerText2P;

    //リス地
    public GameObject respornPoint1;
    public GameObject respornPoint2;

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        playerObj1P = GameObject.Find("Player1");
        playerObj2P = GameObject.Find("Player2");
        playerScript1P = playerObj1P.GetComponent<PlayerScript>();
        playerScript2P = playerObj2P.GetComponent<PlayerScript>();

        //HPバーをMAXに
        hpBar1P.value = 1;
        hpBar2P.value = 1;
        //ビームのゲージを0に
        beamBar1P.value = 0;
        beamBar2P.value = 0;

        //リスポーンタイマー
        respownPanel1P = GameObject.Find("Canvas/Canvas1P/RespownPanel");
        respownPanel2P = GameObject.Find("Canvas/Canvas2P/RespownPanel");
        respownTimerObj1P = GameObject.Find("Canvas/Canvas1P/RespownPanel/RespownTimer");
        respownTimerObj2P = GameObject.Find("Canvas/Canvas2P/RespownPanel/RespownTimer");
        respownTimerText1P = respownTimerObj1P.GetComponent<TextMeshProUGUI>();
        respownTimerText2P = respownTimerObj2P.GetComponent<TextMeshProUGUI>();
        respownPanel1P.SetActive(false);
        respownPanel2P.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerScript1P.playerStock < 0)
        {
            Debug.Log("あなたの負け");
            return;
        }
        if (playerScript1P.isDead)
        {
            playerObj1P.SetActive(false);
            respownPanel1P.SetActive(true);
            respawnTime1P--;
            respownTimerText1P.text = ((respawnTime1P / 60) + 1).ToString("d1");

            if (respawnTime1P < 0)//復活
            {
                respownPanel1P.SetActive(false);
                stockUi1P[playerScript1P.playerStock].SetActive(false);
                playerScript1P.isDead = false;
                playerScript1P.myHp = 100;
                playerObj1P.SetActive(true);
                respawnTime1P = 180;
                playerObj1P.transform.position = respornPoint1.transform.position;
            }
        }

        if (playerScript2P.playerStock < 0)
        {
            Debug.Log("あなたの負け");
            return;
        }
        if (playerScript2P.isDead)
        {
            playerObj2P.SetActive(false);
            respownPanel2P.SetActive(true);
            respawnTime2P--;
            respownTimerText2P.text = ((respawnTime2P / 60) + 1).ToString("d1");

            if (respawnTime2P < 0)//復活
            {
                respownPanel2P.SetActive(false);
                stockUi2P[playerScript2P.playerStock].SetActive(false);
                playerScript2P.isDead = false;
                playerScript2P.myHp = 100;
                playerObj2P.SetActive(true);
                respawnTime2P = 180;
                playerObj2P.transform.position = respornPoint2.transform.position;
            }
        }

    }

    private void FixedUpdate()
    {
        //HPの変化を描画
        hpBar1P.value = (float)(playerScript1P.myHp) / 100.0f;
        hpBar2P.value = (float)(playerScript2P.myHp) / 100.0f;
        //ビームの描画
        beamBar1P.value = (float)(playerScript1P.beamGauge) / 100.0f;
        beamBar2P.value = (float)(playerScript2P.beamGauge) / 100.0f;
    }
}
