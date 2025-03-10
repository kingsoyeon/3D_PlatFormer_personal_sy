using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData ItemData;

    public UIInventory uiInventory;

    // �������
    public Button button;
    public Image icon;
    public TextMeshProUGUI quantityText;
    private Outline outline;


    // �ʿ��Ѻ���
    public int index; // ĭ ��ȣ
    public bool equipped; // �����ߴ�
    public int quantity; // ���� �ȿ� ������ ����

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    private void OnEnable()
    {
        outline.enabled = equipped; // equipped�� true�� �� �ƿ����� Ȱ��ȭ
    }


    public void Set()
    {
        icon.gameObject.SetActive(true);
        icon.sprite = ItemData.icon;
        quantityText.text = quantity > 1 ? quantity.ToString() : string.Empty;

        // ����ڵ�
        if(outline != null ) {outline.enabled = equipped;}
    }

    public void Clear()
    {
        ItemData = null;
        icon.gameObject.SetActive(false);
        quantityText.text = string.Empty;
    }

    public void OnClickButton()
    {
        uiInventory.SelectItem(index);
    }

}
