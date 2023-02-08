using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttackTwo : MonoBehaviour
{
    [SerializeField] GameObject ArrowPrefab;
    [SerializeField] GameObject ArrowPrefabBolt;
    [SerializeField] SpriteRenderer ArrowGFX;
    [SerializeField] Slider BowPowerSlider;
    [SerializeField] Transform Bow;

    [Range(0, 10)]
    [SerializeField] float BowPower;

    [Range(0, 3)]
    [SerializeField] float MaxBowCharge;

    float BowCharge;

    bool CanFire = true;

    public bool playerIsAttacking;
    float timerAttack = 10f;

    private void Start()
    {
        BowPowerSlider.value = 0f;
        BowPowerSlider.maxValue = MaxBowCharge;
    }

    private void Update()
    {
        //if(Input.GetMouseButton(0) && CanFire)
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log("Shoot Range");
            // ChargeBow();
            FireBow();
        }
        //else if (Input.GetMouseButtonDown(1))
        //{
            // ChargeBow();
        //    FireBowTwo();
       // }
        // else if(Input.GetMouseButtonUp(0) && CanFire)
        // {

        // }
        /*        if (Input.GetMouseButton(1) && CanFire)
                {
                    ChargeBow();
                }
                else if (Input.GetMouseButtonUp(1) && CanFire)
                {
                    FireBow();
                }*/
        else
        {
            if (BowCharge > 0f)
            {
                BowCharge -= 1f * Time.deltaTime;
            }
            else
            {
                BowCharge = 0f;
                CanFire = true;
            }
            BowPowerSlider.value = BowCharge;
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
        if ((Input.GetMouseButtonDown(1)))
        {
            playerIsAttacking = true;
            timerAttack = 15f;
        }

    }

    void ChargeBow()
    {
        ArrowGFX.enabled = true;

        BowCharge += Time.deltaTime;

        BowPowerSlider.value = BowCharge;
        BowPowerSlider.value = 1f;

        if (BowCharge > MaxBowCharge)
        {
            BowPowerSlider.value = MaxBowCharge;
        }
    }

    void FireBow()
    {
        if (BowCharge > MaxBowCharge)
        {
            BowCharge = MaxBowCharge;
        }

        //float ArrowSpeed = BowCharge + BowPower;
        //float ArrowDamage = BowCharge * BowPower;
        float ArrowSpeed = 0 + BowPower;
        float ArrowDamage = 1 * BowPower;
        // float ArrowSpeedTwo = 0 + BowPower;
        //float ArrowDamageTwo = 1 * BowPower;

        float angle = Utility.AngleTowardsMouse(Bow.position);
        Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
        //Quaternion rotTwo = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        Arrow Arrow = Instantiate(ArrowPrefab, Bow.position, rot).GetComponent<Arrow>();
        //ArrowTwo ArrowTwo = Instantiate(ArrowPrefabBolt, Bow.position, rotTwo).GetComponent<ArrowTwo>();
        Arrow.ArrowVelocity = ArrowSpeed;
        Arrow.ArrowDamage = ArrowDamage;
        //ArrowTwo.ArrowVelocityTwo = ArrowSpeedTwo;
        //ArrowTwo.ArrowDamageTwo = ArrowDamageTwo;

        CanFire = false;
        ArrowGFX.enabled = false;
    }

    void FireBowTwo()
    {
        if (BowCharge > MaxBowCharge)
        {
            BowCharge = MaxBowCharge;
        }

        float ArrowSpeedTwo = 1 + BowPower;
        float ArrowDamageTwo = (BowPower / 2);

        float angle = Utility.AngleTowardsMouse(Bow.position);
        //Quaternion rot = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));
        Quaternion rotTwo = Quaternion.Euler(new Vector3(0f, 0f, angle - 90f));

        //Arrow Arrow = Instantiate(ArrowPrefab, Bow.position, rot).GetComponent<Arrow>();
        ArrowTwo ArrowTwo = Instantiate(ArrowPrefabBolt, Bow.position, rotTwo).GetComponent<ArrowTwo>();
        //Arrow.ArrowVelocity = ArrowSpeed;
        //Arrow.ArrowDamage = ArrowDamage;
        ArrowTwo.ArrowVelocityTwo = ArrowSpeedTwo;
        ArrowTwo.ArrowDamageTwo = ArrowDamageTwo;

        CanFire = false;
        ArrowGFX.enabled = false;
    }
}
