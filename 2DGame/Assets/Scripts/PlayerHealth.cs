using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float health = 0f;
    [SerializeField] private float maxHealth = 100f;
    public HealthBar healthBar;
    [SerializeField] private Slider healthSlider;
    private Rigidbody2D rb;

    float timerHit = 10f;
    public bool playerIsHit;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }    

    public void UpdateHealth(float mod)
    {
        health += mod;

        if(health > maxHealth)
        {
            health = maxHealth;
        }
        else if(health <= 0f)
        {
            health = 0f;
            healthBar.SetHealth(health);
            RestartLevel();
            deathFunction();
            Debug.Log("Player Respawn");
        }
        if(mod < 0)
        {
            playerIsHit = true;
            timerHit = 20f;
        }
        AdjustBar(mod);
    }
    private void FixedUpdate()
    {
        if (playerIsHit)
        {
            if (timerHit <= 1)
            {
                playerIsHit = false;
            }
            else
            {
                timerHit -= 1f;
            }
        }
    }
    void AdjustBar(float damage)
    {
        healthBar.SetHealth(health);
    }

    private void OnGUI()
    {
        float t = Time.deltaTime / 1f;
        healthBar.SetHealth(Mathf.Lerp(healthSlider.value, health, t));
    }

    private void deathFunction()
    {
        //Death Animation here
        Debug.Log("Death");

        RestartLevel();
    }

    private void RestartLevel()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
