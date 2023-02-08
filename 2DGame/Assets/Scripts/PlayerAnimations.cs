using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator anim;
    string direction = null;
    int mx;
    int my;
    public Sprite deadAnim;
    bool hit = false;
    bool dead = false;
    bool attack = false;

    private enum MovementState { idleFrontLeft, idleFrontRight, idleBackLeft, idleBackRight, walkFrontLeft, walkFrontRight, walkBackLeft, walkBackRight, hitFrontLeft, hitFrontRight, hitBackLeft, hitBackRight, deadFrontLeft, deadFrontRight, deadBackLeft, deadBackRight, attackFrontLeft, attackFrontRight, attackBackLeft, attackBackRight }
    public GameObject player;
    public HealthBar healthBar;
    private string currentState;
    public GameObject alice;
    public GameObject healthUI;
    bool alreadyHit = false;

    [SerializeField] public GameObject Camera;

    void Start()
    {        
        //////edit later////////
        alice.SetActive(false);
        Camera.SetActive(false);
        healthUI.SetActive(false);

        if (MenuManager.player == 1)
        {
            alice.SetActive(true);
            Camera.SetActive(true);
            healthUI.SetActive(true);
        }
                
        // state = MovementState.idleDown;

        anim = GetComponent<Animator>();
        
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
        mx = (int)Input.GetAxisRaw("Horizontal");
        my = (int)Input.GetAxisRaw("Vertical");

        UpdateAnimationState();
    }

    private void UpdateAnimationState()
    {
        MovementState state = MovementState.idleFrontLeft;
        PlayerHealth player = this.GetComponent<PlayerHealth>();
        PlayerAttack playerAtt = this.GetComponent<PlayerAttack>();
        float healthValue = player.health;
        //player.step = 0f;
        bool nothingPressed = false;
        dead = false;
        hit = player.playerIsHit;
        
        if(Input.GetKey(KeyCode.Mouse0) || (Input.GetKey(KeyCode.Mouse1)))
        {
         //   attack = true;
        }

        if ((!hit) && (!dead))
        {            
            if ((my < 0) && (mx == 0))
            {
                Debug.Log("FrontLeft");
                direction = "leftUp";
                state = MovementState.walkFrontLeft;
                anim.SetInteger("state", (int)state);
            }
            if ((mx > 0) && (my == 0))
            {
                Debug.Log("FrontRight");
                direction = "rightUp";
                state = MovementState.walkFrontRight;
                anim.SetInteger("state", (int)state);
            }
            if ((my > 0) && (mx == 0))
            {
                Debug.Log("BackRight");
                direction = "rightDown";
                state = MovementState.walkBackRight;
                anim.SetInteger("state", (int)state);
            }    
            if ((mx < 0) && (my == 0))
            {
                Debug.Log("BackLeft");
                direction = "leftDown";
                state = MovementState.walkBackLeft;
                anim.SetInteger("state", (int)state);
            }
            if ((my < 0) && (mx < 0))
            {
                Debug.Log("FrontLeft");
                direction = "leftUp";
                state = MovementState.walkFrontLeft;
                anim.SetInteger("state", (int)state);
            }
            if ((mx > 0) && (my < 0))
            {
                Debug.Log("FrontRight");
                direction = "rightUp";
                state = MovementState.walkFrontRight;
                anim.SetInteger("state", (int)state);
            }
            if ((my > 0) && (mx > 0))
            {
                Debug.Log("BackRight");
                direction = "rightDown";
                state = MovementState.walkBackRight;
                anim.SetInteger("state", (int)state);
            }
            if ((mx < 0) && (my > 0))
            {
                Debug.Log("BackLeft");
                direction = "leftDown";
                state = MovementState.walkBackLeft;
                anim.SetInteger("state", (int)state);
            }
            else if (!Input.anyKey)
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
        else if ((player.playerIsHit) && (!dead))
        {            
            if (healthBar.slider.value > 0)
            {
                if (direction.Equals("leftUp"))
                {
                    state = MovementState.hitFrontLeft;
                    anim.SetInteger("state", (int)state);
                    alreadyHit = true;
                }
                else if (direction.Equals("rightUp"))
                {
                    state = MovementState.hitFrontRight;
                    anim.SetInteger("state", (int)state);
                    alreadyHit = true;
                }
                else if (direction.Equals("leftDown"))
                {
                    state = MovementState.hitBackLeft;
                    anim.SetInteger("state", (int)state);
                    alreadyHit = true;
                }
                else if (direction.Equals("rightDown"))
                {
                    state = MovementState.hitBackRight;
                    anim.SetInteger("state", (int)state);
                    alreadyHit = true;
                }                
                else
                {
                    state = MovementState.hitFrontLeft;
                    anim.SetInteger("state", (int)state);
                    alreadyHit = true;
                }
                if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                {
                    hit = false;
              //      UpdateAnimationState();
                }               
            }
        }
        if ((healthBar.slider.value == 0))
        {
            dead = false;
            if (direction.Equals("leftUp"))
            {
                state = MovementState.deadFrontLeft;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("rightUp"))
            {
                state = MovementState.deadFrontRight;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("leftDown"))
            {
                state = MovementState.deadBackLeft;
                anim.SetInteger("state", (int)state);
            }
            else if (direction.Equals("rightDown"))
            {
                state = MovementState.deadBackRight;
                anim.SetInteger("state", (int)state);
            }
        }

        if ((playerAtt.playerIsAttacking) && (!dead))
        {
            Debug.Log("Attacked");
            if (direction.Equals("leftUp"))
            {
                string stateName = "attack_frontLeft";
                state = MovementState.attackFrontLeft;
                anim.SetInteger("state", (int)state);
          //      anim.Play(stateName, 0, 0.0f);
            }
            else if (direction.Equals("rightUp"))
            {
                string stateName = "attack_frontRight";
                state = MovementState.attackFrontRight;
                anim.SetInteger("state", (int)state);
        //        anim.Play(stateName, 0, 0.0f);
            }
            else if (direction.Equals("leftDown"))
            {
                string stateName = "attack_backLeft";
                state = MovementState.attackBackLeft;
                anim.SetInteger("state", (int)state);
     //           anim.Play(stateName, 0, 0.0f);
            }
            else if (direction.Equals("rightDown"))
            {
                string stateName = "attack_backRight";
                state = MovementState.attackBackRight;
                anim.SetInteger("state", (int)state);
        //        anim.Play(stateName, 0, 0.0f);
            }
            else
            {
                string stateName = "attack_frontLeft";
                state = MovementState.attackFrontLeft;
                anim.SetInteger("state", (int)state);
            //    anim.Play(stateName, 0, 0.0f);
            }
            attack = false;
        }
       // Debug.Log(state);
    }
    /*private void UpdateAnimationState()
    {
        MovementState state = MovementState.idleLeft;

        bool nothingPressed = false;
        if(!hit)
        { 
            if (mx < 0)
            {
                direction = "left";
                state = MovementState.walkLeft;
                anim.SetInteger("state", (int)state);
            }
            else if (mx > 0)
            {
                state = MovementState.walkRight;
                direction = "right";
                anim.SetInteger("state", (int)state);
            }
            else if (my < 0)
            {
                direction = "down";
                state = MovementState.walkBackward;
                anim.SetInteger("state", (int)state);
            }
            else if (my > 0)
            {
                direction = "up";
                state = MovementState.walkForward;
                anim.SetInteger("state", (int)state);
            }
            else if (!Input.anyKey)
            {
                if (direction.Equals("left"))
                {
                    state = MovementState.idleLeft;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("right"))
                {
                    state = MovementState.idleRight;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("up"))
                {
                    state = MovementState.idleFront;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("down"))
                {
                    state = MovementState.idleBack;
                    anim.SetInteger("state", (int)state);
                }
                else
                {
                    state = MovementState.idleLeft;
                    anim.SetInteger("state", (int)state);
                }
            }
        }
        else if (hit)
        {
            if (healthBar.slider.value > 0)
            {
                if (direction.Equals("left"))
                {
                    Debug.Log("hit left");
                    state = MovementState.hitLeft;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("right"))
                {
                    Debug.Log("hit right");
                    state = MovementState.hitRight;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("up"))
                {
                    Debug.Log("hit up");
                    state = MovementState.hitForward;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("down"))
                {
                    Debug.Log("hit down");
                    state = MovementState.hitBackward;
                    anim.SetInteger("state", (int)state);
                }
            }
            else if (healthBar.slider.value == 0)
            {
                Debug.Log("Death Animations");
                player.gameObject.GetComponent<SpriteRenderer>().sprite = deadAnim;
            }
            hit = false;
            direction = "none";
        }
    //    if(!Input.anyKey)
    //    {
      //      nothingPressed = true;
        //}

      //  anim.SetInteger("state", (int)state);
    }
    */
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MinorEnemy"))
        {
            hit = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hit = true;
        }
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            hit = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MinorEnemy"))
        {
            hit = false;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            hit = false;
        }
        if (collision.gameObject.CompareTag("EnemyProjectile"))
        {
            hit = false;
        }
    }

  /*  private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("MinorEnemy"))
        {
            hit = false;
        }
        if (other.gameObject.CompareTag("Enemy"))
        {
            hit = false;
        }
        if (other.gameObject.CompareTag("EnemyProjectile"))
        {
            hit = false;
        }
    }*/
}
