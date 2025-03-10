using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIConditions : MonoBehaviour
{
    public Condition health;
    
    public Condition stamina;

    // 추후 필요한 스탯 업데이트
    // public Condition Speed;
 
    void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;
    }

}
