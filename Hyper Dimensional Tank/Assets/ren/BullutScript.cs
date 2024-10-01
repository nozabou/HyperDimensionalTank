using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullutScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private GameObject explosion;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
