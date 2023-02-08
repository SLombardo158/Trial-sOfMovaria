using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAnimationsTwo : MonoBehaviour
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

    private enum MovementState { idleFrontLeft, idleFrontRight, idleBackLeft, idleBackRight, walkFrontLeft, walkFrontRight, walkBackLeft, walkBackRight, hitFrontLeft, hitFrontRight, hitBackLeft, hitBackRight, deadFrontLeft, deadFrontRight, deadBackLeft, deadBackRight, attackFrontLeftShort, attackFrontRightShort, attackBackLeftShort, attackBackRightShort, attackFrontLeftLong, attackFrontRightLong, attackBackLeftLong, attackBackRightLong }
    public GameObject player;
    public HealthBar healthBar;
    private string currentState;

    [SerializeField] private Slider timer;
    public int time;
    public int maxTime;
    MovementState state = MovementState.idleFrontLeft;

    public GameObject julia;
    [SerializeField] public GameObject Camera;
    [SerializeField] public GameObject healthUI;

    public GameObject alice;

    void Start()
    {
        julia.SetActive(false);
        Camera.SetActive(false);
        healthUI.SetActive(false);

        if (MenuManager.player == 4)
        {
            Camera.SetActive(true);
            julia.SetActive(true);
            healthUI.SetActive(true);       
        }

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
        PlayerMeleeAttackJulia playerAtt = this.GetComponent<PlayerMeleeAttackJulia>();
        PlayerAttackTwo playerRangeAtt = this.GetComponent<PlayerAttackTwo>();
        float healthValue = player.health;
        //player.step = 0f;
        bool nothingPressed = false;
        dead = false;
        hit = player.playerIsHit;

        //Debug.Log(state);

        if (Input.GetKey(KeyCode.Mouse0) || (Input.GetKey(KeyCode.Mouse1)))
        {
            //    attack = true;
        }

        if ((!hit) && (!dead))
        {
            if ((my < 0) && (mx == 0))
            {                
                direction = "leftUp";
                state = MovementState.walkFrontLeft;
                anim.SetInteger("state", (int)state);
            }
            if ((mx > 0) && (my == 0))
            {                
                direction = "rightUp";
                state = MovementState.walkFrontRight;
                anim.SetInteger("state", (int)state);
            }
            if ((my > 0) && (mx == 0))
            {                
                direction = "rightDown";
                state = MovementState.walkBackRight;
                anim.SetInteger("state", (int)state);
            }
            if ((mx < 0) && (my == 0))
            {
                direction = "leftDown";
                state = MovementState.walkBackLeft;
                anim.SetInteger("state", (int)state);
            }
            if ((my < 0) && (mx < 0))
            {
                direction = "leftUp";
                state = MovementState.walkFrontLeft;
                anim.SetInteger("state", (int)state);
            }
            if ((mx > 0) && (my < 0))
            {
                direction = "rightUp";
                state = MovementState.walkFrontRight;
                anim.SetInteger("state", (int)state);
            }
            if ((my > 0) && (mx > 0))
            {                
                direction = "rightDown";
                state = MovementState.walkBackRight;
                anim.SetInteger("state", (int)state);
            }
            if ((mx < 0) && (my > 0))
            {                
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
            if (Input.GetKey(KeyCode.Mouse0))
            {
                if (direction.Equals("leftUp"))
                {
                    string stateName = "attack_frontLeftShort";
                    state = MovementState.attackFrontLeftShort;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("rightUp"))
                {
                    string stateName = "attack_frontRightShort";
                    state = MovementState.attackFrontRightShort;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("leftDown"))
                {
                    string stateName = "attack_backLeftShort";
                    state = MovementState.attackBackLeftShort;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("rightDown"))
                {
                    string stateName = "attack_backRightShort";
                    state = MovementState.attackBackRightShort;
                    anim.SetInteger("state", (int)state);
                }
                else
                {
                    string stateName = "attack_frontLeftShort";
                    state = MovementState.attackFrontLeftShort;
                    anim.SetInteger("state", (int)state);
                }
            }
        }
        if ((playerRangeAtt.playerIsAttacking) && (!dead))
        {           
            if (Input.GetKey(KeyCode.Mouse1))
            {
                if (direction.Equals("leftUp"))
                {
                    string stateName = "attack_frontLeftLong";
                    state = MovementState.attackFrontLeftLong;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("rightUp"))
                {
                    string stateName = "attack_frontRightLong";
                    state = MovementState.attackFrontRightLong;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("leftDown"))
                {
                    string stateName = "attack_backLeft";
                    state = MovementState.attackBackLeftLong;
                    anim.SetInteger("state", (int)state);
                }
                else if (direction.Equals("rightDown"))
                {
                    string stateName = "attack_backRight";
                    state = MovementState.attackBackRightLong;
                    anim.SetInteger("state", (int)state);
                }
                else
                {
                    string stateName = "attack_frontLeft";
                    state = MovementState.attackFrontLeftLong;
                    anim.SetInteger("state", (int)state);
                }
            }
            attack = false;            
        }
        Debug.Log(state);
    }

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
        attack = false;
    }
}
