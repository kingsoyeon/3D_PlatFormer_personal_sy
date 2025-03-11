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
    // 띄울 메세지
    public TextMeshProUGUI TrapText;

    // 플레이어 감시 구간
    [SerializeField] private Vector3 boxCenter;
    [SerializeField] private Vector3 boxSize = new Vector3(2f, 2f, 2f);

    public LayerMask playerLayer;

    // 레이저 구역 설정
    public LaszerZone zone;

    private bool hasPopuped = false; // 이전에 팝업이 되었었는지

    // 런처 참조
    

    private void Start()
    {
        TrapText.gameObject.SetActive(false);

        // 구역에 따른 텍스트 팝업 비활성화 조건
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
        // 박스의 좌표를 월드좌표로 변환
        Vector3 boxWorldCenter = transform.position + boxCenter;

        // 플레이어 레이어마스크 설정 필수
        Collider[] hits = Physics.OverlapBox(boxWorldCenter, boxSize / 2, Quaternion.identity, playerLayer);

        // 플레이어가 감지가 되면
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
