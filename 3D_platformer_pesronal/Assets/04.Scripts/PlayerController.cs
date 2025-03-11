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
    public LayerMask groundLayerMask; // isgrounded에 쓰임
    public LayerMask wallLayerMask; // isClimbing

    public float useStamina; // 점프/공격할 때 소모되는 stamina
    public float climbingStamina; // 벽 탈 때 소모되는 스태미나

    [Header("Look")]
    public Transform cameraContainer;
    //public Transform playerPosition;
    public float minLook;
    public float maxLook;

    private float camCurXRot; // 마우스 회전 값
    public float lookSensitivy; // 마우스 민감도
    private Vector2 mouseDelta; // 마우스 위치값


    private Rigidbody rigidbody;
    public AnimationHandler animationHandler;

    //인벤토리
    public Action inventory;
    public bool canLook = true; // 마우스커서 잠김

    public PlayerConditions playerConditions;

    // 런처
    private LauncherObject currentLauncher; // 현재 접촉한 런처


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

    // 이동 입력
    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            // 키를 계속 누르면, 값을 불러와서 계속 움직이기
            curMovementInput = context.ReadValue<Vector2>();
            animationHandler.Move();
        }
        else if (context.phase == InputActionPhase.Canceled)
        {
            // 키를 떼면, 멈추기
            curMovementInput = Vector2.zero;
            animationHandler.Idle();
        }
    }

    // 실제 이동
    private void Move()
    {
        // 방향 (앞/뒤 + 좌/우)
        Vector3 dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
        
        
        // 벽타기
        if (IsClimbing()  && CharacterManager.Instance.Player.condition.UseStamina(climbingStamina)) 
        {
            dir = transform.up * curMovementInput.y + transform.right * curMovementInput.x;
          dir *= moveSpeed;
            rigidbody.useGravity = false;
            rigidbody.velocity = dir;  
        }
       
            //// 대각선 이동 시 속도 균일화
            //if (dir.magnitude > 1f)
            //    dir.normalized();
           // dir = transform.forward * curMovementInput.y + transform.right * curMovementInput.x;
            // 속력 = 방향 x 속도
            dir *= moveSpeed;

            // 기존 y 속도 유지
            dir.y = rigidbody.velocity.y;

            // Rigidbody 속도 적용
            rigidbody.velocity = dir;
            rigidbody.useGravity = true;
        
        
    }

    // 회전 입력값
    public void OnLook(InputAction.CallbackContext context)
    {
        // 마우스는 계속해서 값이 유지되므로
        mouseDelta = context.ReadValue<Vector2>();
    }

    // 실제 회전
    void CameraLook()
    {
        camCurXRot += mouseDelta.y * lookSensitivy;
        // 회전값이 최소값, 최대값을 벗어나지 않게
        camCurXRot = Mathf.Clamp(camCurXRot, minLook, maxLook);

        cameraContainer.localEulerAngles = new Vector3(-camCurXRot, 0, 0);
        // 캐릭터의 각도
        //playerPosition.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivy, 0);
        transform.eulerAngles += new Vector3(0, mouseDelta.x * lookSensitivy, 0);

        
    }

    // 점프 입력
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && isGrounded() && CharacterManager.Instance.Player.condition.UseStamina(useStamina))
        {
           
                rigidbody.AddForce(Vector2.up * jumpPower, ForceMode.Impulse);
                animationHandler.Jump();
        }
    }

    // 현재 플레이어가 땅에 있는지, 공중에 있는지 확인하는 함수
    bool isGrounded()
    {
        // Ray 생성
        Ray[] rays = new Ray[4]
        {
            new Ray(transform.position + (transform.forward * 0.2f) + (transform.up  *0.01f), Vector3.down),
            new Ray(transform.position + (-transform.forward * 0.2f) + (transform.up  *0.01f), Vector3.down),
            new Ray(transform.position + (transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down),
            new Ray(transform.position + (-transform.right * 0.2f) + (transform.up * 0.01f), Vector3.down)
        };

        // Ray 검출
        for(int i = 0; i<rays.Length; i++)
        {
            // groundLayerMask 검출, player만 제외해야 함
            if (Physics.Raycast(rays[i], 0.1f, groundLayerMask))
            {
                
                return true; // 걸리면 True 반환
            }
        }
        return false; // 아무것도 안 걸리면 False 반환
    }

    // 벽 인식
    bool IsClimbing()
    {

        // Ray 검출


        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1f, wallLayerMask))
        {
            Debug.Log("벽");
            
            return true;
        }
            return false; // 아무것도 안 걸리면 False 반환

    }
    // 플랫폼 발사대 충돌 여부

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent<LauncherObject>(out LauncherObject launcher))
        {
            Debug.Log("런처 충돌");
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
   
    // 플랫폼 발사대

    public void OnLauncher(InputAction.CallbackContext context)
    {
        if(context.phase == InputActionPhase.Started && currentLauncher != null)
        {
            Debug.Log("R 누름");
            StartCoroutine(Launch());
        }
    }
    private IEnumerator Launch()
    {
        Debug.Log("발사 실행됨");
        Vector3 startPos = transform.position;
        Vector3 endPos = transform.position + transform.forward * 40f + Vector3.down * 5f; // z 좌표 +30, y 좌표 +10
        float duration = 2f; // 이동시간
        float elapsedTime = 0f; // 경과시간
        float height = 20f; // 포물선 높이

        Rigidbody rb = GetComponent<Rigidbody>();

        while (elapsedTime < duration)
        {
            // 포물선 운동 계산
            Vector3 targetPos = Parabola(startPos, endPos, height, elapsedTime / duration);
            // 이동
            rb.MovePosition(targetPos);

            elapsedTime += Time.deltaTime;
            yield return null;

        }
    }

    // 발사대-포물선 함수
    protected static Vector3 Parabola(Vector3 start, Vector3 end, float height, float t)
    {
        Func<float, float> f = x => -4 * height * x * x + 4 * height * x;
        Vector3 mid = Vector3.Lerp(start, end, t);
        return new Vector3(mid.x, f(t) + Mathf.Lerp(start.y, end.y, t), mid.z);
    }


    ///////인벤토리
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
