using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemyAnimations : MonoBehaviour
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
    public RangedEnemy health;
    private string currentState;
    private Vector3 moveDirection;
    public Rigidbody2D rb;
    public float oldPositionX;
    public float oldPositionY;
    public bool movingRight = false;
    public bool movingLeft = false;
    public bool isShootingCheck;
    public bool isHit;

    void Start()
    {
        // state = MovementState.idleDown;
        rb = GetComponent<Rigidbody2D>();

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
        RangedEnemy enemy = gameObject.GetComponent<RangedEnemy>();
        float healthValue = enemy.health;
        isShootingCheck = enemy.GetComponent<RangedEnemy>().isShooting;
        isHit = enemy.GetComponent<RangedEnemy>().isHit;
        isHit = enemy.GetComponent<RangedEnemy>().isHit;

        bool nothingPressed = false;
        bool dead = false;

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
        else if ((enemy.isHit == true) && (!dead))
        {
            if ((healthValue > 0))
            {
                if (direction.Equals("leftUp"))
                {
                    state = MovementState.hitFrontLeft;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("rightUp"))
                {
                    state = MovementState.hitFrontRight;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("leftDown"))
                {
                    state = MovementState.hitBackLeft;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("rightDown"))
                {
                    state = MovementState.hitBackRight;
                    anim.SetInteger("state", (int)state);
                }
                direction = null;
            }
        }
        if ((healthValue <= 0) || enemy.health <= 0)
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

        if ((!dead) && (enemy.isShooting == true))
        {
            if (direction.Equals("leftDown"))
            {                
                state = MovementState.attackBackLeft;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("rightDown"))
            {               
                state = MovementState.attackBackRight;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("leftUp"))
            {                
                state = MovementState.attackFrontLeft;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("rightUp"))
            {               
                state = MovementState.attackFrontRight;
                anim.SetInteger("state", (int)state);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hit = true;
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
    }
}
