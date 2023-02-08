using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cut1 : MonoBehaviour

{
    public static bool iscuton;
    public GameObject videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.SetActive(false);
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider player)
    {
        if(player.gameObject.tag== "Player")
        {
            iscuton = true;
            videoPlayer.SetActive(true);
            Destroy(videoPlayer, 5f);
            iscuton=false;
        }
        
    }
}
