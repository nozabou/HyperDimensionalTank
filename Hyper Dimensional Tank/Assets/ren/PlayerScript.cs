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
    private float moveSpeed = 5f;
    private Vector3 moveVec;
    private Vector2 inputMove;

    private bool isLeft = false;
    private bool isRight = false;

    private float bodyRotateSpeed = 50f;
    private float headRotateSpeed = 50f;
    //Vector3 moveSpeed = new Vector3(0, 0, 1f);
    //float moveSpeed = 5f;
    Transform head;

    //弾の動き
    //弾の発射場所
    public GameObject shotPoint;

    //弾
    public GameObject bulletNomal;
    public GameObject bulletStrong;
   

    //弾の速さ
    private float nomalBulletSpeed = 600f;
    private float strongBulletSpeed = 400f;

    //クールタイム
    private int nomalCountTime = 0;
    private int canNomalCoolTime = 60;
    private int strongCountTime = 0;
    private int canStrongCoolTime = 90;

    private bool isShotNomal = true;
    private bool isShotStrong = true;
   

    //体力
    public int myHp = 100;
    public bool isDead = false;
    public int playerStock = 2;

  
    //ビーム(必殺技)
    public GameObject bulletBeam;
    private bool isShotBeam = false;
    //ビームの全体フレーム
    private int beamFream = 90;
    private int beamFreamCount = 0;
    private bool isBeamCount = false;
    //ゲージ
    public int beamGauge = 0;

    //InputSystem
    private PlayerControl playerControl;

    // Start is called before the first frame update
    void Start()
    {
        moveVec = new Vector3(0,0,moveSpeed);
        head = transform.GetChild(0);
        playerControl = new PlayerControl();
        playerControl.Enable();
    }

    // Update is called once per frame
    void Update()
    {
    
        if (isDead)
        {
            return;
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

        if (beamGauge > 100)
        {
            beamGauge = 100;
            isShotBeam = true;
        }
        else
        {
            beamGauge++;
        }
        ////////////////////////////////////////////////////
        nomalCountTime++;
        if (nomalCountTime > canNomalCoolTime)
        {
            isShotNomal = true;
        }

        strongCountTime++;
        if (strongCountTime > canStrongCoolTime)
        {
            isShotStrong = true;
        }

        this.transform.Translate(inputMove.y * moveVec * Time.deltaTime);
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

       
    }
 
    public void OnShotNomal(InputAction.CallbackContext context)
    {
        if (isShotNomal)
        {
            //弾の発射する場所を取得する
            Vector3 bulletPosition = shotPoint.transform.position;
            //
            GameObject newBullet = Instantiate(bulletNomal, bulletPosition, head.gameObject.transform.rotation);
            Vector3 dir = newBullet.transform.forward;
            newBullet.GetComponent<Rigidbody>().AddForce(dir * nomalBulletSpeed * Time.deltaTime, ForceMode.Impulse);
            nomalCountTime = 0;
            isShotNomal = false;
        }
    }
    public void OnShotStrong(InputAction.CallbackContext context)
    {
        if (isShotStrong)
        {
            //弾の発射する場所を取得する
            Vector3 bulletPosition = shotPoint.transform.position;
            //
            GameObject newBullet = Instantiate(bulletStrong, bulletPosition, head.gameObject.transform.rotation);
            Vector3 dir = newBullet.transform.forward;
            newBullet.GetComponent<Rigidbody>().AddForce(dir * strongBulletSpeed * Time.deltaTime, ForceMode.Impulse);
            strongCountTime = 0;
            isShotStrong = false;
        }
    }

    public void OnShotBeam(InputAction.CallbackContext context)
    {
        if (context.started && isShotBeam) // ボタンを押したとき
        {
            //弾の発射する場所を取得する
            Vector3 bulletPosition = shotPoint.transform.position;
            //
            GameObject newBullet = Instantiate(bulletBeam, bulletPosition, head.gameObject.transform.rotation);
            // Vector3 dir = newBullet.transform.forward;
            beamGauge = 0;
            isBeamCount = true;
            isShotBeam = false;
            Destroy(newBullet, 1); //10秒後に弾を消す
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


    //public void OnTriggerEnter(Collider other)
    //{
    //    string layerName = LayerMask.LayerToName(other.gameObject.layer);
    //    if (layerName != playerIndex)
    //    {
    //        if (other.gameObject.tag == "Bullet")
    //        {
    //            myHp -= 5;
    //        }
    //        if (other.gameObject.tag == "StrongBullet")
    //        {
    //            myHp -= 30;
    //        }
           
    //    }

    //    if (myHp <= 0)
    //    {
    //        isDead = true;
    //        playerStock--;
    //    }
    //}
    //ビームの多段ヒット
    public void OnTriggerStay(Collider other)
    {
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
            if (other.gameObject.tag == "Beam")
            {
                myHp -= 2;
            }
        }

        if (myHp <= 0)
        {
            isDead = true;
            playerStock--;
        }
    
        if (myHp <= 0)
        {
            isDead = true;
            playerStock--;
        }
       
    }
}
