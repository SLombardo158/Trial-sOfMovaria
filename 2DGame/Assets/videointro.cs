using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class videointro : MonoBehaviour
{
    public GameObject videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.SetActive(false);
        if (Input.GetMouseButtonDown(0))
        {
            task();
        }
    }

    void task()
    {
        videoPlayer.SetActive(true);
        Destroy(videoPlayer, 16f);
    }
   
}
