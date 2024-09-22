using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Transactions;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class PlayerScript : MonoBehaviour
{
    //プレイヤーの動き
    float inputVertical;
    float inputHorizontal;

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
    private float bulletSpeed = 100f;

    //クールタイム
    private int countCoolTime = 0;
    private int shotCoolTime = 60;
    private bool isShot = true;

    //体力
    public int myHp = 100;
    public bool isDead = false;
    public int playerStock = 2;


    // Start is called before the first frame update
    void Start()
    {
        head = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (isDead)
        {
            return;
        }
        
        PlayerMove();

       

        if(isShot)
        {
            countCoolTime = 0;
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

    private void PlayerMove()
    {
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
