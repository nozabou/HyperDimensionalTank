using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public GameObject scoreObject = null; // Textオブジェクト

    private int score = 0;

    // Start is called before the first frame update
    void Start()
    {
        //// オブジェクトからTextコンポーネントを取得
        //TextMeshProUGUI scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        //// テキストの表示を入れ替える
        //scoreText.text = "Score00000" + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // スコアを増加させるメソッド
    // 外部からアクセスするからpublicで定義させる
    public void AddScore(int amount)
    {
        score += amount;
        // オブジェクトからTextコンポーネントを取得
        TextMeshProUGUI scoreText = scoreObject.GetComponent<TextMeshProUGUI>();
        scoreText.text = "Score" + score;

    }
}
