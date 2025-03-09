using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    public ItemData ItemData;

    public UIInventory uiInventory;

    // 구성요소
    public Button button;
    public Image icon;
    public TextMeshProUGUI quntityText;
    private Outline outline;


    // 필요한변수
    public int index; // 칸 번호
    public bool equipped; // 착용했다
    public int quantity; // 슬롯 안에 아이템 개수

    private void Awake()
    {
        outline = GetComponent<Outline>();
    }
    private void OnEnable()
    {
        outline.enabled = equipped; // equipped가 true일 때 아웃라인 활성화
    }

    
    void Update()
    {
        
    }
}
