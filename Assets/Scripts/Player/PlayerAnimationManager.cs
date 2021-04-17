using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationManager : MonoBehaviour
{
    [SerializeField]
    Animator animator;
    Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        /* get a normalized vector of the player's rigidbody velocity in local space,
           so that the Animator Controller can adjust the moving animation in relation to our rotation*/

        Vector3 localVelocity = transform.InverseTransformDirection(playerRigidbody.velocity).normalized;
        animator.SetFloat("Horizontal", localVelocity.x, 0.125f, Time.deltaTime);
        animator.SetFloat("Vertical", localVelocity.z, 0.125f, Time.deltaTime);
    }

    public void TriggerAttack()
    {
        animator.SetTrigger("Attack");
    }

    public void TriggerDamage()
    {
        animator.SetTrigger("On Damage Received");
    }

    public void SetDying()
    {
        animator.SetBool("Is Dead", true);
    }
}
