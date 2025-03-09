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
    public TextMeshProUGUI quntityText;
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

    
    void Update()
    {
        
    }
}
