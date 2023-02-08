using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Threading;
using UnityEngine;

public class Shootingenemy : MonoBehaviour
{
    public float speed;
    private Transform player;
    public float lineOfSight;
    public float shootingRange;
    public float firerate = 1f;
    public float nextfiretime;
    public GameObject bullet;
    public GameObject bulletparent;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; 
    }

    // Update is called once per frame
    void Update()
    {
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        if (distanceFromPlayer < lineOfSight&& distanceFromPlayer> shootingRange)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);
        }
        else if (distanceFromPlayer <= shootingRange&&nextfiretime <Time.time)
        {
            Instantiate(bullet, bulletparent.transform.position, Quaternion.identity);
            nextfiretime = Time.time + firerate;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lineOfSight);
        Gizmos.DrawWireSphere(transform.position, shootingRange);
    }
}
