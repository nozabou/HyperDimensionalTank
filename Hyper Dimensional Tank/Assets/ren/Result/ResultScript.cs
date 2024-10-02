using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class ResultScript : MonoBehaviour
{
    [SerializeField] private GameObject player1P;
    [SerializeField] private GameObject player2P;
    private int winPlayerIndex;
    private GameObject winTextObj;
    private TextMeshProUGUI winText;

    //‚Ç‚¿‚ç‚ğ‘I‘ğ‚µ‚Ä‚¢‚é‚©‚ğ•Ï”‚ÅŠÇ—
    private int selectIndex = 1;
    
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

    void FixedUpdate()
    {
        if(selectIndex == 1)
        {
            Debug.Log("¶");
        }
        else
        {
            Debug.Log("‰E");
        }
    }

    public void OnSelect(InputAction.CallbackContext context)
    {
        // “ü—Í’l‚ğ•Û‚µ‚Ä‚¨‚­
        Vector2 inputStick = context.ReadValue<Vector2>();
        if (inputStick.x < -0.5f)
        {
            selectIndex = 1;
        }
        if (inputStick.x > 0.5f)
        {
            selectIndex = 2;
        }
    }
}
