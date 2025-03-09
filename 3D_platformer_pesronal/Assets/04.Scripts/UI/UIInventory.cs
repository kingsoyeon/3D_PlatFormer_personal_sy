using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    // �κ��丮 �������
    public GameObject inventoryWindow; //�κ��丮â
    public Transform slotPanel;
    public Transform dropPosition; // ��� ��ġ

    // ������ ������
    [Header("Selected Item")]
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemNDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;

    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    //     private int curEquipIndex;

    //����
    private PlayerController controller;
    private PlayerConditions conditions;


    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        conditions = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPositon;
    }

    
    void Update()
    {
        
    }
}
