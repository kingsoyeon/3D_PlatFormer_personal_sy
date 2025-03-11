using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingFlatform : MonoBehaviour
{
    private float jumpForce = 200f;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        // มกวม

        Rigidbody rigidbody = collision.transform.GetComponent<Rigidbody>();
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
   
}
