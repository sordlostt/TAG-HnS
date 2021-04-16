using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public Slider healthBar;
    public Image gameOverScreen;
    public TMP_Text gameOverText;
    public Button restartButton;

    public float screenFadeInTime;
    public float textFadeInTime;
    public float buttonFadeInTime;

    Player player;

    private void Awake()
    {
        gameOverScreen.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(false);
    }

    private void Start()
    {
        restartButton.onClick.AddListener(GameManager.instance.ReloadScene);
        player = GameManager.instance.GetPlayer();
    }

    private void Update()
    {
        healthBar.value = player.GetHealth() / player.maxHealth;
    }

    public void OnGameOver()
    {
        healthBar.gameObject.SetActive(false);
        StartCoroutine(FadeInGameOverScreen());
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
