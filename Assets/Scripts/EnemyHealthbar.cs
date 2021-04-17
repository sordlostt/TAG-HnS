using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthbar : MonoBehaviour
{
    Canvas canvas;
    Enemy enemy;
    Slider slider;

    private void Awake()
    {
        canvas = gameObject.GetComponentInParent<Canvas>();
        enemy = gameObject.GetComponentInParent<Enemy>();
        slider = gameObject.GetComponent<Slider>();
    }

    private void LateUpdate()
    {
        // act like a billboard
        canvas.transform.LookAt(canvas.transform.position + Camera.main.transform.forward);
        slider.value = enemy.GetHealth() / enemy.GetMaxHealth();
    }
}
