using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMeleeAttackTwo : MonoBehaviour
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
                        if (enemy.GetType().ToString() == "BoxCollider2D")
                        {
                            enemy.GetComponent<RangedEnemy>().TakeDamage(meleeDamage * (.5f));
                        }
                    }
                }
                timeBtwAttack = startTimeBtwAttack;
            }
            else
            {
                timeBtwAttack -= Time.deltaTime;
            }
        }
        if (form.wolfState == 1)
        {
            if (timeBtwAttack <= 1)
            {
                if (Input.GetKey(KeyCode.Mouse0))
                {                    
                    Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(attackPos.position, attackRange, whatIsEnemies);
                    foreach (Collider2D enemy in enemiesToDamage)
                    {
                        if (enemy.GetType().ToString() == "BoxCollider2D")
                        {
                            enemy.GetComponent<RangedEnemy>().TakeDamage(meleeDamage * (1.5f));
                        }
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
            playerIsAttacking = true;
            timerAttack = 10f;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(attackPos.position, attackRange);
    }
}