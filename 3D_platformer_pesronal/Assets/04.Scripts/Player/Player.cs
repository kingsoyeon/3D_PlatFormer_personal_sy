using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IDamagble
{
    // µ¥¹ÌÁö
    void TakePhysicalDamage(int damageAmount);
}
public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerConditions condition;

    public ItemData ItemData;
    public Action AddItem;

    public Transform dropPositon;

    private void Awake()
    {
        CharacterManager.Instance.Player = this;

        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerConditions>();
    }

}
