using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    Rigidbody EnemyRigidbody;
    Player Player;
    NavMeshAgent NavAgent;
    EnemyAnimationManager AnimationManager;
    Enemy Enemy;
    private void Start()
    {
        Player = GameManager.instance.GetPlayer();
        NavAgent = gameObject.GetComponent<NavMeshAgent>();
        EnemyRigidbody = gameObject.GetComponent<Rigidbody>();
        Enemy = gameObject.GetComponent<Enemy>();
        AnimationManager = gameObject.GetComponent<EnemyAnimationManager>();
        NavAgent.SetDestination(Player.transform.position);
    }

    Vector3 lastTargetPos = Vector3.zero;

    private void Update()
    {
        if (!Enemy.IsDead)
        {
            if (Player.transform.position != lastTargetPos)
            {
                NavAgent.SetDestination(Player.transform.position);
            }

            Vector3 playerPositionAdjusted = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
            transform.LookAt(playerPositionAdjusted, transform.up);
            Debug.DrawRay(transform.position, transform.forward * 5.0f, Color.blue);

            if (Vector3.Distance(Player.transform.position, Enemy.transform.position) <= NavAgent.stoppingDistance)
            {
                AnimationManager.SetWalking(false);
            }
            else
            {
                AnimationManager.SetWalking(true);
            }

            if (Vector3.Distance(Player.transform.position, Enemy.transform.position) <= Enemy.AttackDistance && Enemy.CanAttack)
            {
                AnimationManager.TriggerAttack();
                Enemy.Attack();
            }
            else
            {
                AnimationManager.ResetAttack();
            }

            lastTargetPos = Player.transform.position;
        }
        else
        {
            NavAgent.isStopped = true;
            EnemyRigidbody.Sleep();
            AnimationManager.SetDying(true);
            AnimationManager.SetWalking(false);
        }

    }

    private void OnCollisionExit(Collision collision)
    {
        // nullify all velocity or angular velocity gained from colliding with other enemies/player
        if (collision.gameObject.layer == LayerMask.GetMask("Player, Enemy"))
        {
            EnemyRigidbody.velocity = Vector3.zero;
            EnemyRigidbody.angularVelocity = Vector3.zero;
        }
    }
}
