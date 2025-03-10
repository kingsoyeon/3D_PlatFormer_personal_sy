using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask; // isgrounded�� ����

    public float useStamina;

    [Header("Look")]
    public Transform cameraContainer;
    //public Transform playerPosition;
    public float minLook;
    public float maxLook;

    private float camCurXRot; // ���콺 ȸ�� ��
    public float lookSensitivy; // ���콺 �ΰ���
    private Vector2 mouseDelta; // ���콺 ��ġ��


    private Rigidbody rigidbody;
    public AnimationHandler animationHandler;

    //�κ��丮
    public Action inventory;
    public bool canLook = true; // ���콺Ŀ�� ���

    public PlayerConditions playerConditions;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
        animationHandler = GetComponent<AnimationHandler>();
        playerConditions = GetComponent<PlayerConditions>();
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        Move();
    }
    void LateUpdate()
    {
        if(canLook)  CameraLook();
    }

    // �̵� �Է�
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // Ű�� ��� ������, ���� �ҷ��ͼ� ��� �����̱�
            curMovementInput = context.ReadValue<Vector2>();
            animationHandler.Move();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            // Ű�� ����, ���߱�
            curMovementInput = Vector2.zero;
            animationHandler.Idle();
        }
    }

    // ���� �̵�
    private void Move()
    {
        // ���� (��/�� + ��/��)
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;

        //// �밢�� �̵� �� �ӵ� ����ȭ
        //if (dir.magnitude > 1f)
        //    dir.normalized();

        // �ӷ� = ���� x �ӵ�
        dir *= moveSpeed;

        // ���� y �ӵ� ����
        dir.y = rigidbody.velocity.y;

        // Rigidbody �ӵ� ����
        rigidbody.velocity = dir;

    }

    // ȸ�� �Է°�
    public void OnLook(InputAction.CallbackContext context)
    {
        // ���콺�� ����ؼ� ���� �����ǹǷ�
        mouseDelta = context.ReadValue<Vector2>();
    }

    // ���� ȸ��
    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivy;
        // ȸ������ �ּҰ�, �ִ밪�� ����� �ʰ�
        camCurXRot = Mathf.Clamp(camCurXRot, minLook, maxLook);

        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        // ĳ������ ����
        //playerPosition.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivy, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivy, 0);

        
    }

    // ���� �Է�
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGrounded() && CharacterManager.Instance.Player.condition.UseStamina(useStamina))
        {
           
                rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
                animationHandler.Jump();
            
        }
    }

    // ���� �÷��̾ ���� �ִ���, ���߿� �ִ��� Ȯ���ϴ� �Լ�
    bool isGrounded()
    {
        // Ray ����
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up  *0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up  *0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        // Ray ����
        for(int i = 0; i<rays.Length; i++)
        {
            // groundLayerMask ����, player�� �����ؾ� ��
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                return true; // �ɸ��� True ��ȯ
            }
        }
        return false; // �ƹ��͵� �� �ɸ��� False ��ȯ
    }


    ///////�κ��丮
    ///

    public void OnInventoryButton(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            inventory?.Invoke();
            ToggleCursor();
        }
    }

    void ToggleCursor()
    {
        bool toggle = Cursor.lockState == CursorLockMode.Locked;
        Cursor.lockState = toggle ? CursorLockMode.None : CursorLockMode.Locked;
        canLook = !toggle;
    }
}
