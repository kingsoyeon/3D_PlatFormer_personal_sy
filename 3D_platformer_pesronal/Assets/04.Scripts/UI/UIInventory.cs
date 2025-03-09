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

        for (int i = 0; i < slots.Length; i++)
        {
            slots[i] = slotPanel.GetChild(i).GetComponent<ItemSlot>();
            slots[i].index = i;
            slots[i].uiInventory = this;
            slots[i].Clear();
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

    public void UpdateUI()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].ItemData != null)
            {
                slots[i].Set();
            }
            else
            {
                slots[i].Clear();
            }
        }
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

    // 중복 가능한 슬롯 확인하고 반환하는 메서드
    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // 슬롯의 아이템과 넣으려는 아이템이 같고 / 아이템 개수가 최대를 넘지 않을때 
            if (slots[i].ItemData == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    // 빈 슬롯 찾아서 반환 메서드
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // 아이템 안들어있을 때 그 슬롯 반환
            if (slots[i].ItemData == null)
            { return slots[i]; }
        }
        return null;
    }


    // 아이템 인벤토리에 추가
    public void AddItem()
    {
        ItemData itemData = CharacterManager.Instance.Player.ItemData;

        // 중복 가능한 아이템은 이 조건문을 탄다
        if (itemData.canStack)
        {
            ItemSlot slot = GetItemStack(itemData);

            // 슬롯이 있으면 그 슬롯에 넣어준다
            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.ItemData = null; // 들어갔으니까 data는 이제 null
                return;
            }
        }

        // 중복 가능하지 않은 아이템들은 빈 슬롯을 찾는다

        ItemSlot emptySlot = GetEmptySlot();

        // 빈 슬롯이 있으면 거기에 넣어준다
        if (emptySlot != null)
        {
            emptySlot.ItemData = itemData;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.ItemData = null;
            return;
        }

        // 중복도 안되고 빈 슬롯도 없으면 그냥 버린다
        ThrowItem(itemData);
        CharacterManager.Instance.Player.ItemData = null;
    }

    public void ThrowItem(ItemData itemData)
    {
        Instantiate(itemData.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }


    /// ui 관련

    // 선택한 아이템을 정보창에 업데이트 해주는 함수
    public void SelectItem(int index)
    {
        if (slots[index].ItemData == null) return;

        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.ItemData.displayName;
        selectedItemNDescription.text = selectedItem.ItemData.description;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        // 소비 아이템일 때만 StatName, StatValue 나타내기
        for (int i = 0; i < selectedItem.ItemData.ItemsConsumables.Length; i++)
        {
            selectedItemStatName.text += selectedItem.ItemData.ItemsConsumables[i].type.ToString();
            selectedItemStatValue.text += selectedItem.ItemData.ItemsConsumables[i].value.ToString();
        }

        useButton.SetActive(selectedItem.ItemData.type == ItemType.Consumable);
        equipButton.SetActive(selectedItem.ItemData.type == ItemType.Equipable && !slots[index].equipped);
        unEquipButton.SetActive(selectedItem.ItemData.type == ItemType.Equipable && slots[index].equipped);

        dropButton.SetActive(true);
    }

    // 버튼 선택 후에 선택했던 아이템 지워주는 함수 
    void RemoveSelctedItem()
    {
        selectedItem.quantity--;

        // 1개도 없으면
        if (selectedItem.quantity <= 0)
        {
            //착용을 하고 있다면 벗는다
            if (slots[selectedItemIndex].equipped)
            {
                //
            }

            // 착용템 아니면
            selectedItem.ItemData = null;
            ClearSelectedItemWindow();
        }
        UpdateUI();
    }

    // #1사용하기 버튼

    public void OnUeseButton()
    {
        // 소비 타입일 경우
        if (selectedItem.ItemData.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.ItemData.ItemsConsumables.Length; i++)
            {
                switch (selectedItem.ItemData.ItemsConsumables[i].type)
                {
                    // 소비 타입에 따른 함수 호출
                    case ConsumableType.Health:
                        conditions.Eat(selectedItem.ItemData.ItemsConsumables[i].value); break;
                }
            }
            RemoveSelctedItem(); // 선택한 아이템 초기화
        }
    }
    
    // # 2 착용하기 버튼

    // # 3 해제하기 버튼

    // #4  버리기 버튼

    public void OnDropButton()
    {
        ThrowItem(selectedItem.ItemData);
        RemoveSelctedItem();
    }


}
