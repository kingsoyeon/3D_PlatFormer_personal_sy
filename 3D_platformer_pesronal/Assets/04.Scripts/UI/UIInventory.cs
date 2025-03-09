using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Timeline.Actions.MenuPriority;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    // 인벤토리 구성요소
    public GameObject inventoryWindow; //인벤토리창
    public Transform slotPanel;
    public Transform dropPosition; // 드롭 위치

    // 슬롯 중에서 선택한 아이템
    [Header("Selected Item")]
    private ItemSlot selectedItem;
    private int selectedItemIndex;
    public TextMeshProUGUI selectedItemName;
    public TextMeshProUGUI selectedItemNDescription;
    public TextMeshProUGUI selectedItemStatName;
    public TextMeshProUGUI selectedItemStatValue;

    public GameObject useButton;
    public GameObject equipButton;
    public GameObject unEquipButton;
    public GameObject dropButton;

    //     private int curEquipIndex;

    //참조
    private PlayerController controller;
    private PlayerConditions conditions;


    void Start()
    {
        controller = CharacterManager.Instance.Player.controller;
        conditions = CharacterManager.Instance.Player.condition;
        dropPosition = CharacterManager.Instance.Player.dropPositon;

        controller.inventory += Toggle;
        //CharacterManager.Instance.Player.AddItem += AddItem;

        inventoryWindow.SetActive(false);

        // slots 밑에 딸린 itemslot.cs 달린 슬롯 개수 만큼 객체 생성
        slots = new ItemSlot[slotPanel.childCount];

        for (int i=0; i<slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>(); 
            slots[i].index = i;
            slots[i].uiInventory = this; 
            //slots.set();
        }

        ClearSelectedItemWindow();
    }

    void ClearSelectedItemWindow()
    {
        selectedItem = null;
        selectedItemName.text = string.Empty;
        selectedItemNDescription.text = string.Empty;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;
    }
    // 인벤토리 열고 닫기
    public void Toggle()
    {
        if (IsOpen()) 
        {
            inventoryWindow.SetActive(false);
        }
        else
        {
            inventoryWindow.SetActive(true);
        }
    }

    public bool IsOpen()
    {
        return inventoryWindow.activeInHierarchy;
    }
}
