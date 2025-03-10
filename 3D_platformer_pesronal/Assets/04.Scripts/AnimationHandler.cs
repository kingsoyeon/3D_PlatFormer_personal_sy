using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationHandler : MonoBehaviour
{
    public Animator animator;

    private static readonly int isAttack = Animator.StringToHash("Attack");
    private static readonly int isMove = Animator.StringToHash("Move");
    private static readonly int isJump = Animator.StringToHash("Jump");
    private static readonly int isCoin = Animator.StringToHash("GetCoin");

    void Start()
    {
        
        animator =  GetComponent<Animator>();
    }

   
    void Update()
    {
        
    }

    public void Move()
    {
        animator.SetBool(isMove, true);
    }
    public void Idle()
    {
        animator.SetBool(isMove, false);
    }

    public void Jump()
    {
        animator.SetTrigger(isJump);
    }
    public void Attack()
    {
        animator.SetTrigger(isAttack);
    }

    public void GetCoin()
    {
        animator.SetTrigger(isCoin);
    }
}
