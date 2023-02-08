using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class followP : MonoBehaviour
{
    // Start is called before the first frame update
    private Transform player;
    public bool haskey = false;
    public float speed;
    public GameObject circle;
    public Enemy Enemy;
    void Start()
    {

        circle.SetActive(false);
    }
    public void waittill()
    {

    }
    public void begin()
    {
        circle.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(waiter());
        circle.SetActive(true);
        haskey = true;


    }
    void OnTriggerEnter2D(Collider2D player)
    {

        if (player.gameObject.tag == "Player")
        {
            Destroy(circle, 0f);
        }

    }
    IEnumerator waiter()
    {
        //Wait for 4 seconds
        yield return new WaitForSeconds(3f);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        float distanceFromPlayer = Vector2.Distance(player.position, transform.position);
        transform.position = Vector2.MoveTowards(this.transform.position, player.position, speed * Time.deltaTime);

    }
}
