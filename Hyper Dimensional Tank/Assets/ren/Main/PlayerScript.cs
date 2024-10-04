using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Transactions;
using TMPro.EditorUtilities;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEditor.Experimental.GraphView.GraphView;
using static UnityEditor.Timeline.TimelinePlaybackControls;
using static UnityEngine.EventSystems.StandaloneInputModule;

public class PlayerScript : MonoBehaviour
{
    public string playerIndex;

    //プレイヤーの動き
    private float moveSpeed = 20f;
    private float tempSpeed = 0f;
    //private Vector3 moveVec;
    private Vector2 inputMove;
    private Rigidbody playerRb;

    private bool isLeft = false;
    private bool isRight = false;

    private float bodyRotateSpeed = 50f;
    private float headRotateSpeed = 50f;
    //Vector3 moveSpeed = new Vector3(0, 0, 1f);
    //float moveSpeed = 5f;
    private Transform head;

    //弾の動き
    //弾の発射場所
    [SerializeField] private GameObject shotPoint;

    //弾
    [SerializeField] private GameObject bulletNomal;
    [SerializeField] private GameObject bulletStrong;
   

    //弾の速さ
    private float nomalBulletSpeed = 600f;
    private float strongBulletSpeed = 400f;

    //体力 publicでよい
    public int myHp = 100;
    public bool isDead = false;
    public int playerStock = 2;

    //復活したときに無敵  public OK
    public bool isInvincibility = false;
    private int countInvincibility = 0; //無敵時間まで数える
    private int invincibilityTime = 180;  //無敵時間のフレーム数 //3秒

    //無敵ちかちか
    [SerializeField] private GameObject headObj;
    Color32 colorOrigin = new Color32(255, 255, 255, 1);
    Color32 colorChange = new Color32(50, 50, 50, 1);

    //爆発
    [SerializeField] private GameObject deadExplosion = null;


    //クールタイム
    private int coolTime;
    private int canCoolTime = 60;

    private bool isShotNomal = true;
    private bool isShotStrong = true;
    //ビーム(必殺技)
    [SerializeField] private GameObject bulletBeam;
    private bool isShotBeam = false;
    public float beamGauge = 0;
    private bool isCharge = false;
    //ビームの全体フレーム
    private int beamFream = 120;
    private int beamFreamCount = 0;
    private bool isBeamCount = false;

