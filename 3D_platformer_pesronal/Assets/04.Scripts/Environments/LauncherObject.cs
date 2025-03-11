using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherObject : MonoBehaviour
{

    private bool canLaunch = false; // 발사가능상태인지 확인
    

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // 플랫폼 위에 올라서면 발사 가능하다
    private void OnCollisionEnter(Collision collision)
    {
        canLaunch = true;

    }
    private void OnCollisionExit(Collision collision)
    {
        canLaunch = false;
    }
}
