using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConditions : MonoBehaviour
{
    public Condition health;
    
    public Condition stamina;

    // ���� �ʿ��� ���� ������Ʈ
    // public Condition Speed;
 
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }

}
