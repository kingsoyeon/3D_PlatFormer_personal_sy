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
        Debug.Log("���� ȹ��");

        // 2�� �� ������
        StartCoroutine(ActiveAfterDelay(120f));

        this.gameObject.SetActive(false);

        // UI�� ����
        uiCoins.GetCoin();
    }

    private IEnumerator ActiveAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        this.gameObject.SetActive(true);
    }
}
