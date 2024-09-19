using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //プレイヤーの動き
    private float bodyRotateSpeed = 50f;
    private float headRotateSpeed = 50f;
    Vector3 moveSpeed = new Vector3(0, 0, 1f);
    Transform head;

    //弾の動き
    //弾の発射場所
    public GameObject shotPoint;

    //弾
    public GameObject bullet;

    //弾の速さ
    private float bulletSpeed = 100f;

    //クールタイム
    private float shotCoolTime = 0f;
    private bool isShot = true;
    // Start is called before the first frame update
    void Start()
    {
        head = transform.GetChild(0);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerMove();

       

        if(isShot)
        {
            shotCoolTime = 0.0f;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //弾の発射する場所を取得する
                Vector3 bulletPosition = shotPoint.transform.position;
                //
                GameObject newBullet = Instantiate(bullet, bulletPosition, head.gameObject.transform.rotation);
                Vector3 dir = newBullet.transform.forward;
                newBullet.GetComponent<Rigidbody>().AddForce(dir * bulletSpeed * Time.deltaTime, ForceMode.Impulse);
                isShot = false;
            }
        }
        else
        {
            if(shotCoolTime > 60.0f)
            {
                isShot = true;
            }
            shotCoolTime++;
        }


    }

    private void PlayerMove()
    {
        if (Input.GetKey(KeyCode.W))
        {
            // Quaternion.AngleAxis(度数法, 軸);
            this.transform.Translate(moveSpeed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            // Quaternion.AngleAxis(度数法, 軸);
            this.transform.Translate(-moveSpeed * Time.deltaTime);
        }


        if (Input.GetKey(KeyCode.D))
        {
            // Quaternion.AngleAxis(度数法, 軸);
            this.transform.rotation *= Quaternion.AngleAxis(bodyRotateSpeed * Time.deltaTime, Vector3.up);
        }
        if (Input.GetKey(KeyCode.A))
        {
            // Quaternion.AngleAxis(度数法, 軸);
            this.transform.rotation *= Quaternion.AngleAxis(-bodyRotateSpeed * Time.deltaTime, Vector3.up);
        }


        if (Input.GetKey(KeyCode.RightArrow))
        {
            // Quaternion.AngleAxis(度数法, 軸);
            head.rotation *= Quaternion.AngleAxis(headRotateSpeed * Time.deltaTime, Vector3.up);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            // Quaternion.AngleAxis(度数法, 軸);
            head.rotation *= Quaternion.AngleAxis(-headRotateSpeed * Time.deltaTime, Vector3.up);
        }


    }
}
