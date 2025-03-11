using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : MonoBehaviour
{
    public UICoins uiCoins;

    //public AnimationHandler animationHandler;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        Debug.Log("코인 획득");

        // 2분 뒤 재출현
        StartCoroutine(ActiveAfterDelay(120f));

        // UI에 연결
        uiCoins.GetCoin();

        
        this.gameObject.SetActive(false);

    }

    private IEnumerator ActiveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(true);
    }
}
