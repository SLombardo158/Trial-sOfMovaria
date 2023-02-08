using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyBat : MonoBehaviour
{
    public float speed;
    public float stoppingDistance;
    public float retreatingDistance;
    public bool isShooting = false;

    private float timeBtwShots;
    public float startTimeBtwShots;

    public GameObject projectile;
    public Transform player;

    public float health;
    private Rigidbody2D rb;
    [SerializeField] private float maxHealth;
    public float moveSpeed = 1f;
    public float moveSpeedOne = 1f;
    public float shootTimer = 1f;
    public float speedHoldValue;

    float timer = 1000f;
    private bool lightingHit = false;
    public bool closer;
    public bool away;

    public bool isHit = false;
    float timerHit = 10f;

    void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        speedHoldValue = speed;
        if (MenuManager.player == 1)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (MenuManager.player == 2)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        if (MenuManager.player == 4)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isHit)
        {
            if (timerHit <= 1)
            {
                isHit = false;
            }
            else
            {
                timerHit -= 1f;
            }
        }
        if (Vector2.Distance(transform.position, player.position) > stoppingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * speed * Time.deltaTime);
        }
        else if ((Vector2.Distance(transform.position, player.position) <= stoppingDistance) && (Vector2.Distance(transform.position, player.position) >= retreatingDistance))
        {
            transform.position = this.transform.position;
        }
        else if (Vector2.Distance(transform.position, player.position) < retreatingDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, -speed * moveSpeed * Time.deltaTime);
        }
        Vector3 enemyLookDirection = transform.forward;
        Vector3 playerRelativeDirection = (player.transform.position - transform.position).normalized;

        float angle = Vector3.Angle(enemyLookDirection, playerRelativeDirection);

        if (timeBtwShots <= 1)
        {
            isShooting = true;
            speed = 0;
            //shootTimer = 1f;
        }
        else
        {
            isShooting = false;
            speed = speedHoldValue;
            //  shootTimer -= Time.deltaTime;
        }
        if (timeBtwShots <= 0)
        {
            Debug.Log("Shoot");

            transform.position = Vector2.MoveTowards(transform.position, player.position, moveSpeed * speed * Time.deltaTime);

            Vector3 dir = player.position - transform.position;

            float angleTwo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angleTwo - 90));

            transform.position = Vector2.MoveTowards(transform.position, player.position, 0);
            // transform.position = Vector2.MoveTowards(0, 0, 0);
            Instantiate(projectile, transform.position, rot);

            timeBtwShots = startTimeBtwShots;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
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
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        Debug.Log("Enemy Ranged Health: " + health);

        isHit = true;
        timerHit = 10f;

        if (health <= 0)
        {
            speed = 0;
            Destroy(gameObject, .2f);
            //   gameObject.SetActive(false);
        }
        UpdateHealth(health);
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
            Debug.Log("Enemy ranged Death");
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            player = other.transform;
        }
    }
}
