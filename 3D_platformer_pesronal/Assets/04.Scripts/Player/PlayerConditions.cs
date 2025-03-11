using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions : MonoBehaviour, IDamagble
{
    public UIConditions uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    Condition speed { get { return uiCondition.speed; } }


    public Action OnTakeDamge;

    void Update()
    {
        // 시간에 따라 스태미나 자동 증가
        stamina.Add(Time.deltaTime * stamina.passiveValue);

        if (health.curValue <= 0f)
        {
            Die();
        }
    }

    // 사망
    public void Die()
    {
        // 로직 추가 예정
    }


    // 섭취 시 체력 증가
    public void Eat(float amount)
    {
        health.Add(amount);
    }

    // 섭취 시 스태미나 증가
    public void EatStamina(float amount)
    {
        stamina.Add(amount);
    }
    public void EatSpeed()
    {
        CharacterManager.Instance.Player.controller.moveSpeed++;
    }

    // 데미지

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Substract(damageAmount * Time.deltaTime);
        OnTakeDamge?.Invoke();
    }

    // 스태미나 감소
    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0f) { return false; }

        stamina.Substract(amount);
        return true;
    }
}
