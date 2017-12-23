using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class HealthComponent : MonoBehaviour {
    [SerializeField]
    private Slider healthSlider;
    private int currentHealth;
    [SerializeField]
    private float speed = 5f;

    public bool IsOver
    {
        get
        {
            return currentHealth <= healthSlider.minValue;
        }
    }

    [ContextMenu("Test Init 100")]
    private void TestInit()
    {
        Init(100);
    }

    [ContextMenu("Test Hurt 50")]
    private void TestHurt()
    {
        Hurt(50);
    }

    public void Init(int maxHealth)
    {
        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;
        currentHealth = maxHealth;
    }

	public void Hurt(int damage)
    {
        currentHealth -= damage;
        currentHealth = (int)Mathf.Max(currentHealth, healthSlider.minValue);
    }

    private void Update()
    {
        healthSlider.value = Mathf.Lerp(healthSlider.value, currentHealth, Time.deltaTime * speed);
    }
}
