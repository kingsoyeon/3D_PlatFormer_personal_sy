using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherObject : MonoBehaviour
{

    private bool canLaunch = false; // �߻簡�ɻ������� Ȯ��
    public bool CanLaunch => canLaunch;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // �÷��� ���� �ö󼭸� �߻� �����ϴ�
    private void OnCollisionEnter(Collision collision)
    {
        canLaunch = true;

        //Rigidbody rigidbody = collision.transform.GetComponent<Rigidbody>();
        //rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }
    private void OnCollisionExit(Collision collision)
    {
        canLaunch = false;
    }
}
