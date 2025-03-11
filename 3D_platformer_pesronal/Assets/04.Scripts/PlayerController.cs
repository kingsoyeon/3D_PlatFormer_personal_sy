using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
//using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed;
    public float jumpPower;
    private Vector2 curMovementInput;
    public LayerMask groundLayerMask; // isgrounded�� ����
    public LayerMask wallLayerMask; // isClimbing

    public float useStamina; // ����/������ �� �Ҹ�Ǵ� stamina
    public float climbingStamina; // �� Ż �� �Ҹ�Ǵ� ���¹̳�

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

    // ��ó
    private LauncherObject currentLauncher; // ���� ������ ��ó


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
        
        
        // ��Ÿ��
        if (IsClimbing()  && CharacterManager.Instance.Player.condition.UseStamina(climbingStamina)) 
        {
            dir = transform.up * curMovementInput.y + transform.right * curMovementInput.x;
          dir *= moveSpeed;
            rigidbody.useGravity = false;
            rigidbody.velocity = dir;  
        }
       
            //// �밢�� �̵� �� �ӵ� ����ȭ
            //if (dir.magnitude > 1f)
            //    dir.normalized();
           // dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
            // �ӷ� = ���� x �ӵ�
            dir *= moveSpeed;

            // ���� y �ӵ� ����
            dir.y = rigidbody.velocity.y;

            // Rigidbody �ӵ� ����
            rigidbody.velocity = dir;
            rigidbody.useGravity = true;
        
        
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

    // �� �ν�
    bool IsClimbing()
    {

        // Ray ����


        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f, wallLayerMask))
        {
            Debug.Log("��");
            
            return true;
        }
            return false; // �ƹ��͵� �� �ɸ��� False ��ȯ

    }
    // �÷��� �߻�� �浹 ����

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<LauncherObject>(out LauncherObject launcher))
        {
            Debug.Log("��ó �浹");
            currentLauncher = launcher;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<LauncherObject>(out LauncherObject launcher))
        {
            currentLauncher = null;
        }
    }
   
    // �÷��� �߻��

    public void OnLauncher(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && currentLauncher != null)
        {
            Debug.Log("R ����");
            StartCoroutine(Launch());
        }
    }
    private IEnumerator Launch()
    {
        Debug.Log("�߻� �����");
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + transform.forward * 40f + Vector3.down * 5f; // z ��ǥ +30, y ��ǥ +10
        float duration = 2f; // �̵��ð�
        float elapsedTime = 0f; // ����ð�
        float height = 20f; // ������ ����

        Rigidbody rb = GetComponent<Rigidbody>();

        while (elapsedTime < duration)
        {
            // ������ � ���
            Vector3 targetPos = Parabola(startPos, endPos, height, elapsedTime / duration);
            // �̵�
            rb.MovePosition(targetPos);

            elapsedTime += Time.deltaTime;
            yield return null;

        }
    }

    // �߻��-������ �Լ�
    protected static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
        Vector3 mid = Vector3.Lerp(start, end, t);
        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
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
