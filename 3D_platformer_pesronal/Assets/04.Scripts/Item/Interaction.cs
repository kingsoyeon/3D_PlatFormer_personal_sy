using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class Interaction : MonoBehaviour
{ // �÷��̾�� ��ü�� ��ȣ�ۿ�

    public GameObject curInteractGameObject; // ���� ��ȣ�ۿ� ���� ���ӿ�����Ʈ
    public IInteractable curInteractable; // �������̽� ����


    // ray�� �ʿ��� ����
    public float maxCheckDistance; // �ִ�Ÿ�
    public LayerMask layerMask; // ���̾��ũ

    public float checkRate = 0.05f; // 0.05�ʸ��� ray CHECK
    private float lastCheckTime; // ������ RAY üũ�ð�

    private Camera camera;

    // ������ ���� ������Ʈ
    public TextMeshProUGUI promptText;
    
    void Start()
    {
        camera = Camera.main;
    }

    void Update()
    {


        // rayüũ�ֱ⺸�� �� ���� �ð��� �귶�� ��, ray�� �������ش�.
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;
            // ��ü ����
            Ray ray = camera.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2));
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
                    SetPromptText();
                }
            }

            // ������� ������
            else
            {
                // ��ȣ�ۿ� ������ �ʰ� ������Ʈ�� ����
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

    // EŰ ������ ��
    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();

            // eŰ ������ �� �κ����� ���Ƿ� 
            curInteractable = null;
            curInteractGameObject=null;

            // ������Ʈ�� ����
            promptText.gameObject.SetActive(false);
        }
    }
}
