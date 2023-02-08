using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawn : MonoBehaviour
{
    public GameObject[] spawnpoint;
    GameObject currentpoint;
    int index;

    public GameObject enemy;
    public float mintime;
    public float maxime;
    public bool canspawn = false;
    public float spawntime;
    public int noofenemy;

    private float timeBtwShots;
    public float startTimeBtwShots;
    public bool spawning;



    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        /*if (timeBtwShots <= .5)
        {
            spawning = true;
        }
        else
        {
            spawning = false;
        }*/
        if(timeBtwShots <= .5)
        {
  //          spawning = true;
        }
        if(timeBtwShots <= 0)
        {
            // Invoke("Spawnenemy", 2f);

            Spawnenemy();

//            spawning = false;

            timeBtwShots = startTimeBtwShots;
        }
        else
        {           
            
            timeBtwShots -= Time.deltaTime;
        }
        
    }

    void Spawnenemy()
    {
        index = Random.Range(0, spawnpoint.Length);
        currentpoint = spawnpoint[index];
        int numOfEnemiesSpawned = 0;

        if (noofenemy < 30)
        {
            if ((canspawn == true))
            {
                //   spawning = true;
                Instantiate(enemy, currentpoint.transform.position, Quaternion.identity);
                noofenemy++;
            }

            Invoke("Spawnenemy", mintime);
        }
    }
    // Start is called before the first frame update

}
