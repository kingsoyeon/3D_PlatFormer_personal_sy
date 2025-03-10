using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public Equip curEquip;
    private PlayerConditions playerConditions;
    private PlayerController playerController;

    void Start()
    {
        playerConditions = GetComponent<PlayerConditions>();
        playerController = GetComponent<PlayerController>();
    }
    void Update()
    {
        
    }

    public void Equip()
    {
        //if(curEquip == null) { curEquip =  }
    }

    public void UnEquip()
    {
        // 착용하고 있는 것이 있으면
        //if (curEquip != null)
        //{
        //    해제한다

        //}
    }

}
