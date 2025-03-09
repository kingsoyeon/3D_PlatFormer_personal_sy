using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{ // �÷��̾�� ��ü�� ��ȣ�ۿ�

    public GameObject curInteractGameObject; // ���� ��ȣ�ۿ� ���� ���ӿ�����Ʈ
    public IInteractable curInteractable; // �������̽� ����


    // ray�� �ʿ��� ����
    public float maxCheckDistance; // �ִ�Ÿ�
    public LayerMask layerMask; // ���̾��ũ

    private Camera camera;
    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {
        // ��ü ����
        Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width/2, Screen.height/2));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxCheckDistance, layerMask))
        {
            // ���̷� ������ �ְ� ���� ��ȣ�ۿ� ���� ���ӿ�����Ʈ�� �ƴϸ�, �̰ɷ� �ٲ��ش�.
            if (hit.collider.gameObject != curInteractGameObject)
            {
                curInteractGameObject = hit.collider.gameObject;
                // �������̽� �ִ°ɷ� �־��ش�
                curInteractable = hit.collider.GetComponent<IInteractable>();
                // ������Ʈ �ؽ�Ʈ ����ִ� �Լ� ȣ��

            }
        }
        // ������� ������
        else
        {
            curInteractable = null;
            curInteractGameObject = null;

        }
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
