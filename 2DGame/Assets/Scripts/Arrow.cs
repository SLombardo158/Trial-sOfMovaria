using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [HideInInspector] public float ArrowVelocity;
    [HideInInspector] public float ArrowDamage;
    [SerializeField] Rigidbody2D rb;
    private Transform body;

    private void Start()
    { 
        Destroy(gameObject, 4f);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * ArrowVelocity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(ArrowDamage);
        }
        else if (other.gameObject.tag == "MinorEnemy")
        {
            EnemyFollowWhenNear enemyTwo = other.gameObject.GetComponent<EnemyFollowWhenNear>();
            enemyTwo.TakeDamage(ArrowDamage);
        }
        else if (other.gameObject.tag == "EnemyRanged")
        {
            RangedEnemy enemyThree = other.gameObject.GetComponent<RangedEnemy>();
            enemyThree.TakeDamage(ArrowDamage);
        }
        else if (other.gameObject.tag == "EnemyBoss")
        {
            FinalBossEnemy enemyFour = other.gameObject.GetComponent<FinalBossEnemy>();
            enemyFour.TakeDamage(ArrowDamage);
        }
        else if (other.gameObject.tag == "EnemyRanged")
        {
            RangedEnemyBat enemyFive = other.gameObject.GetComponent<RangedEnemyBat>();
            enemyFive.TakeDamage(ArrowDamage);
        }

        Destroy(gameObject);
    }
}
