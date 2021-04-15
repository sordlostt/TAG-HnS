using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float Health = 100.0f;

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
        Destroy(gameObject);
    }

    public void Attack()
    {

    }
}
