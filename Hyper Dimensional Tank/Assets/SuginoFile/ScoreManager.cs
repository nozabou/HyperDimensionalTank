using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject scoreObject = null; // Textオブジェクト

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // オブジェクトからTextコンポーネントを取得
        TextMeshProUGUI scoreText =scoreObject.GetComponent<TextMeshProUGUI>();
        // テキストの表示を入れ替える
        scoreText.text = "000000";
    }
}
