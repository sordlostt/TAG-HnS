using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationManager : MonoBehaviour
{
    public Animator Animator;
    public Player Player;

    public void SetWalking(bool value)
    {
        Animator.SetBool("Walking", value);
    }

    public void SetDying(bool value)
    {
        Animator.SetBool("Dead", value);
    }

    public void TriggerAttack()
    {
        Animator.SetTrigger("Attack");
    }

    public void ResetAttack()
    {
        Animator.ResetTrigger("Attack");
    }
}
