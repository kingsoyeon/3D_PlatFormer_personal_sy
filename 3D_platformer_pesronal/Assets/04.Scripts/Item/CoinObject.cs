using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : MonoBehaviour
{
    public UICoins uiCoins;
    
    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("ÄÚÀÎ È¹µæ");
        this.gameObject.SetActive(false);

        // UI¿¡ ¿¬°á
        uiCoins.GetCoin();
    }
}
