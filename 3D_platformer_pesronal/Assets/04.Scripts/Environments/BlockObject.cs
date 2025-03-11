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
        if (collision.gameObject.CompareTag("Player")) // �÷��̾�� �浹���� ��
        {
            ContactPoint contact = collision.contacts[0];
            Vector3 hitPoint = contact.point; // �浹�� ��ġ
            Vector3 hitNormal = contact.normal; // �浹�� ǥ���� ���� (���� ����)
            resources.Gather(hitPoint, hitNormal);
        }
    }
}
