using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float Health = 100.0f;

    public float AttackRange;
    public float AttackDelay;

    public Transform AttackPoint;

    private float AttackTimer;
    private bool CanAttack = true;

    private void Awake()
    {
        AttackTimer = AttackDelay;
    }

    private void Update()
    {

    }

    public void Attack()
    {
        if (CanAttack)
        {
            foreach (Collider collider in Physics.OverlapSphere(AttackPoint.position, AttackRange, LayerMask.GetMask("Enemy")))
            {
                Enemy enemy = collider.GetComponentInParent<Enemy>();
                //Gizmos.DrawSphere(AttackPoint.position, AttackRange);
                if (enemy != null)
                {
                    CanAttack = false;
                    StartCoroutine(StartAttackTimer());
                    enemy.SetDamage(50.0f);
                }
            }
        }
    }

    public void SetDamage(float damage)
    {
        Health -= damage;

        if (Health <= 0.0f)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        //die
    }

    private IEnumerator StartAttackTimer()
    {
        while (AttackTimer > 0.0f)
        {
            AttackTimer -= Time.deltaTime;
            yield return null;
        }
        AttackTimer = AttackDelay;
        CanAttack = true;
    }
}
