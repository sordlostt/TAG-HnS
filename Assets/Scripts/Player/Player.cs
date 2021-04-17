using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, ICharacter
{
    [SerializeField]
    float maxHealth;

    [SerializeField]
    float damage;

    [SerializeField]
    float attackDelay;

    [SerializeField]
    float healthRegenSpeed;

    [SerializeField]
    float healthRegenKickInTime;

    float healthRegenTimer;
    float health = 100.0f;
    float attackTimer;

    bool canAttack = true;
    bool isAttacking = false;

    PlayerAnimationManager animationManager;

    private void Awake()
    {
        attackTimer = attackDelay;
        animationManager = gameObject.GetComponent<PlayerAnimationManager>();
        health = maxHealth;
        healthRegenTimer = healthRegenKickInTime;
        gameObject.GetComponentInChildren<Weapon>().AssignDamage(damage);
    }

    private void Update()
    {
        if (health < maxHealth)
        {
            healthRegenTimer -= Time.deltaTime;

            if (healthRegenTimer <= 0.0f)
            {
                health += healthRegenSpeed * Time.deltaTime;
            }
        }
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

    public void SetDamage(float damage, ICharacter attacker)
    {
        if (attacker is Enemy)
        {
            health -= damage;
            healthRegenTimer = healthRegenKickInTime;
            // end current attack
            isAttacking = false;
            if (health <= 0.0f && GameManager.instance.GetGameState() == GameManager.GameState.PLAYING)
            {
                OnDeath();
            }
            else
            {
                animationManager.TriggerDamage();
            }
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
        animationManager.SetDying();
        GameManager.instance.OnPlayerDied();
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

    public float GetHealth()
    {
        return health;
    }

    public float GetMaxHealth()
    {
        return maxHealth;
    }

}
