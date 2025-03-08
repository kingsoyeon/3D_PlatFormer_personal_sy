using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConditions : MonoBehaviour, IDamagble
{
    public UIConditions uiCondition;

    Condition health { get { return uiCondition.health; } }
    Condition stamina { get { return uiCondition.stamina; } }

    // ���� �߰��� ����
    // Condition speed { get { return uiCondition.speed; } }

    public Action OnTakeDamge;

    void Update()
    {
        // �ð��� ���� ���¹̳� �ڵ� ����
        stamina.Add(Time.deltaTime * stamina.passiveValue);

        if (health.curValue <= 0f)
        {
            Die();
        }
    }

    // ���
    public void Die()
    {
        // ���� �߰� ����
    }


    // ���� �� ü�� ����
    public void Eat(float amount)
    {
        health.Add(amount);
    }

    public void TakePhysicalDamage(int damageAmount)
    {
        health.Substract(damageAmount * Time.deltaTime);
        OnTakeDamge?.Invoke();
    }
}
