using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    private float rotateSpeed;
    private float rotateAngle;
    // Start is called before the first frame update
    void Start()
    {
        rotateSpeed = 0f;
        rotateAngle = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.Space))
        {
            // Quaternion.AngleAxis(ìxêîñ@, é≤);
            rotateAngle++;
           this.transform.rotation = Quaternion.AngleAxis(rotateAngle, Vector3.up);
        }
    }
}
