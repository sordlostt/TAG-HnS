using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, ICharacter
{
    [SerializeField]
    Collider mainCollider;

    [SerializeField]
    EnemyHealthbar healthBar;

    [SerializeField]
    float maxHealth = 100.0f;

    [SerializeField]
    float attackDelay;

    [SerializeField]
    float attackDistance;

    // time before body starts fading out
    [SerializeField]
    float bodyFadingTime;

    // speed modifier for the body fading out process
    [SerializeField]
    float bodyFadingSpeed;

    float health;
    float bodyFadingTimer;
    float attackTimer;

    bool isDead = false;
    bool canAttack = true;
    bool isAttacking = false;

    EnemyAnimationManager animationManager;

    private void Awake()
    {
        health = maxHealth;
        attackTimer = attackDelay;
        bodyFadingTimer = bodyFadingTime;
        animationManager = gameObject.GetComponent<EnemyAnimationManager>();
    }

    public void SetDamage(float damage, ICharacter attacker)
    {
        if (attacker is Player)
        {
            health -= damage;
            isAttacking = false;
            if (health <= 0.0f)
            {
                OnDeath();
            }
            else
            {
                animationManager.TriggerOnDamage();
            }
        }
    }

    public void Attack()
    {
        if (canAttack)
        {
            canAttack = false;
            animationManager.TriggerAttack();
            StartCoroutine(SetAttackTimer());
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

    private void OnDeath()
    {
        isDead = true;
        mainCollider.enabled = false;
        healthBar.gameObject.SetActive(false);
        NavMeshAgent navAgent = gameObject.GetComponent<NavMeshAgent>();
        navAgent.isStopped = true;
        navAgent.enabled = false;
        StartCoroutine(StartFading());
    }

    public IEnumerator SetAttackTimer()
    {
        while (attackTimer > 0.0f)
        {
            attackTimer -= Time.deltaTime;
            yield return null;
        }

        attackTimer = attackDelay;
        canAttack = true;
    }

    public IEnumerator StartFading()
    {
        while (bodyFadingTimer > 0.0f)
        {
            bodyFadingTimer -= Time.deltaTime;
            yield return null;
        }

        while (gameObject.transform.position.y > -1.0f)
        {
            transform.Translate(Vector3.down * bodyFadingSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }

    public bool IsDead()
    {
        return isDead;
    }

    public float GetAttackDistance()
    {
        return attackDistance;
    }
}
