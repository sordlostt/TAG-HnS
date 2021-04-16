using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    private float health = 100.0f;

    [SerializeField]
    private float attackDelay;

    private float attackTimer;
    private bool canAttack = true;
    private bool isAttacking = false;

    private PlayerAnimationManager animationManager;

    private void Awake()
    {
        attackTimer = attackDelay;
        animationManager = gameObject.GetComponent<PlayerAnimationManager>();
    }

    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            StartCoroutine(SetAttackTimer());
            animationManager.TriggerAttack();
        }
    }

    public void SetDamage(float damage)
    {
        health -= damage;

        if (health <= 0.0f)
        {
            OnDeath();
        }
    }

    public void OnAttackBegin()
    {
        isAttacking = true;
    }

    public void OnAttackEnd()
    {
        isAttacking = false;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    private void OnDeath()
    {
        //die
    }

    private IEnumerator SetAttackTimer()
    {
        while (attackTimer > 0.0f)
        {
            attackTimer -= Time.deltaTime;
            yield return null;
        }
        attackTimer = attackDelay;
        canAttack = true;
    }
}
