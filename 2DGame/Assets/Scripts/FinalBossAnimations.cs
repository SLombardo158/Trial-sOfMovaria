using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class FinalBossAnimations : MonoBehaviour
{
    private Animator anim;
    string direction = null;
    int mx;
    int my;
    // public Sprite deadAnim;
    bool hit = false;
    bool attack = false;
    bool alreadyPlayed = false;

    private enum MovementState { idleFrontLeft, idleFrontRight, idleBackLeft, idleBackRight, walkFrontLeft, walkFrontRight, walkBackLeft, walkBackRight, hitFrontLeft, hitFrontRight, hitBackLeft, hitBackRight, deadFrontLeft, deadFrontRight, deadBackLeft, deadBackRight, attackFrontLeft, attackFrontRight, attackBackLeft, attackBackRight, spawnFrontLeft, spawnFrontRight, spawnBackLeft, spawnBackRight }
    public GameObject enemy;
    public EnemyHealthBar healthBar;
    private string currentState;
    private Vector3 moveDirection;
    public Rigidbody2D rb;
    public float oldPositionX;
    public float oldPositionY;
    public bool movingRight = false;
    public bool movingLeft = false;

    public bool isShootingCheck;
    public bool isSpawning;
    public GameObject spawner;
    void Start()
    {
        anim = GetComponent<Animator>();
        oldPositionX = transform.position.x;
        oldPositionY = transform.position.y;

        MovementState stateOne = MovementState.idleFrontLeft;
        stateOne = MovementState.idleFrontLeft;
        anim.SetInteger("state", (int)stateOne);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementInput();
    }

    void MovementInput()
    {
        if (transform.position.x > oldPositionX)
        {
            mx = 1;
        }
        else if (transform.position.x < oldPositionX)
        {
            mx = -1;
        }
        if (transform.position.y > oldPositionY)
        {
            my = 1;
        }
        else if (transform.position.y < oldPositionY)
        {
            my = -1;
        }
        if (transform.position.x == oldPositionX)
        {
            mx = 0;
        }
        if (transform.position.y == oldPositionY)
        {
            my = 0;
        }

        UpdateAnimationState();

        oldPositionX = transform.position.x;
        oldPositionY = transform.position.y;
    }

    private void UpdateAnimationState()
    {
        MovementState state = MovementState.idleFrontLeft;
        FinalBossEnemy enemy = gameObject.GetComponent<FinalBossEnemy>();
        isShootingCheck = enemy.GetComponent<FinalBossEnemy>().isShooting;
       // spawn spawnAnim = gameObject.GetComponent<spawn>().spawning;
        float healthValue = enemy.health;
        enemy.step = 0f;
        bool nothingPressed = false;
        bool dead = false;
        hit = enemy.isHit;
        attack = isShootingCheck;
        isSpawning = enemy.GetComponent<FinalBossEnemy>().spawning;

        Debug.Log("state: " + state);

        if ((!hit) && (!dead))
        {
            if ((mx < 0) && (my > 0))
            {
                direction = "leftDown";
                state = MovementState.walkBackLeft;
                anim.SetInteger("state", (int)state);
            }
            else if ((mx > 0) && (my > 0))
            {
                direction = "rightDown";
                state = MovementState.walkBackRight;
                anim.SetInteger("state", (int)state);
            }
            else if ((mx < 0) && (my < 0))
            {
                direction = "leftUp";
                state = MovementState.walkFrontLeft;
                anim.SetInteger("state", (int)state);
            }
            else if ((mx > 0) && (my < 0))
            {
                direction = "rightUp";
                state = MovementState.walkFrontRight;
                anim.SetInteger("state", (int)state);
            }
            else if ((my == 0) && (mx == 0))
            {
                if (direction.Equals("leftUp"))
                {
                    state = MovementState.idleFrontLeft;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("rightUp"))
                {
                    state = MovementState.idleFrontRight;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("leftDown"))
                {
                    state = MovementState.idleBackLeft;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("rightDown"))
                {
                    state = MovementState.idleBackRight;
                    anim.SetInteger("state", (int)state);
                }
                else
                {
                    state = MovementState.idleFrontLeft;
                    anim.SetInteger("state", (int)state);
                }
            }
        }
        else if (enemy.isHit && (!dead))
        {
            if ((healthValue > 0))
            {
                if (direction.Equals("leftUp"))
                {
                    state = MovementState.hitFrontLeft;
                    anim.SetInteger("state", (int)state);
                    anim.Play("hitFrontLeft", 0, -1f);
                }
                else if (direction.Equals("rightUp"))
                {
                    state = MovementState.hitFrontRight;
                    anim.SetInteger("state", (int)state);
                    anim.Play("hitFrontRight", 0, -1f);
                }
                else if (direction.Equals("leftDown"))
                {
                    state = MovementState.hitBackLeft;
                    anim.SetInteger("state", (int)state);
                    anim.Play("hitLeftRight", 0, -1f);
                }
                else if (direction.Equals("rightDown"))
                {
                    state = MovementState.hitBackRight;
                    anim.SetInteger("state", (int)state);
                    anim.Play("hitBackRight", 0, -1f);
                }
                direction = null;
            }
        }
        if ((healthValue == 0) || enemy.health == 0)
        {
            dead = true;
            if (direction.Equals("leftUp"))
            {
                Debug.Log("Dead Up Left");
                
                state = MovementState.deadFrontLeft;
                anim.SetInteger("state", (int)state);
                Debug.Log("StateOne: " + state);
                //Destroy(gameObject, 1f);
            }
            else if (direction.Equals("rightUp"))
            {
                Debug.Log("Dead Up Right");
                
                state = MovementState.deadFrontRight;
                anim.SetInteger("state", (int)state);
                Debug.Log("StateOne: " + state);
             //   Destroy(gameObject, 1f);

            }
            else if (direction.Equals("leftDown"))
            {
                Debug.Log("Dead Down Left");
                
                state = MovementState.deadBackLeft;
                anim.SetInteger("state", (int)state);
                Debug.Log("StateOne: " + state);
           //     Destroy(gameObject, 1f);

            }
            else if (direction.Equals("rightDown"))
            {
                Debug.Log("Dead Down Right");
                state = MovementState.deadBackRight;
                anim.SetInteger("state", (int)state);
                Debug.Log("StateOne: " + state);

            //    Destroy(gameObject, 1f);
            }
            else
            {
                Debug.Log("Dead Up Left");
                
                state = MovementState.deadFrontLeft;
                anim.SetInteger("state", (int)state);
                Debug.Log("StateOne: " + state);
             //   Destroy(gameObject, 1f);

            }
        }
        if ((enemy.isShooting == true) && (!dead))
        {
            if (direction.Equals("leftUp"))
            {                
                state = MovementState.attackFrontLeft;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("rightUp"))
            {                
                state = MovementState.attackFrontRight;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("leftDown"))
            {                
                state = MovementState.attackBackLeft;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("rightDown"))
            {                
                state = MovementState.attackBackRight;
                anim.SetInteger("state", (int)state);
            }
            else
            {
                state = MovementState.attackFrontLeft;
                anim.SetInteger("state", (int)state);
            }
        }
        if(isSpawning)
        {
            if (direction.Equals("leftUp"))
            {                
                state = MovementState.attackFrontLeft;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("rightUp"))
            {                
                state = MovementState.attackFrontRight;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("leftDown"))
            {                
                state = MovementState.attackBackLeft;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("rightDown"))
            {                
                state = MovementState.attackBackRight;
                anim.SetInteger("state", (int)state);
            }
            else
            {
                state = MovementState.attackFrontLeft;
                anim.SetInteger("state", (int)state);
            }
           
        }    
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            attack = true;
            Debug.Log("player attacked");
        }
        if (collision.gameObject.CompareTag("Projectile"))
        {
            hit = true;
            Debug.Log("projectile hit");
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        hit = false;
        attack = false;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        hit = false;
        attack = false;
    }
}
