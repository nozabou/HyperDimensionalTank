using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ResultScript : MonoBehaviour
{
    [SerializeField] private GameObject player1P;
    [SerializeField] private GameObject player2P;
    private int winPlayerIndex;
    private GameObject winTextObj;
    private TextMeshProUGUI winText;
    // Start is called before the first frame update
    void Start()
    {
        winPlayerIndex = PlayerPrefs.GetInt("Winner", 0);
        winTextObj = GameObject.Find("Canvas/WinText");
        winText = winTextObj.GetComponent<TextMeshProUGUI>();
        if(winPlayerIndex == 1)
        {
            winText.text = "1P WIN!!";
            Instantiate(player1P, new Vector3(0,3,-5), Quaternion.identity);
        }
        else if (winPlayerIndex == 2)
        {
            winText.text = "2P WIN!!";
            Instantiate(player2P, new Vector3(0, 3, -5), Quaternion.identity);
        }
        else
        {
            winText.text = "error";
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
