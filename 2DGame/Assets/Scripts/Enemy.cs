using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    public float speedPhaseTwo = 1.5f; 
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    private float canAttack;
    public Sprite deadSprite;
    [Header("Health")]

    public float health;
    [SerializeField] private float maxHealth;

    public EnemyHealthBar healthBar;
    [SerializeField] private Slider healthSlider;

    private Transform target;
    public float step = 0f;
    public float moveSpeed = 1f;
    public float moveSpeedOne = 1f;
    float timer = 1000f;
    private bool lightingHit = false;

    public Transform moveSpot;
    public float minX;
    public float minY;
    public float maxX;
    public float maxY;
    private float waitTime;
    public float startWaitTime;
    public followP followP;
    public bool haswon = false;
    //public Transform wallLocation;

    public GameObject EnemyHealthBar;
    bool hasPlayerBeenSeen = false;

    public bool isHit = false;
    float timerHit = 5f;

    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        EnemyHealthBar.SetActive(false);

        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        Debug.Log("Enemy Mini Boss Health: " + health);

        isHit = true;
        timerHit = 5f;
        speed = 0;

        if (health <= 0)
        {
            Debug.Log("Death Mini boss");

            speed = 0;
            healthBar.gameObject.SetActive(false);
            Destroy(gameObject, .4f);
            Destroy(healthBar.gameObject);
            
            //gameObject.SetActive(false);
        }
        UpdateHealth(health);
    }
    private void FixedUpdate()
    {
        if (isHit)
        {
            if (timerHit <= 1)
            {
                isHit = false;
                speed = 1;
            }
            else
            {
                timerHit -= 1f;
            }
        }
        if (hasPlayerBeenSeen)
        {
            EnemyHealthBar.SetActive(true);
            if (health > (maxHealth / 2))
            {
                step = moveSpeed * speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, step);
                
                if(Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
                {
                    if(waitTime <= 0)
                    {
                        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                        waitTime = startWaitTime;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                    }
                }
            }
            else if (target != null)
            {                
                step = moveSpeed * speedPhaseTwo * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, target.position, step);
            }
        }  
        else
        {
            EnemyHealthBar.SetActive(false);
        }
        if (moveSpeed == .25f || lightingHit)
        {
            if (!lightingHit)
            {
                lightingHit = true;
                timer = timer + 10 * 100;
                moveSpeedOne = .25f;
            }
            if (timer <= 0)
            {
                moveSpeedOne = 1f;
                moveSpeed = 1f;
                lightingHit = false;
            }
            //moveSpeedOne = .25f;
        }
        if (lightingHit)
        {
            timer -= 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
                step = 0f * Time.deltaTime;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
        if (other.gameObject.tag == "Boundaries")
        {
            moveSpot.position = new Vector2(0, 0);
            transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, step);   
        }
    }
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                other.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
                step = 0f * Time.deltaTime;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (attackSpeed <= canAttack)
            {
                collision.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-attackDamage);
                canAttack = 0f;
                step = 0f * Time.deltaTime;
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
            hasPlayerBeenSeen = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = null;
        }
    }
    public void UpdateHealth(float mod)
    {
        health = mod;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health < 0f)
        {
            health = 0f;
            healthBar.SetHealth(health);
            Debug.Log("Enemy Death");
        }
        if(hasPlayerBeenSeen)
        {
            healthBar.gameObject.SetActive(true);
        }
        if(health == 0f)
        {
            followP.begin();
            healthBar.gameObject.SetActive(false);
            haswon = true;
        }
        AdjustBar(mod);
    }

    void AdjustBar(float health)
    {
        healthBar.SetHealth(health);
    }
}
