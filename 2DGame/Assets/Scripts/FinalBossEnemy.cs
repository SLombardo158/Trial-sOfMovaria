using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalBossEnemy : MonoBehaviour
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

    public bool isDead = false;
    public bool lilDead = false;

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

    public float stoppingDistance;
    public float retreatingDistance;
    public bool isShooting = false;

    private float timeBtwShots;
    public float startTimeBtwShots;

    private float timeBtwShotsTwo;
    public float startTimeBtwShotsTwo;

    public GameObject projectile;
    public Transform player;

    public bool isHit = false;
    float timerHit = 10f;
    public float speedHoldValue;

    public spawn spawn;
    float fMinX = 50.0f;
    float fMaxX = 250.0f;
    int Direction = -1;

    public GameObject playerOne;
    public GameObject playerTwo;
    public GameObject playerThree;

    public FinalVideo FinalVideo;
    public GameObject canvas;

    public bool spawning;

    [SerializeField] private AudioSource hurt;
    private void Start()
    {
        health = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
        EnemyHealthBar.SetActive(false);
               
        waitTime = startWaitTime;
        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
        


        if (MenuManager.player == 1)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            player = playerOne.transform;
        }
        else if (MenuManager.player == 2)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            player = playerTwo.transform;
        }
        else if (MenuManager.player == 4)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
            player = playerThree.transform;
        }
    }
    public void TakeDamage(float dmg)
    {
        health -= dmg;
        Debug.Log("Enemy Final Boss Health: " + health);

        isHit = true;
        timerHit = 20f;
        //speed = 0;

        hurt.Play();

        if (health <= 0)
        {
            Debug.Log("Death Final boss");

            speed = 0;
            healthBar.gameObject.SetActive(false);
            Destroy(gameObject, 1f);
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
            if (health > ((maxHealth * 2) / 3))
            {
                Debug.Log("first stage");
                
                spawn.canspawn = false;
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
                step = moveSpeed * speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, step);

                if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
                {
                    if (waitTime <= 0)
                    {
                        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                        waitTime = startWaitTime;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                    }
                }

                if (timeBtwShots <= .5)
                {
                    isShooting = true;
                }
                else
                {
                    isShooting = false;                   
                }

                if (timeBtwShots <= 0)
                {
                    Debug.Log("Shoot");                 

                    transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, moveSpeed * speed * Time.deltaTime);

                    Vector3 dir = player.position - transform.position;

                    float angleTwo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                    // Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
                    Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angleTwo - 90));

                //    transform.position = Vector2.MoveTowards(transform.position, player.position, 0);
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
            if ((health <= ((maxHealth * 2) / 3)) && (health > (maxHealth / 3)))
            {
                Debug.Log("stage Two");
                
                spawn.canspawn = true;
                
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
                            

                step = moveSpeed * speed * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, step);

                if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
                {
                    if (waitTime <= 0)
                    {
                        moveSpot.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
                        waitTime = startWaitTime;
                    }
                    else
                    {
                        waitTime -= Time.deltaTime;
                    }
                }                

                if (timeBtwShots <= 1.65)
                {
                    isShooting = true;
                }
                else
                {
                    isShooting = false;
                }
                
                if (timeBtwShots <= 1.35)
                {
                    spawning = true;
                }
                else if(timeBtwShots <= 1.20)
                {
                    spawning = false;
                }
                
                if (timeBtwShots <= 1.15)
                {
                    //   isShooting = true;                      

                    transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, moveSpeed * speed * Time.deltaTime);

                    Vector3 dir = player.position - transform.position;               

                    float angleTwo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                    Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angleTwo - 90));

                  //  transform.position = Vector2.MoveTowards(transform.position, player.position, 0);
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
                }
                if (lightingHit)
                {
                    timer -= 1f;
                }
            }
            else if ((health <= (maxHealth / 3)))
            {
                spawn.canspawn = false;
                Debug.Log("stage Three");
                
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
                //      step = moveSpeed * speedPhaseTwo * Time.deltaTime;
                //      player = GameObject.FindGameObjectWithTag("Player").transform;
                //      transform.position = Vector2.MoveTowards(transform.position, player.position, step);
                step = moveSpeed * (speed * 2) * Time.deltaTime;
                transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, step);

                if (Vector2.Distance(transform.position, moveSpot.position) < 0.2f)
                {
                    if (waitTime <= 0)
                    {
                        step = moveSpeed * (speed * 4) * Time.deltaTime;
                        
                        moveSpot.position = new Vector2(player.position.x, player.position.y);
                        waitTime = startWaitTime;
                    }
                    else
                    {
                        step = moveSpeed * (speed * 2) * Time.deltaTime;
                        moveSpot.position = new Vector2(moveSpot.position.x, Random.Range(minY, maxY));
                        waitTime -= Time.deltaTime;
                    }
                }         
                
                if (timeBtwShots <= 1.95)
                {
                    isShooting = true;
                }
                else
                {
                    isShooting = false;
                }     
                if (timeBtwShots <= 1.55)
                {
//                    isShooting = true;

                    transform.position = Vector2.MoveTowards(transform.position, moveSpot.position, moveSpeed * speed * Time.deltaTime);

                    Vector3 dir = player.position - transform.position;

                    float angleTwo = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

                    Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angleTwo - 90));

                  //  transform.position = Vector2.MoveTowards(transform.position, player.position, 0);
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
                }
                if (lightingHit)
                {
                    timer -= 1f;
                }
            }
        }
        else if(health <= 0)
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

    
   /* private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
          //  target = null;
        }
    }*/
    public void UpdateHealth(float mod)
    {
        health = mod;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        else if (health <= 0f)
        {
            health = 0f;
            healthBar.SetHealth(health);
            Debug.Log("Enemy Death");
            isDead = true;
            lilDead = true;
            canvas.gameObject.SetActive(false);
            FinalVideo.called();
            //FinalVideo.Update();
        }
        if (hasPlayerBeenSeen)
        {
            healthBar.gameObject.SetActive(true);
        }
        if (health == 0f)
        {
            // followP.begin();
            lilDead = true;
            healthBar.gameObject.SetActive(false);
            //FinalVideo.Update();
            //videoPlayer.SetActive(true);
            //Destroy(videoPlayer, 5f);
            //StartCoroutine(waiter());
            
            //haswon = true;
        }
        AdjustBar(mod);
    }

    /*IEnumerator waiter()
    {
        //Wait for 3 seconds
        yield return new WaitForSecondsRealtime(5f);
        string name = "Menu";
        SceneManager.LoadScene(name);

    }*/

    void AdjustBar(float health)
    {
        healthBar.SetHealth(health);
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            target = other.transform;
            hasPlayerBeenSeen = true;
            Debug.Log("Here Trigger");
        }
    }
}
