using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMinorAnimations : MonoBehaviour
{
    private Animator anim;
    string direction = null;
    int mx;
    int my;
    public Sprite deadAnim;
    bool hit = false;
    bool attack = false;
    private enum MovementState { idleFrontLeft, idleFrontRight, idleBackLeft, idleBackRight, walkFrontLeft, walkFrontRight, walkBackLeft, walkBackRight, hitFrontLeft, hitFrontRight, hitBackLeft, hitBackRight, deadFrontLeft, deadFrontRight, deadBackLeft, deadBackRight, attackFrontLeft, attackFrontRight, attackBackLeft, attackBackRight }
    public GameObject enemy;
    public EnemyFollowWhenNear health;
    private string currentState;
    private Vector3 moveDirection;
    public Rigidbody2D rb;
    public float oldPositionX;
    public float oldPositionY;
    public bool movingRight = false;
    public bool movingLeft = false;

    void Start()
    {
        // state = MovementState.idleDown;
        rb = GetComponent<Rigidbody2D>();
                
        anim = GetComponent<Animator>();
        oldPositionX = transform.position.x;
        oldPositionY = transform.position.y;

        MovementState stateOne = MovementState.idleFrontLeft;
        stateOne = MovementState.idleBackLeft;
        anim.SetInteger("state", (int)stateOne);

        UpdateAnimationState();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MovementInput();
    }

    void MovementInput()
    {
        //anim.SetFloat("xMov", moveDirection.normalized.x);
        // anim.SetFloat("yMov", moveDirection.normalized.y);
        //    mx = (int)anim.SetFloat();
        //    my = (int)anim.SetFloat(direction.);

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
        EnemyFollowWhenNear enemy = gameObject.GetComponent<EnemyFollowWhenNear>();
        float healthValue = enemy.health;
        enemy.step = 0f;
        bool nothingPressed = false;
        bool dead = false;
        hit = enemy.isHit; 

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
            }
        }
        if (enemy.isHit && (!dead))
        {
            if ((healthValue > 0))
            {
                if (direction.Equals("leftUp"))
                {
                    state = MovementState.hitFrontLeft;
                    anim.SetInteger("state", (int)state);
                  //  anim.Play("hitFrontLeft", 0, -1f);
                }
                else if (direction.Equals("rightUp"))
                {
                    state = MovementState.hitFrontRight;
                    anim.SetInteger("state", (int)state);
               //     anim.Play("hitFrontRight", 0, -1f);
                }
                else if (direction.Equals("leftDown"))
                {
                    state = MovementState.hitBackLeft;
                    anim.SetInteger("state", (int)state);
                 //   anim.Play("hitBackLeft", 0, -1f);
                }
                else if (direction.Equals("rightDown"))
                {
                    state = MovementState.hitBackRight;
                    anim.SetInteger("state", (int)state);
                   // anim.Play("hitBackRight", 0, -1f);
                }
                    
                //                direction = null;
                // hit = false;
                                              
            }
        
        if ((healthValue == 0) || enemy.health <= 0)
        {
            dead = true;
            if (direction.Equals("leftUp"))
            {
                state = MovementState.deadFrontLeft;
                anim.SetInteger("state", (int)state);
                Destroy(gameObject, .2f);
            }
            else if (direction.Equals("rightUp"))
            {
                state = MovementState.deadFrontRight;
                anim.SetInteger("state", (int)state);
                Destroy(gameObject, .2f);
            }
            else if (direction.Equals("leftDown"))
            {
                state = MovementState.deadBackLeft;
                anim.SetInteger("state", (int)state);
                Destroy(gameObject, .2f);
            }
            else if (direction.Equals("rightDown"))
            {
                state = MovementState.deadBackRight;
                anim.SetInteger("state", (int)state);
                Destroy(gameObject, .2f);
            }
        }
        }
        if (attack && (!dead))
        {
            if (direction.Equals("leftUp"))
            {                
                Debug.Log("FrontLeft");
                state = MovementState.attackFrontLeft;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("rightUp"))
            {
                Debug.Log("FrontRight");
                state = MovementState.attackFrontRight;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("leftDown"))
            {
                Debug.Log("BackLeft");
                state = MovementState.attackBackLeft;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("rightDown"))
            {
                Debug.Log("BackRight");
                state = MovementState.attackBackRight;
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
        //Debug.Log("not hit");
        if (collision.gameObject.CompareTag("Projectile"))
        {
            hit = false;
            Debug.Log("projectile finish");
        }
    }
}
