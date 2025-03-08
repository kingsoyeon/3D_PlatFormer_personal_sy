using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Condition : MonoBehaviour
{
    public float curValue;
    public float maxValue;
    public float startValue;
    public float passiveValue;
    public Image uiBar;

    private void Start()
    {
        curValue = startValue;
    }

    void Update()
    {
        // fillAmount는 이미지 타입이 filled여야 함.
        uiBar.fillAmount = GetPercentage();
    }
    // ui바 게이지
    public float GetPercentage()
    {
        return curValue / maxValue;
    }

    // 게이지 증가
    public void Add(float amount)
    {
        curValue = Mathf.Min(curValue + amount, maxValue);
    }

    // 게이지 감소
    public void Substract(float amount)
    {
        curValue = Mathf.Max(curValue - amount, 0);
    }
}
