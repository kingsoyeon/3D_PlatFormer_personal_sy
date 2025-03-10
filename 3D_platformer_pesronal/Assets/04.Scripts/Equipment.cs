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
}
