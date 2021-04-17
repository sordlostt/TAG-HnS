using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    [SerializeField]
    Animator animator;

    public void SetWalking(bool value)
    {
        animator.SetBool("Walking", value);
    }

    public void SetDying(bool value)
    {
        animator.SetBool("Dead", value);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void TriggerOnDamage()
    {
        animator.SetTrigger("On Damage Received");
    }
}
