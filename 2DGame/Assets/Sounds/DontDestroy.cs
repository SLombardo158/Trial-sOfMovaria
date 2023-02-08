using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource song;
    void Start()
    {   
            DontDestroyOnLoad(song);
       
    }
    
}