    //InputSystem
    private PlayerControl playerControl;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = this.transform.GetComponent<Rigidbody>();
        //moveVec = new Vector3(0,0,moveSpeed);
        head = transform.GetChild(0);
        playerControl = new PlayerControl();
        playerControl.Enable();
        tempSpeed = moveSpeed;
    }

    // Update is called once per frame
    void Update()
    {
    
        if (isDead)
        {
            return;
        }
        if(this.transform.position.y < -3)
        {
            myHp = 0;
        }
        //無敵時間のカウント
        if(isInvincibility)
        {
            Color32 color = colorOrigin;
            if (countInvincibility > invincibilityTime)
            {
                isInvincibility = false;
                countInvincibility = 0;
                GetComponent<Renderer>().material.color = color;
                headObj.GetComponent<Renderer>().material.color = color;
            }
            countInvincibility++;
            if((float)(countInvincibility) % 6 == 0)
            {
                color = colorChange;
            }
            GetComponent<Renderer>().material.color = color;
            headObj.GetComponent<Renderer>().material.color = color;
        }
        /////////////////////////////////////////////////////
        ///ビーム
        if (isBeamCount)
        {
            beamFreamCount++;
            if (beamFreamCount > beamFream)
            {
                beamFreamCount = 0;
                isBeamCount = false;
                isShotBeam = false ;
            }
            return;
        }

        if (isCharge)
        {
            if (beamGauge > 100.0f)
            {
                beamGauge = 100.0f;
                isShotBeam = true;
            }
            else
            {
                beamGauge += 0.5f;
            }
        }

      
        //////////////////////////////////////////////////

        coolTime++;
        if(coolTime > canCoolTime)
        {
            isShotNomal = true;
            isShotStrong = true;
        }
        if(coolTime > 20)
        {
            moveSpeed = tempSpeed;
        }

        //プレイヤーの移動
        playerRb.AddForce(this.transform.forward * inputMove.y * moveSpeed * Time.deltaTime, ForceMode.Impulse);


        //this.transform.Translate(inputMove.y * moveVec * Time.deltaTime);
        //Quaternion.AngleAxis(度数法, 軸);
        this.transform.rotation *= Quaternion.AngleAxis(inputMove.x * bodyRotateSpeed * Time.deltaTime, Vector3.up);
        
        if (isLeft)
        {
            //Quaternion.AngleAxis(度数法, 軸);
            head.rotation *= Quaternion.AngleAxis(-headRotateSpeed * Time.deltaTime, Vector3.up);
        }
        if (isRight)
        {
            //Quaternion.AngleAxis(度数法, 軸);
            head.rotation *= Quaternion.AngleAxis(headRotateSpeed * Time.deltaTime, Vector3.up);
        }

        if (myHp <= 0)
        {
            isDead = true;
            Instantiate(deadExplosion, this.transform.position, Quaternion.identity);
            playerStock--;
        }

    }
 
    public void OnShotNomal(InputAction.CallbackContext context)
    {
        moveSpeed = 0;
        if (isShotNomal)
        {
            //弾の発射する場所を取得する
            Vector3 bulletPosition = shotPoint.transform.position;
            //
            GameObject newBullet = Instantiate(bulletNomal, bulletPosition, head.gameObject.transform.rotation);
            Vector3 dir = newBullet.transform.forward;
            newBullet.GetComponent<Rigidbody>().AddForce(dir * nomalBulletSpeed * Time.deltaTime, ForceMode.Impulse);
            coolTime = 0;
            isShotStrong = false;
            isShotNomal = false;
        }
    }
    public void OnShotStrong(InputAction.CallbackContext context)
    {
        moveSpeed = 0;
        if (isShotStrong)
        {
            //弾の発射する場所を取得する
            Vector3 bulletPosition = shotPoint.transform.position;
            //
            GameObject newBullet = Instantiate(bulletStrong, bulletPosition, head.gameObject.transform.rotation);
            Vector3 dir = newBullet.transform.forward;
            newBullet.GetComponent<Rigidbody>().AddForce(dir * strongBulletSpeed * Time.deltaTime, ForceMode.Impulse);
            coolTime = 0;
            isShotNomal = false;
            isShotStrong = false;
        }
    }

    public void OnShotBeam(InputAction.CallbackContext context)
    {
        if (context.started && isShotBeam) // ボタンを押したとき
        {
            isBeamCount = true;
            if (beamFreamCount > 60)
            {
                //弾の発射する場所を取得する
                Vector3 bulletPosition = shotPoint.transform.position;
                //
                GameObject newBullet = Instantiate(bulletBeam, bulletPosition, head.gameObject.transform.rotation);
                // Vector3 dir = newBullet.transform.forward;
                beamGauge = 0;

                isShotBeam = false;
                Destroy(newBullet, 1); //10秒後に弾を消す
            }
          
        }
    }

    public void OnCharge(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //押した瞬間の処理
            isCharge = true;
        }
        if (context.canceled)
        {
            //離した瞬間の処理
            isCharge = false;
        }
       
       
    }

        public void OnMove(InputAction.CallbackContext context)
    {
        // 入力値を保持しておく
        inputMove = context.ReadValue<Vector2>();
     
    }

    public void HeadRotationLeft(InputAction.CallbackContext context)
    {
        if (context.started) // ボタンを押したとき
        {
            isLeft = true;
        }
        else if (context.canceled) // ボタンを離したとき
        {
            isLeft = false;
        }
    }

    public void HeadRotationRight(InputAction.CallbackContext context)
    {
        if (context.started) // ボタンを押したとき
        {
            isRight = true;
        }
        else if (context.canceled) // ボタンを離したとき
        {
            isRight = false;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isInvincibility)
        {
            return;
        }
        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        if (layerName != playerIndex)
        {
            if (other.gameObject.tag == "Bullet")
            {
                myHp -= 5;
            }
            if (other.gameObject.tag == "StrongBullet")
            {
                myHp -= 30;
            }

        }
    }
    //ビームの多段ヒット
    public void OnTriggerStay(Collider other)
    {
        if (isInvincibility)
        {
            return;
        }
        string layerName = LayerMask.LayerToName(other.gameObject.layer);
        if (layerName != playerIndex)
        {
            if (other.gameObject.tag == "Beam")
            {
                myHp -= 2;
            }
        }
    }
}
