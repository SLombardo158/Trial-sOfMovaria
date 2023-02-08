using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{

  //  public SpriteRenderer spriteRenderer;
    public Sprite newSprite;
    public Sprite oldSprite;
    [SerializeField] private Slider timer;
    public int time;
    bool goUp = true;
    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        timer.maxValue = time;    
    }

    // Update is called once per frame
    void Update()
    {
        
        if(timer.value == 0)
        {
            //    while(timer.value < timer.maxValue)
            //    {
            //        timer.value += Time.deltaTime;
            //    }
            this.gameObject.GetComponent<SpriteRenderer>().sprite = oldSprite;
            goUp = false;
            timer.value += (Time.deltaTime / 4);
            
        }
        else if(timer.value == timer.maxValue)
        {
            if (Input.GetKey(KeyCode.T))
            {
                this.gameObject.GetComponent<SpriteRenderer>().sprite = newSprite; 
                goUp = true;
                timer.value -= Time.deltaTime;
                //Reduce Player health
            }
        }     
        else if((timer.value > 0) && (timer.value < timer.maxValue))
        {
            if(!goUp)
            {
                timer.value += (Time.deltaTime / 4);
            }
            else
            {
                timer.value -= Time.deltaTime;
               
            }
        }
    }
}
