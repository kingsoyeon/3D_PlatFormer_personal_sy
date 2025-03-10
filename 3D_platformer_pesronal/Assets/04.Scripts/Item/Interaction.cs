using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Interaction : MonoBehaviour
{ // 플레이어와 물체의 상호작용

    public GameObject curInteractGameObject; // 현재 상호작용 중인 게임오브젝트
    public IInteractable curInteractable; // 인터페이스 참조


    // ray에 필요한 변수
    public float maxCheckDistance; // 최대거리
    public LayerMask layerMask; // 레이어마스크

    public float checkRate = 0.05f; // 0.05초마다 ray CHECK
    private float lastCheckTime; // 마지막 RAY 체크시간

    private Camera camera;

    // 아이템 정보 프롬프트
    public TextMeshProUGUI promptText;
    
    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {


        // ray체크주기보다 더 많은 시간이 흘렀을 때, ray를 실행해준다.
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            // 물체 감지
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
            {
                // 레이로 검출한 애가 현재 상호작용 중인 게임오브젝트가 아니면, 이걸로 바꿔준다.
                if (hit.collider.gameObject != curInteractGameObject)
                {
                    curInteractGameObject = hit.collider.gameObject;
                    // 인터페이스 있는걸로 넣어준다
                    curInteractable = hit.collider.GetComponent<IInteractable>();
                    // 프롬프트 텍스트 띄워주는 함수 호출
                    SetPromptText();
                }
            }

            // 검출되지 않으면
            else
            {
                // 상호작용 중이지 않고 프롬프트도 없다
                curInteractable = null;
                curInteractGameObject = null;
                promptText.gameObject.SetActive(false);
            }
        }
    }

    private void SetPromptText()
    {
        promptText.gameObject.SetActive (true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    // E키 눌렀을 때
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();

            // e키 누르면 내 인벤으로 들어가므로 
            curInteractable = null;
            curInteractGameObject=null;

            // 프롬프트도 꺼줌
            promptText.gameObject.SetActive(false);
        }
    }
}
