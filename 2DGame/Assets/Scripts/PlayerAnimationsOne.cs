using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimationsOne : MonoBehaviour
{
    Animator anim;
    string direction = null;
    int mx;
    int my;
    public Sprite deadAnim;
    public Sprite oldSprite;
    public Sprite newSprite;
    bool hit = false;
    bool attack = false;
    bool dead = false;
    public float oldPositionX;
    public float oldPositionY;
    public Rigidbody2D rb;

    private enum MovementState { idleFrontLeft, idleFrontRight, idleBackLeft, idleBackRight, walkFrontLeft, walkFrontRight, walkBackLeft, walkBackRight, hitFrontLeft, hitFrontRight, hitBackLeft, hitBackRight, deadFrontLeft, deadFrontRight, deadBackLeft, deadBackRight, attackFrontLeft, attackFrontRight, attackBackLeft, attackBackRight, idleFrontLeftWolf, idleFrontRightWolf, idleBackLeftWolf, idleBackRightWolf, walkFrontLeftWolf, walkFrontRightWolf, walkBackLeftWolf, walkBackRightWolf, hitFrontLeftWolf, hitFrontRightWolf, hitBackLeftWolf, hitBackRightWolf, deadFrontLeftWolf, deadFrontRightWolf, deadBackLeftWolf, deadBackRightWolf, attackFrontLeftWolf, attackFrontRightWolf, attackBackLeftWolf, attackBackRightWolf, humanToWolfFrontLeft, humanToWolfFrontRight, humanToWolfBackLeft, humanToWolfBackRight, wolfToHumanFrontLeft, wolfToHumanFrontRight, wolfToHumanBackLeft, wolfToHumanBackRight }
    public GameObject player;
    public HealthBar healthBar;
    private string currentState;

    [SerializeField] private Slider timer;
    public int time;
    public int maxTime;
    bool goUp = true;
    public int wolfState;
    MovementState state = MovementState.idleFrontLeft;

    public GameObject jordan;
    [SerializeField] public GameObject Camera;
    [SerializeField] public GameObject healthUI;
    [SerializeField] public GameObject staminaUI;

    public GameObject alice;

    void Start()
    {
        timer.maxValue = time;
        // state = MovementState.idleDown;
        
        jordan.SetActive(false);
        Camera.SetActive(false);
        healthUI.SetActive(false);
        staminaUI.SetActive(false);
        if (MenuManager.player == 2)
        {
            Camera.SetActive(true);
            jordan.SetActive(true);
            healthUI.SetActive(true);
            staminaUI.SetActive(true);
        }
        
        anim = GetComponent<Animator>();
       
        MovementState stateOne = MovementState.idleFrontLeft;
        stateOne = MovementState.idleFrontLeft;
        anim.SetInteger("state", (int)stateOne);
    }

        // Update is called once per frame
    void FixedUpdate()
    {
        
        if (timer.value == 0)
        {            
            //    while(timer.value < timer.maxValue)
            //    {
            //        timer.value += Time.deltaTime;
            //    }
          //  this.gameObject.GetComponent<SpriteRenderer>().sprite = oldSprite;
            goUp = false;
            timer.value += (Time.deltaTime * 4);
            wolfState = 0;                        
        }
        if ((timer.value == timer.maxValue) && (Input.GetKeyDown(KeyCode.Mouse1)))  
        {               
            goUp = true;
            timer.value -= (Time.deltaTime / 16);
            wolfState = 1;
            //this.gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
            this.gameObject.GetComponent<PlayerHealth>().UpdateHealth(-20);
                //Reduce Player health
        }
        if ((timer.value > 0) && (timer.value < timer.maxValue))
        {           
            if (!goUp)
            {
                timer.value += (Time.deltaTime / 4);
            }
            else
            {
                timer.value -= (Time.deltaTime );
            }
        }
        MovementInput();
    }

    void MovementInput()
    {        
        mx = (int)Input.GetAxisRaw("Horizontal");
        my = (int)Input.GetAxisRaw("Vertical");

        UpdateAnimationState();
   //     oldPositionX = transform.position.x;
     //   oldPositionY = transform.position.y;
    }

    private void UpdateAnimationState()
    {
        MovementState state = MovementState.idleFrontLeft;
        PlayerHealth player = this.GetComponent<PlayerHealth>();
        PlayerMeleeAttack playerAtt = this.GetComponent<PlayerMeleeAttack>();
        float healthValue = player.health;
        //player.step = 0f;
        bool nothingPressed = false;
        dead = false;
        hit = player.playerIsHit;

        if (Input.GetKey(KeyCode.Mouse0) || (Input.GetKey(KeyCode.Mouse1)))
        {
        //    attack = true;
        }

        if(wolfState == 0)
        {
            if (false)
            {
                if (direction.Equals("leftDown"))
                {
                    Debug.Log("transform BackLeft");
                    state = MovementState.wolfToHumanBackLeft;
                    anim.SetInteger("state", (int)state);
                }
                if (direction.Equals("rightDown"))
                {
                    Debug.Log("transform BackRight");
                    state = MovementState.wolfToHumanBackRight;
                    anim.SetInteger("state", (int)state);
                }
                if (direction.Equals("leftUp"))
                {
                    Debug.Log("transform FrontLeft");
                    state = MovementState.wolfToHumanFrontLeft;
                    anim.SetInteger("state", (int)state);
                }
                if (direction.Equals("rightUp"))
                {
                    Debug.Log("transform FrontRight");
                    state = MovementState.wolfToHumanFrontRight;
                    anim.SetInteger("state", (int)state);
                }
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
                    if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                    {
                        hit = false;
                    //    UpdateAnimationState();
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
                }
                else if (direction.Equals("rightUp"))
                {
                    string stateName = "attack_frontRight";
                    state = MovementState.attackFrontRight;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("leftDown"))
                {
                    string stateName = "attack_backLeft";
                    state = MovementState.attackBackLeft;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("rightDown"))
                {
                    string stateName = "attack_backRight";
                    state = MovementState.attackBackRight;
                    anim.SetInteger("state", (int)state);                
                }
                else
                {
                    string stateName = "attack_frontLeft";
                    state = MovementState.attackFrontLeft;
                    anim.SetInteger("state", (int)state);
                }
                attack = false;
            }
        }
        else if(wolfState == 1)
        {
            if (false)
            {
                if (direction.Equals("leftDown"))
                {
                    Debug.Log("transform BackLeft");
                    state = MovementState.humanToWolfBackLeft;
                    anim.SetInteger("state", (int)state);
                }
                if (direction.Equals("rightDown"))
                {
                    Debug.Log("transform BackRight");
                    state = MovementState.humanToWolfBackRight;
                    anim.SetInteger("state", (int)state);
                }
                if (direction.Equals("leftUp"))
                {
                    Debug.Log("transform FrontLeft");
                    state = MovementState.humanToWolfFrontLeft;
                    anim.SetInteger("state", (int)state);
                }
                if (direction.Equals("rightUp"))
                {
                    Debug.Log("transform FrontRight");
                    state = MovementState.humanToWolfFrontRight;
                    anim.SetInteger("state", (int)state);
                }                
            }
            if ((!hit) && (!dead))
            {
                if ((my < 0) && (mx == 0))
                {
                    direction = "leftUpWolf";
                    state = MovementState.walkFrontLeftWolf;
                    anim.SetInteger("state", (int)state);
                }
                if ((mx > 0) && (my == 0))
                {
                    direction = "rightUpWolf";
                    state = MovementState.walkFrontRightWolf;
                    anim.SetInteger("state", (int)state);
                }
                if ((my > 0) && (mx == 0))
                {
                    direction = "rightDownWolf";
                    state = MovementState.walkBackRightWolf;
                    anim.SetInteger("state", (int)state);
                }
                if ((mx < 0) && (my == 0))
                {
                    direction = "leftDownWolf";
                    state = MovementState.walkBackLeftWolf;
                    anim.SetInteger("state", (int)state);
                }
                if ((my < 0) && (mx < 0))
                {
                    direction = "leftUpWolf";
                    state = MovementState.walkFrontLeftWolf;
                    anim.SetInteger("state", (int)state);
                }
                if ((mx > 0) && (my < 0))
                {
                    direction = "rightUpWolf";
                    state = MovementState.walkFrontRightWolf;
                    anim.SetInteger("state", (int)state);
                }
                if ((my > 0) && (mx > 0))
                {
                    direction = "rightDownWolf";
                    state = MovementState.walkBackRightWolf;
                    anim.SetInteger("state", (int)state);
                }
                if ((mx < 0) && (my > 0))
                {
                    direction = "leftDownWolf";
                    state = MovementState.walkBackLeftWolf;
                    anim.SetInteger("state", (int)state);
                }
                else if (!Input.anyKey || (Input.GetKey(KeyCode.T)))
                {
                    if (direction.Equals("leftUpWolf"))
                    {
                        state = MovementState.idleFrontLeftWolf;
                        anim.SetInteger("state", (int)state);
                    }
                    else if (direction.Equals("rightUpWolf"))
                    {
                        state = MovementState.idleFrontRightWolf;
                        anim.SetInteger("state", (int)state);
                    }
                    else if (direction.Equals("leftDownWolf"))
                    {
                        state = MovementState.idleBackLeftWolf;
                        anim.SetInteger("state", (int)state);
                    }
                    else if (direction.Equals("rightDownWolf"))
                    {
                        state = MovementState.idleBackRightWolf;
                        anim.SetInteger("state", (int)state);
                    }
                    else
                    {
                        state = MovementState.idleFrontLeftWolf;
                        anim.SetInteger("state", (int)state);
                    }
                }
            }
            else if ((player.playerIsHit) && (!dead))
            {
                if (healthBar.slider.value > 0)
                {
                    if (direction.Equals("leftUpWolf"))
                    {
                        state = MovementState.hitFrontLeftWolf;
                        anim.SetInteger("state", (int)state);
                    }
                    else if (direction.Equals("rightUpWolf"))
                    {
                        state = MovementState.hitFrontRightWolf;
                        anim.SetInteger("state", (int)state);
                    }
                    else if (direction.Equals("leftDownWolf"))
                    {
                        state = MovementState.hitBackLeftWolf;
                        anim.SetInteger("state", (int)state);
                    }
                    else if (direction.Equals("rightDownWolf"))
                    {
                        state = MovementState.hitBackRightWolf;
                        anim.SetInteger("state", (int)state);
                    }
                    else
                    {
                        state = MovementState.hitFrontLeftWolf;
                        anim.SetInteger("state", (int)state);
                    }
                    
                    if (Input.GetKey(KeyCode.Mouse0) || Input.GetKey(KeyCode.Mouse1) || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
                    {
                        hit = false;
                //    UpdateAnimationState();
                    } 
                    //direction = null;               
                }                
            }
            if ((healthBar.slider.value == 0))
            {
                dead = false;
                if (direction.Equals("leftUpWolf"))
                {
                    state = MovementState.deadFrontLeftWolf;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("rightUpWolf"))
                {
                    state = MovementState.deadFrontRightWolf;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("leftDownWolf"))
                {
                    state = MovementState.deadBackLeftWolf;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("rightDownWolf"))
                {
                    state = MovementState.deadBackRightWolf;
                    anim.SetInteger("state", (int)state);
                }
            }
            if ((playerAtt.playerIsAttacking) && (!dead))
            {
                Debug.Log("Attacked Wolf");
                if (direction.Equals("leftUpWolf"))
                {
                    string stateName = "attack_frontLeftWolf";
                    state = MovementState.attackFrontLeftWolf;
                    anim.SetInteger("state", (int)state);
                    //      anim.Play(stateName, 0, 0.0f);
                }
                else if (direction.Equals("rightUpWolf"))
                {
                    string stateName = "attack_frontRightWolf";
                    state = MovementState.attackFrontRightWolf;
                    anim.SetInteger("state", (int)state);
                    //        anim.Play(stateName, 0, 0.0f);
                }
                else if (direction.Equals("leftDownWolf"))
                {
                    string stateName = "attack_backLeftWolf";
                    state = MovementState.attackBackLeftWolf;
                    anim.SetInteger("state", (int)state);
                    //           anim.Play(stateName, 0, 0.0f);
                }
                else if (direction.Equals("rightDownWolf"))
                {
                    string stateName = "attack_backRightWolf";
                    state = MovementState.attackBackRightWolf;
                    anim.SetInteger("state", (int)state);
                    //        anim.Play(stateName, 0, 0.0f);
                }
                else
                {
                    string stateName = "attack_frontLeftWolf";
                    state = MovementState.attackFrontLeftWolf;
                    anim.SetInteger("state", (int)state);
                    //    anim.Play(stateName, 0, 0.0f);
                }
                attack = false;
            }
        }
        // Debug.Log(state);
    }
    
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("MinorEnemy"))
        {
            Debug.Log("minor hit");
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
        attack = false;
    }
}
