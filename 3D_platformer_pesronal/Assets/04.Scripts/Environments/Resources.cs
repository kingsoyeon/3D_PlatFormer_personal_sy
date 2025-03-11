using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public ItemData itemToGive; // ����� ������
    public int quantityPerHit = 1; // �� ���� ����� ������ ����
    public int capacity; // ĥ �� �ִ� Ƚ��

    public void Gather(Vector3 hitPoint, Vector3 hitNormal)
    {
        for (int i = 0; i < quantityPerHit; i++)
        {
            if (capacity <= 0) break;

            capacity -= 1;
            
        }
        if (capacity <= 0) 
        { Destroy(gameObject); 
         Instantiate(itemToGive.dropPrefab, hitPoint + Vector3.up, Quaternion.LookRotation(hitNormal, Vector3.up)); 
        }
    }
}
