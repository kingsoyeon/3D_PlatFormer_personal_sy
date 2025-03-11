using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum LaszerZone
{
    Tunnel,
    PlatformLauncher
}

public class LaserTrap : MonoBehaviour
{
    // ��� �޼���
    public TextMeshProUGUI TrapText;

    // �÷��̾� ���� ����
    [SerializeField] private Vector3 boxCenter;
    [SerializeField] private Vector3 boxSize = new Vector3(2f, 2f, 2f);

    public LayerMask playerLayer;

    // ������ ���� ����
    public LaszerZone zone;

    private bool hasPopuped = false; // ������ �˾��� �Ǿ�������

    // ��ó ����
    

    private void Start()
    {
        TrapText.gameObject.SetActive(false);

        // ������ ���� �ؽ�Ʈ �˾� ��Ȱ��ȭ ����
        switch (zone)
        {
            case LaszerZone.Tunnel:
                break;
            case LaszerZone.PlatformLauncher:
                break;
        }
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
        switch (zone) 
        {
            case LaszerZone.Tunnel:
        TrapText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        TrapText.gameObject.SetActive(false);
                break;

            case LaszerZone.PlatformLauncher:
        TrapText.gameObject.SetActive(true);

                break;
        }
    }
}
