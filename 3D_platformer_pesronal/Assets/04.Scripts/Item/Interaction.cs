using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{ // �÷��̾�� ��ü�� ��ȣ�ۿ�

    public GameObject curInteractGameObject; // ���� ��ȣ�ۿ� ���� ���ӿ�����Ʈ
    public IInteractable curInteractable; // �������̽� ����
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    // EŰ ������ ��
    void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            curInteractable.OnInteract();
        }
    }
}
