using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//using static UnityEditor.Timeline.Actions.MenuPriority;

public class UIInventory : MonoBehaviour
{
    public ItemSlot[] slots;

    // �κ��丮 �������
    public GameObject inventoryWindow; //�κ��丮â
    public Transform slotPanel;
    public Transform dropPosition; // ��� ��ġ

    // ���� �߿��� ������ ������
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

    //����
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

        // slots �ؿ� ���� itemslot.cs �޸� ���� ���� ��ŭ ��ü ����
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

    // �κ��丮 ���� �ݱ�
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

    // �ߺ� ������ ���� Ȯ���ϰ� ��ȯ�ϴ� �޼���
    ItemSlot GetItemStack(ItemData data)
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // ������ �����۰� �������� �������� ���� / ������ ������ �ִ븦 ���� ������ 
            if (slots[i].ItemData == data && slots[i].quantity < data.maxStackAmount)
            {
                return slots[i];
            }
        }
        return null;
    }

    // �� ���� ã�Ƽ� ��ȯ �޼���
    ItemSlot GetEmptySlot()
    {
        for (int i = 0; i < slots.Length; i++)
        {
            // ������ �ȵ������ �� �� ���� ��ȯ
            if (slots[i].ItemData == null)
            { return slots[i]; }
        }
        return null;
    }


    // ������ �κ��丮�� �߰�
    public void AddItem()
    {
        ItemData itemData = CharacterManager.Instance.Player.ItemData;

        // �ߺ� ������ �������� �� ���ǹ��� ź��
        if (itemData.canStack)
        {
            ItemSlot slot = GetItemStack(itemData);

            // ������ ������ �� ���Կ� �־��ش�
            if (slot != null)
            {
                slot.quantity++;
                UpdateUI();
                CharacterManager.Instance.Player.ItemData = null; // �����ϱ� data�� ���� null
                return;
            }
        }

        // �ߺ� �������� ���� �����۵��� �� ������ ã�´�

        ItemSlot emptySlot = GetEmptySlot();

        // �� ������ ������ �ű⿡ �־��ش�
        if (emptySlot != null)
        {
            emptySlot.ItemData = itemData;
            emptySlot.quantity = 1;
            UpdateUI();
            CharacterManager.Instance.Player.ItemData = null;
            return;
        }

        // �ߺ��� �ȵǰ� �� ���Ե� ������ �׳� ������
        ThrowItem(itemData);
        CharacterManager.Instance.Player.ItemData = null;
    }

    public void ThrowItem(ItemData itemData)
    {
        Instantiate(itemData.dropPrefab, dropPosition.position, Quaternion.Euler(Vector3.one * Random.value * 360));
    }


    /// ui ����

    // ������ �������� ����â�� ������Ʈ ���ִ� �Լ�
    public void SelectItem(int index)
    {
        if (slots[index].ItemData == null) return;

        selectedItem = slots[index];
        selectedItemIndex = index;

        selectedItemName.text = selectedItem.ItemData.displayName;
        selectedItemNDescription.text = selectedItem.ItemData.description;
        selectedItemStatName.text = string.Empty;
        selectedItemStatValue.text = string.Empty;

        // �Һ� �������� ���� StatName, StatValue ��Ÿ����
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

    // ��ư ���� �Ŀ� �����ߴ� ������ �����ִ� �Լ� 
    void RemoveSelctedItem()
    {
        selectedItem.quantity--;

        // 1���� ������
        if (selectedItem.quantity <= 0)
        {
            //������ �ϰ� �ִٸ� ���´�
            if (slots[selectedItemIndex].equipped)
            {
                //
            }

            // ������ �ƴϸ�
            selectedItem.ItemData = null;
            ClearSelectedItemWindow();
        }
        UpdateUI();
    }

    // #1����ϱ� ��ư

    public void OnUeseButton()
    {
        // �Һ� Ÿ���� ���
        if (selectedItem.ItemData.type == ItemType.Consumable)
        {
            for (int i = 0; i < selectedItem.ItemData.ItemsConsumables.Length; i++)
            {
                switch (selectedItem.ItemData.ItemsConsumables[i].type)
                {
                    // �Һ� Ÿ�Կ� ���� �Լ� ȣ��
                    case ConsumableType.Health:
                        conditions.Eat(selectedItem.ItemData.ItemsConsumables[i].value); break;
                }
            }
            RemoveSelctedItem(); // ������ ������ �ʱ�ȭ
        }
    }
    
    // # 2 �����ϱ� ��ư

    // # 3 �����ϱ� ��ư

    // #4  ������ ��ư

    public void OnDropButton()
    {
        ThrowItem(selectedItem.ItemData);
        RemoveSelctedItem();
    }


}
