using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipTool : Equip
{
    // ��� ���� //

    public float attackRate; // �����ֱ�
    private bool attacking; // ���ݿ���
    public float attackDistance; // ���� ��Ÿ�

    [Header("Resource Gathering")]
    public bool doesGatherResources; // �ڿ��� ������ ��������

    [Header("Combat")]
    public bool doesDealDamage; // �������� �ִ� ��������
    public int damage;
}
