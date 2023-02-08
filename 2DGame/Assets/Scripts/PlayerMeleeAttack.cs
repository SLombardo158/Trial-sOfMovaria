using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttack : MonoBehaviour
{
    public float meleeDamage;
    private float timeBtwAttack;
    public float startTimeBtwAttack;

    public Transform attackPos;
    public LayerMask whatIsEnemies;
    public float attackRange;

    public bool playerIsAttacking;
    float timerAttack = 10f;

    // Update is called once per frame
    void Update()
    {
        PlayerAnimationsOne form = this.GetComponent<PlayerAnimationsOne>();

        if (form.wolfState == 0)
        {
            if (timeBtwAttack <= 1)
            {
                if (Input.GetKey(KeyCode.Mouse0) )
                {                   
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                    foreach (Collider2D enemy in enemiesToDamage)
                    {
                        //Debug.Log(enemy.GetType().ToString());
                        //if (enemy.GetType().ToString() == "UnityEngine.BoxCollider2D")
                        //{
                        //    enemy.GetComponent<EnemyFollowWhenNear>().TakeDamage(meleeDamage); //* (.5f));
                        //}
                    }
                }
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
        if(form.wolfState == 1)
        {
            if (timeBtwAttack <= 1)
            {
                if (Input.GetKey(KeyCode.Mouse0) )
                {                    
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                    foreach (Collider2D enemy in enemiesToDamage)
                    {
                        //if (enemy.GetType().ToString() == "UnityEngine.BoxCollider2D")
                        //{
                            //enemy.GetComponent<EnemyFollowWhenNear>().TakeDamage(meleeDamage); // * (1.5f));
                        //}
                    }
                }
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
    }

    private void FixedUpdate()
    {
        if (playerIsAttacking)
        {
            if (timerAttack <= 1)
            {
                playerIsAttacking = false;
            }
            else
            {
                timerAttack -= 1f;
            }
        }
        if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
        {
            Debug.Log("Mouse clicked");
            playerIsAttacking = true;
            timerAttack = 10f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }

    private void OnCollisionStay2D(Collision2D other)
    {
        Debug.Log("hit here");
        if (Input.GetKey(KeyCode.Mouse0))
        {
            if (other.gameObject.tag == "Enemy")
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.TakeDamage(5);
            }
            else if (other.gameObject.tag == "MinorEnemy")
            {
                EnemyFollowWhenNear enemyTwo = other.gameObject.GetComponent<EnemyFollowWhenNear>();
                enemyTwo.TakeDamage(5);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                RangedEnemy enemyThree = other.gameObject.GetComponent<RangedEnemy>();
                enemyThree.TakeDamage(5);
            }
            else if (other.gameObject.tag == "EnemyBoss")
            {
                FinalBossEnemy enemyFour = other.gameObject.GetComponent<FinalBossEnemy>();
                enemyFour.TakeDamage(5);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                RangedEnemyBat enemyFive = other.gameObject.GetComponent<RangedEnemyBat>();
                enemyFive.TakeDamage(5);
            }
        }
    }
}