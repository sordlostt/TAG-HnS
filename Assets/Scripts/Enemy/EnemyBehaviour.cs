using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    Rigidbody enemyRigidbody;
    Player player;
    NavMeshAgent navAgent;
    EnemyAnimationManager animationManager;
    Enemy enemy;

    private void Start()
    {
        player = GameManager.instance.GetPlayer();
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        enemyRigidbody = gameObject.GetComponent<Rigidbody>();
        enemy = gameObject.GetComponent<Enemy>();
        animationManager = gameObject.GetComponent<EnemyAnimationManager>();
        navAgent.SetDestination(player.transform.position);
    }

    Vector3 lastTargetPos = Vector3.zero;

    private void Update()
    {
        if (!enemy.isDead)
        {
            FollowPlayer();

            // stop the walking animation after crossing the navAgent stopping distance
            if (Vector3.Distance(player.transform.position, enemy.transform.position) <= navAgent.stoppingDistance)
            {
                animationManager.SetWalking(false);
            }
            else
            {
                animationManager.SetWalking(true);
            }

            // attack player after getting within attack range
            if (Vector3.Distance(player.transform.position, enemy.transform.position) <= enemy.attackDistance)
            {
                enemy.Attack();
            }
        }
        else
        {
            enemyRigidbody.Sleep();
            animationManager.SetDying(true);
            animationManager.SetWalking(false);
        }

    }

    private void FollowPlayer()
    {
        if (player.transform.position != lastTargetPos)
        {
            navAgent.SetDestination(player.transform.position);
        }

        Vector3 playerPositionYAdjusted = new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z);
        transform.LookAt(playerPositionYAdjusted, transform.up);
        Debug.DrawRay(transform.position, transform.forward * 5.0f, Color.blue);


        lastTargetPos = player.transform.position;
    }

    private void OnCollisionExit(Collision collision)
    {
        // nullify all velocity or angular velocity gained from colliding with the player/other enemies
        if (collision.gameObject.layer == LayerMask.GetMask("Player, Enemy"))
        {
            enemyRigidbody.velocity = Vector3.zero;
            enemyRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
