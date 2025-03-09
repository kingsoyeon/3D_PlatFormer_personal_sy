using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{ // 플레이어와 물체의 상호작용

    public GameObject curInteractGameObject; // 현재 상호작용 중인 게임오브젝트
    public IInteractable curInteractable; // 인터페이스 참조


    // ray에 필요한 변수
    public float maxCheckDistance; // 최대거리
    public LayerMask layerMask; // 레이어마스크

    private Camera camera;
    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        // 물체 감지
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
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

            }
        }
        // 검출되지 않으면
        else
        {
            curInteractable = null;
            curInteractGameObject = null;

        }
    }


    // E키 눌렀을 때
    void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            curInteractable.OnInteract();
        }
    }
}
