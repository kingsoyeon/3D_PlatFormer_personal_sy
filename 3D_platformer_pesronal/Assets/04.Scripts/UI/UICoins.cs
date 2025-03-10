using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UICoins : MonoBehaviour
{
    public TextMeshProUGUI coinText; 
    
    public int quantity;

    public AnimationHandler animationHandler;

    void Start()
    {
        animationHandler =  GetComponentInChildren<AnimationHandler>();
    }

    
    void Update()
    {
        
    }

    public void GetCoin()
    {
        quantity++;
        coinText.text = $"X {quantity}";
        animationHandler.GetCoin();

    }
}
