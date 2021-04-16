using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    public Animator animator;
    public Player Player;
    Rigidbody PlayerRigidbody;

    private void Awake()
    {
        PlayerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(PlayerRigidbody.velocity).normalized;
        animator.SetFloat("Horizontal", localVelocity.x, 0.1f, Time.deltaTime);
        animator.SetFloat("Vertical", localVelocity.z, 0.1f, Time.deltaTime);
    }

    public void TriggerAttack()
    {
        if (Player.CanAttack)
        {
            animator.SetTrigger("Attack");
        }
        else
        {
            animator.ResetTrigger("Attack");
        }
    }
}
