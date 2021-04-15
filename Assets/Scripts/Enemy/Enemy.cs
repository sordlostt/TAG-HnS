using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float Health = 100.0f;

    public Transform AttackPoint;
    public float AttackDelay;
    public float AttackDamage;
    public float AttackDistance;
    public float AttackRange;

    public bool IsDead = false;
    public bool CanAttack = true;

    private float AttackTimer;

    private void Awake()
    {
        AttackTimer = AttackDelay;
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
        //TEMPORARY
        IsDead = true;
        gameObject.GetComponentInChildren<Collider>().enabled = false;
        //Destroy(gameObject);
    }

    public void Attack()
    {
        foreach (Collider collider in Physics.OverlapSphere(AttackPoint.position, AttackRange, LayerMask.GetMask("Player")))
        {
            Player player = collider.GetComponentInParent<Player>();
            //Gizmos.DrawSphere(AttackPoint.position, AttackRange);
            if (player != null)
            {
                CanAttack = false;
                StartCoroutine(SetAttackTimer());
                player.SetDamage(AttackDamage);
            }
        }
    }

    public IEnumerator SetAttackTimer()
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
