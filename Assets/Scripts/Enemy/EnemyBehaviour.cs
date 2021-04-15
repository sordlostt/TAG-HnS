using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    Rigidbody EnemyRigidbody;
    Player Player;
    NavMeshAgent NavAgent;
    private void Start()
    {
        Player = GameManager.instance.GetPlayer();
        NavAgent = gameObject.GetComponent<NavMeshAgent>();
        EnemyRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    Vector3 lastTargetPos = Vector3.zero;

    private void Update()
    {
        if (Player.transform.position != lastTargetPos)
        {
            NavAgent.SetDestination(Player.transform.position);
        }
        Vector3 playerPositionAdjusted = new Vector3(Player.transform.position.x, transform.position.y, Player.transform.position.z);
        transform.LookAt(playerPositionAdjusted, transform.up);
        Debug.DrawRay(transform.position, transform.forward * 5.0f, Color.blue);
        lastTargetPos = Player.transform.position;
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
