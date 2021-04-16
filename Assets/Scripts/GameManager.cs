using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;

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

        gameState = GameState.PLAYING;
    }

    public void OnPlayerDied()
    {
        gameState = GameState.GAME_OVER;
        UIHandler.OnGameOver();
    }

    public void ReloadScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public Player GetPlayer() { return player; }

    public GameState GetGameState()
    {
        return gameState;
    }
}
