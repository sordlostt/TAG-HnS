using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;


    [System.Serializable]
    public struct SpawningInterval
    {
        public float levelPercentage;
        public float spawningInterval;
    }

    public enum GameState
    {
        PLAYING,
        GAME_OVER
    };

    GameState gameState;

    [SerializeField]
    Player player;

    [SerializeField]
    UIHandler UIHandler;

    //in minutes
    [SerializeField]
    float gameTime;

    [SerializeField]
    List<SpawningInterval> spawningIntervals = new List<SpawningInterval>();

    [SerializeField]
    List<EnemySpawner> spawners = new List<EnemySpawner>();

    [SerializeField]
    HeliBehaviour helicopter;

    float levelTimer;
    float levelPercentage;
    float activeSpawningInterval;
    float spawningTimer;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }

        // make sure that the spawning list is sorted by level completion percentage
        spawningIntervals.Sort((p1, p2) => p1.levelPercentage.CompareTo(p2.levelPercentage));
        activeSpawningInterval = spawningIntervals[0].spawningInterval;
        spawningTimer = activeSpawningInterval;
        gameTime *= 60.0f;
        levelTimer = 0.0f;
        gameState = GameState.PLAYING;
        helicopter.gameObject.SetActive(false);
    }

    private void Update()
    {
        levelTimer += Time.deltaTime;

        levelPercentage = levelTimer/gameTime;

        foreach (SpawningInterval interval in spawningIntervals)
        {
            if (levelPercentage > interval.levelPercentage)
            {
                activeSpawningInterval = interval.spawningInterval;
            }
        }

        spawningTimer -= Time.deltaTime;

        if (spawningTimer <= 0.0f)
        {
            SpawnEnemy();
            spawningTimer = activeSpawningInterval;
        }

        if (levelTimer >= gameTime && gameState == GameState.PLAYING)
        {
            OnGameComplete();
        }
    }

    public void OnPlayerDied()
    {
        gameState = GameState.GAME_OVER;
        UIHandler.OnGameOver();
    }

    public void OnGameComplete()
    {
        helicopter.gameObject.SetActive(true);
        UIHandler.OnTimerEnd();
        if (helicopter.isOnTarget())
        {
            gameState = GameState.GAME_OVER;
            UIHandler.OnGameComplete();
        }
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    private void SpawnEnemy()
    {
        EnemySpawner spawnerToSpawn = spawners[Random.Range(0, spawners.Count)];
        spawnerToSpawn.Spawn();
    }

    public Player GetPlayer() { return player; }

    public GameState GetGameState()
    {
        return gameState;
    }

    public float GetGameTime()
    {
        return gameTime;
    }

    public float GetElapsedTime()
    {
        return levelTimer;
    }
}
