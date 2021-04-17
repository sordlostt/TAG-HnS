using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    Enemy enemy;

    public void Spawn()
    {
        GameObject.Instantiate(enemy, transform.position, Quaternion.identity);
    }
}
