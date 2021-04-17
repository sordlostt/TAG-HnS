using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Image gameOverScreen;
    [SerializeField]
    TMP_Text gameOverText;
    [SerializeField]
    TMP_Text gameTimer;
    [SerializeField]
    TMP_Text objectiveText;
    [SerializeField]
    Button restartButton;

    [SerializeField]
    float screenFadeInTime;
    [SerializeField]
    float textFadeInTime;
    [SerializeField]
    float buttonFadeInTime;
    [SerializeField]
    float objectiveFadeTime;

    Player player;

    private void Awake()
    {
        gameOverScreen.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
        objectiveText.gameObject.SetActive(true);
        gameTimer.gameObject.SetActive(true);
        healthBar.gameObject.SetActive(true);
    }

    private void Start()
    {
        restartButton.onClick.AddListener(GameManager.instance.ReloadScene);
        player = GameManager.instance.GetPlayer();
        StartCoroutine(StartFadingObjective());
    }

    private void Update()
    {
        healthBar.value = player.GetHealth() / player.GetMaxHealth();
        float gameTime = GameManager.instance.GetGameTime();
        float elapsedTime = GameManager.instance.GetElapsedTime();
        gameTimer.text = $"Time Left: {Mathf.Floor((gameTime - elapsedTime) / 60.0f).ToString("00")}:{Mathf.Floor((gameTime - elapsedTime) % 60.0f).ToString("00")}";
    }

    public void OnTimerEnd()
    {
        gameTimer.gameObject.SetActive(false);
    }

    public void OnGameOver()
    {
        healthBar.gameObject.SetActive(false);
        gameTimer.gameObject.SetActive(false);
        StartCoroutine(FadeInGameOverScreen());
    }

    public void OnGameComplete()
    {
        // just change the text and fade the screen out as if the player died
        gameOverText.text = "Game Complete";
        OnGameOver();
    }

    private IEnumerator StartFadingObjective()
    {
        yield return new WaitForSeconds(1.5f);

        for (float t = 0.0f; t < objectiveFadeTime; t += Time.deltaTime)
        {
            objectiveText.CrossFadeAlpha(0.0f, objectiveFadeTime, false);
            yield return null;
        }

        objectiveText.gameObject.SetActive(false);
    }

    private IEnumerator FadeInGameOverScreen()
    {
        gameOverScreen.gameObject.SetActive(true);
        gameOverScreen.CrossFadeAlpha(0.0f, 0.0f, true);

        for (float t = 0.0f; t < screenFadeInTime; t += Time.deltaTime)
        {
            gameOverScreen.CrossFadeAlpha(1.0f, screenFadeInTime, false);
            yield return null;
        }

        gameOverText.gameObject.SetActive(true);
        gameOverText.CrossFadeAlpha(0.0f, 0.0f, true);

        for (float t = 0.0f; t < textFadeInTime; t += Time.deltaTime)
        {
            gameOverText.CrossFadeAlpha(1.0f, textFadeInTime, false);
            yield return null;
        }

        restartButton.gameObject.SetActive(true);
        restartButton.image.CrossFadeAlpha(0.0f, 0.0f, true);

        for (float t = 0.0f; t < buttonFadeInTime; t += Time.deltaTime)
        {
            restartButton.image.CrossFadeAlpha(1.0f, buttonFadeInTime, false);
            yield return null;
        }

    }
}
