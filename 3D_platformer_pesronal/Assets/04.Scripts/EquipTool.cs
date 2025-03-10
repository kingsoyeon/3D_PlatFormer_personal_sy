using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
    // 장비 정보 //

    public float attackRate; // 공격주기
    private bool attacking; // 공격여부
    public float attackDistance; // 공격 사거리

    [Header("Resource Gathering")]
    public bool doesGatherResources; // 자원을 모으는 도구인지

    [Header("Combat")]
    public bool doesDealDamage; // 데미지를 주는 무기인지
    public int damage;
}
