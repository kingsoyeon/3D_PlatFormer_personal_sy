using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherObject : MonoBehaviour
{

    private bool canLaunch = false; // �߻簡�ɻ������� Ȯ��
    

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

    }
    private void OnCollisionExit(Collision collision)
    {
        canLaunch = false;
    }
}
