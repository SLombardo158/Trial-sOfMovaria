using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyFollowWhenNear : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 3f;
    public float speedPhaseTwo = 1.5f;
    [SerializeField] private float attackDamage = 10f;
    [SerializeField] private float attackSpeed = 1f;
    private float canAttack;
    public bool seen = false;
    public bool pass = true;
    float timer = 1000f;
    float timerHit = 10f;
    private bool lightingHit = false;
    [Header("Health")]

    public float health;
    public float moveSpeed = 1f;
    public float moveSpeedOne = 1f;
    [SerializeField] private float maxHealth;
    public bool isHit = false;
    

    private Transform target;
    public float step = 0f;

    private float waitTime;
    public float startWaitTime;
    private Rigidbody2D rb;
    //public Transform wallLocation;

    bool hasPlayerBeenSeen = false;

    private void Start()
    {
        health = maxHealth;
        rb = GetComponent<Rigidbody2D>();

        waitTime = startWaitTime;
    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        Debug.Log("Enemy Follow When Near Health: " + health);

        isHit = true;
        timerHit = 10f;

        if (health <= 0)
        {
            speed = 0;
          //  Destroy(gameObject, .2f);
         //   gameObject.SetActive(false);
        }
        UpdateHealth(health);
    }
    private void FixedUpdate()
    {
        //if (target != null)
        if (hasPlayerBeenSeen)
        {
            if (target != null)
            {
                step = moveSpeedOne * speedPhaseTwo * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, target.position, step);
                seen = true;
            }
        }
        else if(seen)
        {            
            step = moveSpeedOne * speedPhaseTwo * Time.deltaTime;
            transform.position = Vector2.MoveTowards(transform.position, target.position, step);            
        }
        if(moveSpeed == .25f || lightingHit)
        {
            if (!lightingHit)
            {
                lightingHit = true;
                timer = timer + 10 * 7;
                moveSpeedOne = .25f;
            }
            if(timer <= 0)
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
        if(isHit)
        {
            if(timerHit <= 1)
            {
                isHit = false;
            }
            else
            {
                timerHit -= 1f;
            }
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
                //Attack animation called here
            }
            else
            {
                canAttack += Time.deltaTime;
            }
        }
    }
    private void OnCollisionStay2D(Collision2D other)
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
            if (seen)
            {
                target = other.transform;
            }
            else
            {
                target = null;
            }
        }
        if(other.gameObject.tag == "Projectile")
        {
            //isHit = false;
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
            Debug.Log("Enemy minor Death");
        }
    }
}
