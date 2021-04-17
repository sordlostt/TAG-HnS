using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField]
    GameObject character;

    public enum SpawnerType
    {
        PERIODICAL,
        ENEMY_INSTANT,
        PLAYER_INSTANT
    }

    [SerializeField]
    SpawnerType spawnerType;

    private void Awake()
    {
        if (spawnerType == SpawnerType.ENEMY_INSTANT)
        {
            SpawnEnemyInstant();
        }
    }

    private void Start()
    {
        if (spawnerType == SpawnerType.PLAYER_INSTANT)
        {
            Destroy(gameObject);
        }
    }

    public void SpawnEnemy()
    {
        Instantiate(character, transform.position, Quaternion.identity);
    }

    public Player SpawnPlayerInstant()
    {
        GameObject playerObj = Instantiate(character, transform.position, Quaternion.identity);
        Player player = playerObj.GetComponent<Player>();
        return player;
    }

    private void SpawnEnemyInstant()
    {
        Instantiate(character, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
