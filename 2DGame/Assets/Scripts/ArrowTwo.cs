using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTwo : MonoBehaviour
{
    [HideInInspector] public float ArrowVelocityTwo;
    [HideInInspector] public float ArrowDamageTwo;
    [SerializeField] Rigidbody2D rb;
    private Transform body;
    int randomNum = 0;
    public float timer;

    private void Start()
    {
      //  Destroy(gameObject, 4f);
    }

    private void FixedUpdate()
    {
        rb.velocity = transform.up * ArrowVelocityTwo;
        randomNum = Random.Range(1, 10);
        timer = 10f;
        if((timer == 0) && (randomNum == 3))
            {
                Debug.Log("Light hit normal");
                Enemy enemy = gameObject.GetComponent<Enemy>();
                enemy.moveSpeed = 1f;                
                EnemyFollowWhenNear enemyTwo = gameObject.GetComponent<EnemyFollowWhenNear>();
                enemyTwo.moveSpeed = 1f;                 
                RangedEnemy enemyThree = gameObject.GetComponent<RangedEnemy>();
                enemyThree.moveSpeed = 1f;
            }
        timer -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
       
        if(randomNum <= 3)
        {
            Debug.Log("Lighting hit slow");
            if (other.gameObject.tag == "Enemy")
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.moveSpeed = .25f;
                enemy.TakeDamage(ArrowDamageTwo);
            }
            else if (other.gameObject.tag == "MinorEnemy")
            {
                EnemyFollowWhenNear enemyTwo = other.gameObject.GetComponent<EnemyFollowWhenNear>();
                enemyTwo.moveSpeed = .25f;
                enemyTwo.TakeDamage(ArrowDamageTwo);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                RangedEnemy enemyThree = other.gameObject.GetComponent<RangedEnemy>();
                enemyThree.moveSpeed = .25f;
                enemyThree.TakeDamage(ArrowDamageTwo);
            }
            else if (other.gameObject.tag == "EnemyBoss")
            {                
                FinalBossEnemy enemyFour = other.gameObject.GetComponent<FinalBossEnemy>();
                enemyFour.moveSpeed = .25f;
                enemyFour.TakeDamage(ArrowDamageTwo);
            }

        }
        else
        {
            if (other.gameObject.tag == "Enemy")
            {
                Enemy enemy = other.gameObject.GetComponent<Enemy>();
                enemy.moveSpeed = 1f;
                enemy.TakeDamage(ArrowDamageTwo);
            }
            else if (other.gameObject.tag == "MinorEnemy")
            {
                EnemyFollowWhenNear enemyTwo = other.gameObject.GetComponent<EnemyFollowWhenNear>();
                enemyTwo.moveSpeed = 1f;
                enemyTwo.TakeDamage(ArrowDamageTwo);
            }
            else if (other.gameObject.tag == "EnemyRanged")
            {
                RangedEnemy enemyThree = other.gameObject.GetComponent<RangedEnemy>();
                enemyThree.moveSpeed = 1f;
                enemyThree.TakeDamage(ArrowDamageTwo);
            }
            else if (other.gameObject.tag == "EnemyBoss")
            {
                FinalBossEnemy enemyFour = other.gameObject.GetComponent<FinalBossEnemy>();
                enemyFour.moveSpeed = 1f;
                enemyFour.TakeDamage(ArrowDamageTwo);
            }
        }
        
        Destroy(gameObject);
    }
}
