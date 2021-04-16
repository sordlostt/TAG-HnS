using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour, ICharacter
{
    public Collider mainCollider;

    public EnemyHealthbar healthBar;
    public float maxHealth = 100.0f;
    float health;

    public float attackDelay;
    public float attackDistance;

    // time before body starts fading out
    public float fadingTime;
    // speed modifier for the body fading out process
    public float fadingSpeed;
    float fadingTimer;

    public bool isDead = false;
    public bool canAttack = true;

    bool isAttacking;
    bool isAttacked;
    float attackTimer;

    EnemyAnimationManager animationManager;

    private void Awake()
    {
        health = maxHealth;
        attackTimer = attackDelay;
        fadingTimer = fadingTime;
        animationManager = gameObject.GetComponent<EnemyAnimationManager>();
    }

    public void SetDamage(float damage)
    {
        health -= damage;

        if (health <= 0.0f)
        {
            OnDeath();
        }
        else
        {
            animationManager.TriggerOnDamage();
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
        while (fadingTimer > 0.0f)
        {
            fadingTimer -= Time.deltaTime;
            yield return null;
        }

        while (gameObject.transform.position.y > -10.0f)
        {
            gameObject.transform.Translate(Vector3.down * fadingSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(gameObject);
    }

    public float GetHealth()
    {
        return health;
    }

    public bool IsAttacking()
    {
        return isAttacking;
    }
}
