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

public class PlayerScript : MonoBehaviour
{
    //プレイヤーの動き
    private float moveSpeed = 5f;
    private Vector3 moveVec;
   
    private Rigidbody rb;
    //float inputVertical;
    //float inputHorizontal;

    private float bodyRotateSpeed = 50f;
    private float headRotateSpeed = 50f;
    //Vector3 moveSpeed = new Vector3(0, 0, 1f);
    //float moveSpeed = 5f;
    Transform head;

    //弾の動き
    //弾の発射場所
    public GameObject shotPoint;

    //弾
    public GameObject bullet;

    //弾の速さ
    private float bulletSpeed = 200f;

    //クールタイム
    private int countCoolTime = 0;
    private int shotCoolTime = 60;
    private bool isShot = true;

    //体力
    public int myHp = 100;
    public bool isDead = false;
    public int playerStock = 2;



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
        PlayerMove();

        if (isShot)
        {
            countCoolTime = 0;
            if (playerControl.Player.Fire.triggered)//押された瞬間
            {
                //弾の発射する場所を取得する
                Vector3 bulletPosition = shotPoint.transform.position;
                //
                GameObject newBullet = Instantiate(bullet, bulletPosition, head.gameObject.transform.rotation);
                Vector3 dir = newBullet.transform.forward;
                newBullet.GetComponent<Rigidbody>().AddForce(dir * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
                isShot = false;
            }
            //if (Input.GetKeyDown(KeyCode.Space))
            //{
            //    //弾の発射する場所を取得する
            //    Vector3 bulletPosition = shotPoint.transform.position;
            //    //
            //    GameObject newBullet = Instantiate(bullet, bulletPosition, head.gameObject.transform.rotation);
            //    Vector3 dir = newBullet.transform.forward;
            //    newBullet.GetComponent<Rigidbody>().AddForce(dir * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
            //    isShot = false;
            //}
        }
        else
        {
            if(countCoolTime > shotCoolTime)
            {
                isShot = true;
            }
            countCoolTime++;
        }

       

    }
    //private void OnEnable()
    //{
    //    // 全体のActionを有効化
    //    playerControl.Enable();
    //}

    //private void OnDisable()
    //{
    //    // 全体のActionを無効化
    //    playerControl.Disable();
    //}
    public void OnFire(InputAction.CallbackContext context)
    {
        print("Fire Actionが呼ばれた！");
    }
    public void OnForward(InputAction.CallbackContext context)
    {
        this.transform.Translate(moveVec * Time.deltaTime);
    }
    public void OnBackward(InputAction.CallbackContext context)
    {
        this.transform.Translate(-moveVec * Time.deltaTime);
    }
    public void OnBodyRotationRight(InputAction.CallbackContext context)
    {
        // Quaternion.AngleAxis(度数法, 軸);
        this.transform.rotation *= Quaternion.AngleAxis(bodyRotateSpeed * Time.deltaTime, Vector3.up);
    }
    public void OnBodyRotationLeft(InputAction.CallbackContext context)
    {
        // Quaternion.AngleAxis(度数法, 軸);
        this.transform.rotation *= Quaternion.AngleAxis(-bodyRotateSpeed * Time.deltaTime, Vector3.up);
    }

    private void PlayerMove()
    {

        //前進と後退
        //if (playerControl.Player.Forward.IsPressed())//押されている間
        //{
        //    this.transform.Translate(moveVec * Time.deltaTime);
        //}

        //if (playerControl.Player.Backward.IsPressed())//押されている間
        //{
        //    this.transform.Translate(-moveVec * Time.deltaTime);
        //}
        //回転
        //if (playerControl.Player.BodyRotationRight.IsPressed())//押されている間
        //{
        //    // Quaternion.AngleAxis(度数法, 軸);
        //    this.transform.rotation *= Quaternion.AngleAxis(bodyRotateSpeed * Time.deltaTime, Vector3.up);
        //}
        //if (playerControl.Player.BodyRotationLeft.IsPressed())//押されている間
        //{
        //    // Quaternion.AngleAxis(度数法, 軸);
        //    this.transform.rotation *= Quaternion.AngleAxis(-bodyRotateSpeed * Time.deltaTime, Vector3.up);
        //}

        //大砲の向きの回転
        //ActionMaps/Action/入力のふるまい
        if (playerControl.Player.HeadRotationLeft.IsPressed())//押されている間
        {
            //Quaternion.AngleAxis(度数法, 軸);
            head.rotation *= Quaternion.AngleAxis(-headRotateSpeed * Time.deltaTime, Vector3.up);
        }
        if (playerControl.Player.HeadRotationRight.IsPressed())//押されている間
        {
            //Quaternion.AngleAxis(度数法, 軸);
            head.rotation *= Quaternion.AngleAxis(headRotateSpeed * Time.deltaTime, Vector3.up);
        }



        //inputVertical = Input.GetAxis("Vertical");
        //inputHorizontal = Input.GetAxis("Horizontal");

        //this.transform.Translate(0,0, inputVertical * Time.deltaTime);
        //this.transform.rotation *= Quaternion.AngleAxis(inputHorizontal * bodyRotateSpeed * Time.deltaTime, Vector3.up);

        //if (Input.GetKeyDown(KeyCode.Joystick2Button14))
        //{
        //    head.rotation *= Quaternion.AngleAxis(headRotateSpeed * Time.deltaTime, Vector3.up);
        //}





        //if (Input.GetKey(KeyCode.W))
        //{
        //    // Quaternion.AngleAxis(度数法, 軸);
        //    this.transform.Translate(moveSpeed * Time.deltaTime);
        //}
        //if (Input.GetKey(KeyCode.S))
        //{
        //    // Quaternion.AngleAxis(度数法, 軸);
        //    this.transform.Translate(-moveSpeed * Time.deltaTime);
        //}


        //if (Input.GetKey(KeyCode.D))
        //{
        //    // Quaternion.AngleAxis(度数法, 軸);
        //    this.transform.rotation *= Quaternion.AngleAxis(bodyRotateSpeed * Time.deltaTime, Vector3.up);
        //}
        //if (Input.GetKey(KeyCode.A))
        //{
        //    // Quaternion.AngleAxis(度数法, 軸);
        //    this.transform.rotation *= Quaternion.AngleAxis(-bodyRotateSpeed * Time.deltaTime, Vector3.up);
        //}


        //if (Input.GetKey(KeyCode.RightArrow))
        //{
        //    // Quaternion.AngleAxis(度数法, 軸);
        //    head.rotation *= Quaternion.AngleAxis(headRotateSpeed * Time.deltaTime, Vector3.up);
        //}
        //if (Input.GetKey(KeyCode.LeftArrow))
        //{
        //    // Quaternion.AngleAxis(度数法, 軸);
        //    head.rotation *= Quaternion.AngleAxis(-headRotateSpeed * Time.deltaTime, Vector3.up);
        //}


    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Bullet")
        {
            myHp -= 30;
            Debug.Log(myHp);
        }

        if(myHp <= 0)
        {
            isDead = true;
            playerStock--;
        }
    }
}
