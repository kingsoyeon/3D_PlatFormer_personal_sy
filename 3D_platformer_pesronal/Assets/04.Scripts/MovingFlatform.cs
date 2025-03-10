using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFlatform : MonoBehaviour
{
    public Rigidbody rigidBody;

    [SerializeField] private float movingSpeed; // 움직이는 속도
    [SerializeField] private float maxMovingDistance; // 움직이는 최대 거리
    [SerializeField] private MoveDirection moveDirection; // 움직이는 방향

    private Vector3 startPos;
    private Vector3 dirVec;// 방향벡터
    private Vector3 moveVec; // 실제 이동벡터

    public enum MoveDirection
    {
        Vertical,
        Horizontal
    }

    void Start()
    {
        rigidBody = GetComponentInParent<Rigidbody>();
        startPos = transform.position;

        //rigidBody.MovePosition(transform.position * maxMovingDistance);
        // 초기화
        switch (moveDirection)
        {
            case MoveDirection.Vertical:
                dirVec = Vector3.forward;
                break;
            case MoveDirection.Horizontal:
                dirVec = Vector3.right;
                break;
        } 
    }
        void Update()
        {

        // 이동 처리
        Vector3 moveVec = dirVec * movingSpeed * Time.deltaTime;
        rigidBody.MovePosition(transform.position + moveVec);


        if (Vector3.Distance(startPos, transform.position) >= maxMovingDistance) 
        {
            dirVec = -dirVec;
            startPos = transform.position;
        }


        //if (isRight)
        //{
        //    moveVec = dirVec * movingSpeed * Time.deltaTime;
        //    rigidBody.MovePosition(transform.position + moveVec);
        //    if (dirVec.x >= maxMovingDistance)
        //        isRight = false;
        //}
        //else
        //{
        //    moveVec = -dirVec * movingSpeed * Time.deltaTime;
        //    rigidBody.MovePosition(transform.position + moveVec);
        //    if (dirVec.x <= maxMovingDistance)
        //        isRight = true;
        //}

        }
    }

