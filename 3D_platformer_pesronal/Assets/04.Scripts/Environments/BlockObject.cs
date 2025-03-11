using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockObject : MonoBehaviour
{
    private Resources resources;

    public LayerMask playerLayerMask;

    void Start()
    {
        resources = GetComponent<Resources>();
    }

    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player")) // 플레이어와 충돌했을 때
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 hitPoint = contact.point; // 충돌한 위치
            Vector3 hitNormal = contact.normal; // 충돌한 표면의 방향 (법선 벡터)
            resources.Gather(hitPoint, hitNormal);
        }
    }
}
