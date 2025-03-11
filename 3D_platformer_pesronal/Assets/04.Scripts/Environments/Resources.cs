using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour
{
    public ItemData itemToGive; // 드랍할 아이템
    public int quantityPerHit = 1; // 한 번당 드랍할 아이템 갯수
    public int capacity; // 칠 수 있는 횟수

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
