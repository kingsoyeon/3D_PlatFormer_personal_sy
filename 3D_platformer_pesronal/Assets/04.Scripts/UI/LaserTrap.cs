using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LaserTrap : MonoBehaviour
{
    // ��� �޼���
    public TextMeshProUGUI TrapText;

    // �÷��̾� ���� ����
    [SerializeField] private Vector3 boxCenter;
    [SerializeField] private Vector3 boxSize = new Vector3(2f, 2f, 2f);

    public LayerMask playerLayer;

    private bool hasPopuped = false; // ������ �˾��� �Ǿ�������

    private void Start()
    {
        TrapText.gameObject.SetActive(false);
    }
    void Update()
    {
        DetectPlayer();
    }

    void DetectPlayer()
    {
        // �ڽ��� ��ǥ�� ������ǥ�� ��ȯ
        Vector3 boxWorldCenter = transform.position + boxCenter;

        // �÷��̾� ���̾��ũ ���� �ʼ�
        Collider[] hits = Physics.OverlapBox(boxWorldCenter, boxSize / 2, Quaternion.identity, playerLayer);

        // �÷��̾ ������ �Ǹ�
        if (hits.Length > 0 && !hasPopuped)
        {
            
            hasPopuped = true;
            StartCoroutine(PopupText());
        }  
    }

    private IEnumerator PopupText()
    {
        TrapText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        TrapText.gameObject.SetActive(false);
    }
}
