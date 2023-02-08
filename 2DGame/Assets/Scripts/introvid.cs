using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class introvid : MonoBehaviour
{
    public GameObject videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(videoPlayer, 16f);
    }

    
}
