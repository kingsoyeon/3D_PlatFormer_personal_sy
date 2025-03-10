using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICoins : MonoBehaviour
{
    public TextMeshProUGUI coinText; 
    
    public int quantity;


    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void GetCoin()
    {
        quantity++;
        coinText.text = $"X {quantity}";
    }
}
