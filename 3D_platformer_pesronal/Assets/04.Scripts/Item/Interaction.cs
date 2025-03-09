using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{ // 플레이어와 물체의 상호작용

    public GameObject curInteractGameObject; // 현재 상호작용 중인 게임오브젝트
    public IInteractable curInteractable; // 인터페이스 참조
    
    void Start()
    {
        
    }

    void Update()
    {
        
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
