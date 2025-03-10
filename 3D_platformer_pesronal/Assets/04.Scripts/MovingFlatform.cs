using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingFlatform : MonoBehaviour
{
    public Rigidbody rigidBody;

    [SerializeField] private float movingSpeed; // �����̴� �ӵ�
    [SerializeField] private float maxMovingDistance; // �����̴� �ִ� �Ÿ�
    [SerializeField] private MoveDirection moveDirection; // �����̴� ����

    private Vector3 startPos;
    private Vector3 dirVec;// ���⺤��
    private Vector3 moveVec; // ���� �̵�����

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
        // �ʱ�ȭ
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

        // �̵� ó��
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

